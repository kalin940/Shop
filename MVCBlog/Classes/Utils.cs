using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.Classes
{
    public class Utils
    {
        public static string CutText(string text, int maxSize=100)
        {
            if (text == null)
            {
                return null;
            }
            if (text.Length > maxSize)
            {
                return text.Substring(0, maxSize) + "...";
            }
            return text;
        }
    }
}