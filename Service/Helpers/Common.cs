using Entities.Component;
using Entities.DTO;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;

namespace Services.Helpers
{
    public static class Common
    {
        public static List<DropDownItemData<TProperty>> ToDropDownList<T, TProperty>(
        this IEnumerable<T> source,
        Func<T, TProperty> codeSelector,
        Func<T, string> textSelector) where TProperty : class
        {
            return source.Select(item => new DropDownItemData<TProperty>
            {
                Code = codeSelector(item),
                Text = textSelector(item)
            }).ToList();
        }

        public static Type GetType(string type)
        {
            switch (type)
            {
                case "String":
                    return typeof(string);
                case "DateTime":
                    return typeof(DateTime);
                case "Int32":
                    return typeof(int);
                case "Decimal":
                    return typeof(decimal);
                default: return null;
            }
        }
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
        public static Dictionary<string, object> ToDictionary(this WinnerModel winner, params Expression<Func<WinnerModel, object>>[] selectors)
        {
            var dict = new Dictionary<string, object>();

            if (selectors == null || selectors.Length == 0)
            {
                foreach (PropertyInfo prop in typeof(WinnerModel).GetProperties())
                {
                    dict[prop.Name] = prop.GetValue(winner);
                }
                return dict;
            }

            foreach (var selector in selectors)
            {
                if (selector.Body is MemberExpression member)
                {
                    var key = member.Member.Name;
                    var value = selector.Compile()(winner);
                    dict[key] = value;
                }
                else if (selector.Body is UnaryExpression unary && unary.Operand is MemberExpression memberExp)
                {
                    var key = memberExp.Member.Name;
                    var value = selector.Compile()(winner);
                    dict[key] = value;
                }
            }

            return dict;
        }
    }
}
