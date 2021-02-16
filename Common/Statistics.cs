using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static www.dagligkaffe.dk.Common.IP_Stats;

namespace www.dagligkaffe.dk.Common
{
    public class IP_Stats
    {
        public static List<Elems> list = new List<Elems>();
        public static bool Logged(Elems elem)
        {
            return list.Where(e => e.Equals(elem)).Count() > 0;
        }
        public class Elems
        {
            public string ip { get; set; }
            public string cats { get; set; }
            public DateTime date { get; set; }
            /*public static bool operator == (Elems a, Elems b)
            {
                return a.ip == b.ip;
            }
            public static bool operator != (Elems a, Elems b)
            {
                return a.ip != b.ip;
            }*/
            public bool Equals(Elems b)
            {
                return this.ip == b.ip;
            }
        }
    }
    public class Stats
    {
        public bool ok { get; set; }
        public string ip { get; set; }
        public bool first { get; set; }
        public int users_per_day { get; set; }
        public int users_per_month { get; set; }
        public int booths_count { get; set; }
        public int items_count { get; set; }
    }
    public class Statistics
    {
        Stats stats = new Stats();

        public Statistics()
        {

        }
        private void SetUsersPerDay(string ip, string path, int count, out bool first, out int users_per_day)
        {
            DateTime now = DateTime.Now;

            var xdoc = XElement.Load(path);
            var elem = xdoc.Element("usersperday");
            int old_count = int.Parse(elem.Attribute("count").Value);
            string old_date = elem.Attribute("date").Value;

            if (now.ToString("dd-MM-yyyy") == old_date)
            {
                if (!IP_Stats.Logged(new Elems() { ip = ip }))
                {
                    int new_count = old_count + count;
                    elem.Attribute("count").SetValue(new_count);

                    first = true;
                    users_per_day = new_count;

                    //if (!string.IsNullOrEmpty(c_url))
                    //    return false;
                }
                else
                {
                    first = false;
                    users_per_day = old_count;
                }
                IP_Stats.list.Add(new Elems() { ip = ip, date = now });
            }
            else
            {
                IP_Stats.list = new List<Elems>();
                IP_Stats.list.Add(new Elems { ip = ip, date = now });
                elem.Attribute("count").SetValue(1);
                elem.Attribute("date").SetValue(now.ToString("dd-MM-yyyy"));

                first = true;
                users_per_day = 1;
            }
            if (first)
                xdoc.Save(path);
            //return true;
        }
        public Stats GetStatistics(string ip)
        {
            stats.ip = ip;
            string path = "";
            if (Statics.IsDebug)
                path = "C:\\inetpub\\wwwroot\\www.doctormembrain.com\\www.doctormembrain.com\\App_Data\\Statistics.xml";
            else
                path = "C:\\inetpub\\wwwroot\\www.doctormembrain.com\\App_Data\\Statistics.xml";

            bool first;
            //bool ok;
            int users_per_day;
            if (ip == Settings.Basic.IP() || Check.Generel.IsAdmin(ip))
                this.SetUsersPerDay(ip, path, 0, out first, out users_per_day);
            else
                this.SetUsersPerDay(ip, path, 1, out first, out users_per_day);

            this.stats.ip = ip;
            this.stats.first = first;
            this.stats.users_per_day = users_per_day;
            return stats;
        }
    }
}
