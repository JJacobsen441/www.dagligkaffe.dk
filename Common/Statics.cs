using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public class Conf
    {
        public static readonly IConfiguration _configuration;
        static Conf()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
        public static string ConnectionString()
        {
            return _configuration.GetConnectionString("MyConnection");
        }
        //public static string Secret2()
        //{
        //    return _configuration.GetValue<string>("Secret2");
        //}
        //public static string ClientID2()
        //{
        //    return _configuration.GetValue<string>("ClientID2");
        //}
        //public static string SecretNets()
        //{
        //    return _configuration.GetValue<string>("SecretNets");
        //}
        //public static string ClientIDNets()
        //{
        //    return _configuration.GetValue<string>("ClientIDNets");
        //}
        //public static string Account()
        //{
        //    return _configuration.GetValue<string>("Account");
        //}
        //public static string CVR()
        //{
        //    return _configuration.GetValue<string>("CVR");
        //}
        //public static string Password()
        //{
        //    return _configuration.GetValue<string>("Password");
        //}
    }

    //public class BaseSum
    //{
    //    public int qty;
    //    public double ship;
    //    public double vat;
    //    public double t_vat;
    //    public int b_price;
    //    public double n_price;
    //    public double b_total;
    //    public double n_total;
    //    public double b_total_noship;
    //    public double n_total_noship;

    //    //public string vat;
    //    public string str_qty;
    //    public string str_ship;
    //    public string str_vat;
    //    public string str_t_vat;
    //    public string str_b_price;
    //    public string str_n_price;
    //    public string str_b_total;
    //    public string str_n_total;
    //    public string str_b_total_noship;
    //    public string str_n_total_noship;

    //    public string str_ship_100;
    //    public string str_t_vat_100;
    //    public string str_b_price_100;
    //    public string str_n_price_100;
    //    public string str_b_total_100;
    //    public string str_n_total_100;
    //    public string str_b_total_noship_100;
    //    public string str_n_total_noship_100;

    //    private string DecimalNumber(double num, bool _100)
    //    {
    //        string[] arr;
    //        if (("" + num).Contains("."))
    //            arr = ("" + num).Split(".");
    //        else if (("" + num).Contains(","))
    //            arr = ("" + num).Split(",");
    //        else
    //            arr = new string[] { "" + num, "00" };

    //        string first = arr[0];
    //        string dec;

    //        if (arr.Length > 0)
    //            dec = arr[1];
    //        else
    //            dec = "0";

    //        if (dec.Length == 1)
    //            dec = dec.Substring(0, 1) + "0";
    //        else if (dec.Length > 1)
    //            dec = dec.Substring(0, 2);
    //        string div = ".";
    //        if (_100)
    //            div = "";
    //        return first + div + dec;
    //    }
    //    public void Set()
    //    {
    //        str_qty = "" + qty;
    //        str_ship = "" + ship + ".00";
    //        str_vat = DecimalNumber(vat, false);
    //        str_t_vat = DecimalNumber(t_vat, false);
    //        str_b_price = b_price + ".00";
    //        str_n_price = DecimalNumber(n_price, false);
    //        str_b_total = DecimalNumber(b_total, false);
    //        str_n_total = DecimalNumber(n_total, false);
    //        str_b_total_noship = DecimalNumber(b_total_noship, false);
    //        str_n_total_noship = DecimalNumber(n_total_noship, false);

    //        str_t_vat_100 = DecimalNumber(t_vat, true);
    //        str_b_price_100 = b_price + "00";
    //        str_n_price_100 = DecimalNumber(n_price, true);
    //        str_b_total_100 = DecimalNumber(b_total, true);
    //        str_n_total_100 = DecimalNumber(n_total, true);
    //        str_b_total_noship_100 = DecimalNumber(b_total_noship, true);
    //        str_n_total_noship_100 = DecimalNumber(n_total_noship, true);
    //        str_ship_100 = ship + "00";
    //    }
    //}

    public class Characters
    {
        public static char[] All(bool withreturnnewline)
        {
            char c = '\r';
            char r = '\n';
            //char[] a = new char[] { ' ', '.', ',', '\'', '"', ':', ';', '&', '#', '!', '?', '/', '%', '+', '-', '(', ')', '[', ']', '{', '}', '<', '>' };
            char[] a = new char[] { ' ', '.', ',', '+', '-', '*', '/', ':', ';', '\'', '#', '(', ')', '[', ']', '<', '>', '{', '}', '=', '?', '!', '@', '%', '$', '&' };
            if (withreturnnewline)
                a = a.Append(c).ToArray();
            if (withreturnnewline)
                a = a.Append(r).ToArray();
            return a;
        }
        public static char[] Limited(bool withsemi)
        {
            char s = ';';
            char[] a = new char[] { ' ', '_', ',', '+', '-', '\'', '&', '#', '(', ')', '[', ']' };
            if (withsemi)
                a = a.Append(s).ToArray();
            return a;
        }
        public static char[] VeryLimited()
        {
            return new char[] { ' ', '\'', '-', '&', '#' };
        }
        public static char[] Website()
        {
            return new char[] { '-', '/', '.', ':' };
        }
        public static char[] Name()
        {
            return new char[] { ' ', '-' };
        }
        public static char[] Address()
        {
            return new char[] { ' ', '.', ',', '-', '\'' };
        }
        public static char[] Space()
        {
            return new char[] { ' ' };
        }
    }

    public class Statics
    {

        //public static double Round(double num, int dec, bool up)
        //{
        //    if (up)
        //        return Math.Round(num * dec) / dec;
        //    return Math.Floor(num * dec) / dec;
        //}

        //public static double Vat(string price, out double vat)
        //{
        //    double pr = double.Parse(price);
        //    double non = Round(pr * 0.8, 100, false);
        //    vat = Round(pr - non, 100, true);
        //    return non;
        //}
        //public static BaseSum BaseCalc(string amount, string quantity, string shipping)
        //{
        //    BaseSum bs = new BaseSum();
        //    bs.qty = int.Parse(quantity);
        //    bs.ship = double.Parse(shipping);

        //    bs.b_price = int.Parse(amount);

        //    bs.n_price = Statics.Vat("" + bs.b_price, out bs.vat);
        //    bs.b_total = bs.qty * bs.b_price + bs.ship;
        //    bs.n_total = bs.qty * bs.n_price + bs.ship;
        //    bs.b_total_noship = bs.qty * bs.b_price;
        //    bs.n_total_noship = bs.qty * bs.n_price;
        //    Statics.Vat("" + (bs.b_price * bs.qty), out bs.t_vat);
        //    bs.Set();

        //    return bs;
        //}
        public static bool IsDebug
        {
            get
            {
                bool isDebug = false;
#if DEBUG
                isDebug = true;
#endif
                return isDebug;
            }
        }

        public static string WWWroot
        {
            get
            {
                string nd = Path.DirectorySeparatorChar.ToString();
                //string contentRootPath = _env.ContentRootPath;
                //string webRootPath = _env.WebRootPath;
                string contentRootPath = Directory.GetCurrentDirectory();
                string webRootPath = "wwwroot";

                return contentRootPath + nd + webRootPath + nd;
            }
        }
        //public static string Images
        //{
        //    get
        //    {
        //        string nd = Path.DirectorySeparatorChar.ToString();
        //        //string contentRootPath = _env.ContentRootPath;
        //        //string webRootPath = _env.WebRootPath;
        //        string contentRootPath = Directory.GetCurrentDirectory();
        //        string webImagePath = "_content" + nd + "images";

        //        return contentRootPath + nd + webImagePath + nd;
        //    }
        //}
        public static string Root
        {
            get
            {
                string nd = Path.DirectorySeparatorChar.ToString();
                //string contentRootPath = _env.ContentRootPath;
                //string webRootPath = _env.WebRootPath;
                string contentRootPath = Directory.GetCurrentDirectory();
                //string webRootPath = "wwwroot";

                return contentRootPath + nd;
            }
        }
        //public static string Content
        //{
        //    get
        //    {
        //        string nd = Path.DirectorySeparatorChar.ToString();
        //        //string contentRootPath = _env.ContentRootPath;
        //        //string webRootPath = _env.WebRootPath;
        //        string contentRootPath = Directory.GetCurrentDirectory();
        //        //string webRootPath = "wwwroot";

        //        return contentRootPath + nd + "_content" + nd; ;
        //    }
        //}
        //public static string RootOrders
        //{
        //    get
        //    {
        //        string nd = Path.DirectorySeparatorChar.ToString();
        //        //string contentRootPath = _env.ContentRootPath;
        //        //string webRootPath = _env.WebRootPath;
        //        string contentRootPath;
        //        if (IsDebug)
        //            contentRootPath = "C:\\VTRoot\\HarddiskVolume4\\inetpub\\wwwroot\\www.centr.dk\\www.centr.dk";
        //        else
        //            contentRootPath = "C:\\VTRoot\\HarddiskVolume4\\inetpub\\wwwroot\\www.centr.dk\\www.centr.dk";
        //        //string webRootPath = "wwwroot";

        //        return contentRootPath + nd;
        //    }
        //}
        //public static DateTime Marker
        //{
        //    get
        //    {
        //        return DateTime.ParseExact("2020-09-30 12:00:00", "yyyy-MM-dd HH:mm:ss", new CultureInfo("da-DK"));
        //    }
        //}


        public static void Log(string msg)
        {
            try
            {
                string nd = Path.DirectorySeparatorChar.ToString();
                //string contentRootPath = _env.ContentRootPath;
                //string webRootPath = _env.WebRootPath;
                string contentRootPath = Directory.GetCurrentDirectory();

                string path = contentRootPath + nd + "_content" + nd + "log" + nd + "logfile.txt";
                using (StreamWriter writer = System.IO.File.AppendText(path))
                {
                    string d = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    writer.WriteLine(d + ": " + msg);
                }
            }
            catch (Exception e)
            {
                ;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static bool IsNull<T>(T value) where T : class
        {
            return value == null;
        }

        public static bool IsNotNull<T>(T value) where T : class
        {
            return value != null;
        }

        public static bool IsNull<T>(T? nullableValue) where T : struct
        {
            return !nullableValue.HasValue;
        }

        public static bool IsNotNull<T>(T? nullableValue) where T : struct
        {
            return nullableValue.HasValue;
        }

        public static bool HasValue<T>(T? nullableValue) where T : struct
        {
            return nullableValue.HasValue;
        }

        public static bool HasNoValue<T>(T? nullableValue) where T : struct
        {
            return !nullableValue.HasValue;
        }
    }
}
