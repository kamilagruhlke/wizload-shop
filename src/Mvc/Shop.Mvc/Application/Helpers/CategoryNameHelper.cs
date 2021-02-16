using System.Text.RegularExpressions;

namespace Shop.Mvc.Application.Helpers
{
    public class CategoryNameHelper
    {
        public static string Normalize(string name) => Regex.Replace(name, "[^a-zA-Z0-9 ]", string.Empty).ToLower().Replace(" ", "-");
    }
}