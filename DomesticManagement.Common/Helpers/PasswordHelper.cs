using System.Text.RegularExpressions;

namespace DomesticManagement.Common.Helpers
{
    public static class PasswordHelper
    {
        public static bool CheckPassword(string password)
        {
            if (password == null)
            {
                return false;
            }
            if (password.Length < 1)
                return false;
            if (password.Length < 4)
                return false;

            if (password.Length < 8)
                return false;

            if (Regex.Matches(password, @"[a-z]").Count <= 0)
            {
                return false;
            }
            if (Regex.Matches(password, @"[A-Z]").Count <= 0)
            {
                return false;
            }
            if (Regex.Matches(password, @"[0-9]").Count <= 0)
            {
                return false;
            }
            if (password.Contains(","))
            {
                return true;
            }
            if (password.Contains("!"))
            {
                return true;
            }
            if (password.Contains("@"))
            {
                return true;
            }
            if (password.Contains("#"))
            {
                return true;
            }
            if (password.Contains("$"))
            {
                return true;
            }
            if (password.Contains("*"))
            {
                return true;
            }
            if (password.Contains("%"))
            {
                return true;
            }
            if (password.Contains("^"))
            {
                return true;
            }
            if (password.Contains("&"))
            {
                return true;
            }
            if (password.Contains("?"))
            {
                return true;
            }
            if (password.Contains("_"))
            {
                return true;
            }
            if (password.Contains("~"))
            {
                return true;
            }
            if (password.Contains("("))
            {
                return true;
            }
            if (password.Contains(")"))
            {
                return true;
            }
            if (password.Contains("+"))
            {
                return true;
            }
            if (password.Contains("="))
            {
                return true;
            }
            if (password.Contains("{"))
            {
                return true;
            }
            if (password.Contains("["))
            {
                return true;
            }
            if (password.Contains("}"))
            {
                return true;
            }
            if (password.Contains("\\"))
            {
                return true;
            }
            if (password.Contains("]"))
            {
                return true;
            }
            if (password.Contains("/"))
            {
                return true;
            }
            if (password.Contains("-"))
            {
                return true;
            }
            if (password.Contains(":"))
            {
                return true;
            }
            if (password.Contains("'"))
            {
                return true;
            }
            if (password.Contains("|"))
            {
                return true;
            }
            if (password.Contains(">"))
            {
                return true;
            }
            return false;
        }
    }
}
