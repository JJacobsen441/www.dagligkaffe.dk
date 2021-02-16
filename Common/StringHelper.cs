using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public static class StringHelper
    {
        public static string RemoveCharacter(string str, char character)
        {
            if (str == null)
                return null;
            int charindex = str.IndexOf(character);
            if (charindex != -1)
                str = str.Remove(charindex, 1);
            return str;
        }
        private static int CountChars(string str, char c)
        {
            int count = 0;
            foreach (char c_in_str in str)
            {
                if (c_in_str == c)
                    count += 1;
            }
            return count;
        }
        public static string _RemoveCharacters(string str, char[] characters)
        {
            if (str == null)
                return null;
            //string result="";
            foreach (char c in characters)
            {
                int count = CountChars(str, c);
                for (int i = 0; i < count; i++)
                    str = RemoveCharacter(str, c);
            }
            return str;
        }
        public static string RemoveStrings(string str, string[] strings)
        {
            if (str == null)
                return null;
            //string result="";
            foreach (string s in strings)
            {
                str = str.Replace(s, "");
            }
            return str;
        }
        public static string SearchStringForTag(string str, string allow_tag)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ElementAt(i);
                if (i < str.Length - 4 && str.Substring(i, 5) == "tag?1")
                {
                    int len1, len2;
                    string templ = "href=\"/wiki?wiki_id=***\"";
                    string val = Val(str, i + 3, out len1);
                    string text = Text(str, i + 3 + len1 + 2, out len2);
                    string tag = "<" + allow_tag + " " + templ.Replace("***", val) + ">" + text + "</" + allow_tag + ">";
                    str = str.Replace(str.Substring(i, 3 + 2 + len1 + 2 + len2 + 2), tag);
                }
            }
            return str;
        }
        private static string Val(string str, int index, out int len)
        {
            //index = index + 5;
            string res = "";
            int counter = 0;
            for (int i = index + 2; i < str.Length; i++)
            {
                if (str.Substring(i, 2) == "?2")
                {
                    res = str.Substring(index + 2, counter);
                    len = res.Length;
                    return res;
                }
                counter++;
            }
            len = -1;
            return "";
        }
        private static string Text(string str, int index, out int len)
        {
            //index = index + 5;
            string res = "";
            int counter = 0;
            for (int i = index + 2; i < str.Length; i++)
            {
                if (str.Substring(i, 2) == "?3")
                {
                    res = str.Substring(index + 2, counter);
                    len = res.Length;
                    return res;
                }
                counter++;
            }
            len = -1;
            return "";
        }

        public static string OnlyAlphanumeric(string str, bool allow_newline, bool allow_upper, string allow_tag, char[] allowed, out bool ok)
        {
            ok = true;
            if (str == null)
            {
                ok = false;
                return null;
            }

            char[] numeric = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] alphalower = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'æ', 'ø', 'å' };
            char[] alphaupper = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Æ', 'Ø', 'Å' };
            char[] newline = { '\r', '\n' };

            //string result="";
            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ElementAt(i);
                if (allowed.Contains(c) || (allow_newline && newline.Contains(c)) || (allow_upper && alphaupper.Contains(c)) || alphalower.Contains(c) || numeric.Contains(c))
                    ;
                else
                {
                    str = RemoveCharacter(str, c);
                    ok = false;
                    i--;
                }
            }
            //str = str.Replace("<", "");
            //str = str.Replace(">", "");
            string tmp = str;
            for (int i = 0; i < str.Length; i++)
            {
                if (allow_tag != "")
                {
                    char c = str.ElementAt(i);
                    if (i < str.Length - allow_tag.Length - 4 && str.Substring(i, allow_tag.Length + 4) == "<" + allow_tag + " />")
                        i += allow_tag.Length + 3;
                    else if (i < str.Length - allow_tag.Length - 2 && str.Substring(i, allow_tag.Length + 2) == "<" + allow_tag + ">")
                        i += allow_tag.Length + 1;
                    else if (i < str.Length - allow_tag.Length - 3 && str.Substring(i, allow_tag.Length + 3) == "</" + allow_tag + ">")
                        i += allow_tag.Length + 2;
                    else if (c == '<')
                    {
                        int j = i;
                        int counter = 1;
                        while (j < i + 15 && j < str.Length)
                        {
                            if (str.ElementAt(j) == '>')
                            {
                                str = str.Replace(str.Substring(i, counter), "");
                                //i -= counter;
                                break;
                            }
                            j++;
                            counter++;
                        }
                    }

                }
            }
            ok = str == tmp;
            return str;
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
