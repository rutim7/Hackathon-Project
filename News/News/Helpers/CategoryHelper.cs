using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace News.Helpers
{
    public class CategoryHelper
    {
        private const char Separator = ';';

        public static List<string> GetUserCategory(string userCategoryField)
        {
            var list = userCategoryField.Split(Separator);

            return list.ToList();
        }

        public static string SetUserCategory(List<string> categories)
        {
            var str =new StringBuilder();
            foreach (var item in categories)
            {
                str.Append(item + Separator);
            }

            return str.ToString();
        }
    }
}