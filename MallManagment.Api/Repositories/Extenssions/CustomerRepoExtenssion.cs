using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using MallManagment.Models.Entities;

namespace MallManagment.Api.Repositories.Extenssions
{
    public static class CustomerRepoExtenssion
    {
        public static IQueryable<Customer> Search(this IQueryable<Customer> Customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Customers;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return Customers.Where(p => p.FullName!.ToLower().Contains(lowerCaseSearchTerm)
            || p.CompanyTin.ToString().ToLower().Contains(lowerCaseSearchTerm)
            || p.MobilePhone!.ToLower().Contains(lowerCaseSearchTerm)
            );
        }

        public static IQueryable<Customer> Sort(this IQueryable<Customer> Customers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return Customers.OrderBy(e => e.CompanyTin);

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
                return Customers.OrderBy(e => e.CompanyTin);

            return Customers.OrderBy(orderQuery);
        }
    }
}
