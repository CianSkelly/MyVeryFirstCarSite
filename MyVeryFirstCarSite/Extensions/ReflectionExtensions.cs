using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Extensions
{
    //this class has the static keyword as this class will contain extension methods
    public  static class ReflectionExtensions
    {
        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}