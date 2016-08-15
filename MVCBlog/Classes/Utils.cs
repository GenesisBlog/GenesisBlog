using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlog.Classes
{
    public class Utils
    {
        public static string CutText(string text, int maxLength = 100)
        {
            if (text == null || text.Length <= maxLength)
            {
                return text;
            }
            var shortText = text.Substring(0, maxLength) + "...";
            return shortText;
        }
    }
}