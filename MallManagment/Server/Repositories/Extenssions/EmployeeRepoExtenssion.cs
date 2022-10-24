using MallManagment.Shared.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace MallManagment.Server.Repositories.Extenssions
{
    public static class EmployeeRepoExtenssion
    {
        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return employees.Where(p => p.FullName!.ToLower().Contains(lowerCaseSearchTerm)
            || p.IDNumber!.ToLower().Contains(lowerCaseSearchTerm)
            || p.EmployeeNumber.ToString().ToLower().Contains(lowerCaseSearchTerm)
            || p.MobilePhone!.ToLower().Contains(lowerCaseSearchTerm)
            );
        }

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.EmployeeNumber);

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
                return employees.OrderBy(e => e.EmployeeNumber);

            return employees.OrderBy(orderQuery);
        }
    }
}
