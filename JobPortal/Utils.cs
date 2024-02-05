using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal
{
    public class Utils
    {
        public static bool IsValidExtention(string fileName)
        {
            bool isValid = false;
            string[] filExtentions = { ".jpg", ".jpeg", ".png" };
            for (int i = 0; i <= filExtentions.Length - 1; i++)
            {
                if (fileName.Contains(filExtentions[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
        public static bool IsValidExtentionForResume(string fileName)
        {
            bool isValid = false;
            string[] filExtentions = { ".doc", ".docx", ".pdf" };
            for (int i = 0; i <= filExtentions.Length - 1; i++)
            {
                if (fileName.Contains(filExtentions[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
    }
}