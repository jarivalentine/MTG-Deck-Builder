using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> Sort(this IQueryable<Card> cards, string orderByQueryString)
    {
        if (string.IsNullOrEmpty(orderByQueryString))
        {
            return cards;
        }

        string[] orderParameters = orderByQueryString.Trim().Split(',');
        PropertyInfo[] propertyInfos = typeof(Card).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        StringBuilder orderQueryBuilder = new StringBuilder();

        foreach (string param in orderParameters)
        {
            if (!string.IsNullOrEmpty(param))
            {
                string propertyFromQueryName = param.Split(" ")[0];
                PropertyInfo objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is not null)
                {
                    string direction = param.EndsWith(" desc") ? "descending" : "ascending";
                    orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
                }
            }
        }

        string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return cards.OrderBy(b => b.Name);
        }

        return cards.OrderBy(orderQuery);
    }
}
