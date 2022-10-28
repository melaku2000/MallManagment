using MallManagment.Shared.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace MallManagment.Server.Repositories.Extenssions
{
    public static class AdminsRepoExtenssion
    {
        public static IQueryable<Adminstrator> Search(this IQueryable<Adminstrator> Adminstrators, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Adminstrators;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return Adminstrators.Where(p => p.Email!.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Adminstrator> Sort(this IQueryable<Adminstrator> Adminstrators, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return Adminstrators.OrderBy(e => e.Email);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return Adminstrators.OrderBy(e => e.Email);

            return Adminstrators.OrderBy(orderQuery);
        }
    }
}
