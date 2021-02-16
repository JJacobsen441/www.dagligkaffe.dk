using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public class Check
    {
        public class Generel
        {
            public static bool IsValidEmail(string email)
            {
                try
                {
                    if (string.IsNullOrEmpty(email))
                        return false;
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            public static bool IsAdmin(string ip)
            {
                if (string.IsNullOrEmpty(ip))
                    return false;

                string set_ip = Settings.Basic.IP().Trim();
                ip = ip.Trim();
                if (Statics.IsDebug)
                    return ip == "::1" || ip == "127.0.0.1" || ip == set_ip;

                return ip == set_ip;
            }
            public static bool IsPhonenumber(int? nr)
            {
                if (nr != null && nr > 0 && nr <= 99999999 && nr.ToString().Count() == 8)
                    return true;
                return false;
            }
            public static bool IsPhonenumber(string nr)
            {
                int n;
                bool ok = int.TryParse(nr, out n);
                if (ok && n > 0 && n <= 99999999 && n.ToString().Count() == 8)
                    return true;
                return false;
            }

            public static bool IsAmount(ref string str)
            {
                int tmp;
                bool parse_ok = int.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp);
                if (parse_ok && tmp >= 0)
                {
                    str = "" + tmp;
                    return true;
                }
                return false;
            }
        }
    }
}