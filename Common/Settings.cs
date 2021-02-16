using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace www.dagligkaffe.dk.Common
{
    public class Settings
    {
        //private static string path 
        //{
        //    get
        //    {
        //        string path = "";
        //        //if (Statics.IsDebug)
        //        //    path = "C:\\inetpub\\wwwroot\\www.centr.dk\\www.centr.dk\\App_Data\\settings.xml";
        //        //else
        //        //    path = "C:\\inetpub\\wwwroot\\www.centr.dk\\App_Data\\settings.xml";
        //        if (Statics.IsDebug)
        //            path = Statics.content(_env) + "App_Data\\settings.xml";
        //        else
        //            path = "C:\\inetpub\\wwwroot\\www.centr.dk\\App_Data\\settings.xml";
        //        return path;
        //    }
        //}


        public class Basic
        {
            public static string SITE_NAME()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SITE_NAME")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string SITE_NAME_SHORT()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SITE_NAME_SHORT")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string SITE_NAME_FULL()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SITE_NAME_FULL")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string SLOGAN()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SLOGAN")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string COMMENT()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "COMMENT")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string EMAIL_ADMIN()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_ADMIN")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string EMAIL_MAIL()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_MAIL")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string EMAIL_NO_REPLY()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_NO_REPLY")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string EMAIL_TEST()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_TEST")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
            public static string IP()
            {
                var xdoc = XElement.Load(Statics.Root + "App_Data\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "IP")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
        }
    }
}
