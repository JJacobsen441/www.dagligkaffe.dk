using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public static class Extensions
    {
        public static string Capitalize(this string str)
        {
            return string.IsNullOrEmpty(str) ? "" : str.Substring(0, 1).ToUpper() + str.Substring(1);// new HtmlString(anchorHtml);
        }

        public static IHtmlContent ActionImage(this IHtmlHelper html/*, IUrlHelper url*/, string action, string controllerName, object routeValues, string sepPath, string imagePath, string data_src, string alt = null, string[] classes_img = null, string[] classes_a = null)
        {
            UrlHelperFactory urlHelperFactory = (UrlHelperFactory)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var url = urlHelperFactory.GetUrlHelper(html.ViewContext);

            // build the <img> tag
            TagBuilder imgBuilder = new TagBuilder("img");
            imgBuilder.TagRenderMode = TagRenderMode.SelfClosing;
            if (imagePath != null)
                imgBuilder.MergeAttribute("src", url.Content(imagePath));
            if (sepPath != null)
                imgBuilder.MergeAttribute("data-sep", url.Content(sepPath));
            if (data_src != null)
                imgBuilder.MergeAttribute("data-src", url.Content(data_src));
            if (alt != null)
                imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("loading", "lazy");
            string classes_img_string = "";
            foreach (string cssclass in classes_img)
            {
                if (cssclass != null)
                    classes_img_string += " " + cssclass;
            }
            imgBuilder.MergeAttribute("class", classes_img_string);
            string imgHtml;
            using (StringWriter writera = new StringWriter())
            {
                imgBuilder.WriteTo(writera, HtmlEncoder.Default);
                imgHtml = writera.ToString();
            }

            // build the <a> tag
            TagBuilder anchorBuilder = new TagBuilder("a");
            anchorBuilder.TagRenderMode = TagRenderMode.Normal;
            if (action != "")
                anchorBuilder.MergeAttribute("href", url.Action(action, controllerName, routeValues));
            else
                anchorBuilder.MergeAttribute("href", "#/");
            if (classes_a != null)
            {
                string classes_a_string = "";
                foreach (string cssclass in classes_a)
                {
                    if (cssclass != null)
                        classes_a_string += " " + cssclass;
                }
                anchorBuilder.MergeAttribute("class", classes_a_string);
            }
            anchorBuilder.InnerHtml.SetHtmlContent(imgHtml); // include the <img> tag inside
            string anchorHtml;
            using (StringWriter writerb = new StringWriter())
            {
                anchorBuilder.WriteTo(writerb, HtmlEncoder.Default);
                anchorHtml = writerb.ToString();
            }
            return new HtmlString(anchorHtml);// new HtmlString(anchorHtml);

        }
        public static IHtmlContent RouteImage(this IHtmlHelper html, string route, object routeValues, string sepPath, string imagePath, string data_src, string alt = null, string[] classes_img = null, string[] classes_a = null)
        {
            UrlHelperFactory urlHelperFactory = (UrlHelperFactory)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var url = urlHelperFactory.GetUrlHelper(html.ViewContext);

            // build the <img> tag
            TagBuilder imgBuilder = new TagBuilder("img");
            imgBuilder.TagRenderMode = TagRenderMode.SelfClosing;
            if (imagePath != null)
                imgBuilder.MergeAttribute("src", url.Content(imagePath));
            if (sepPath != null)
                imgBuilder.MergeAttribute("data-sep", url.Content(sepPath));
            if (data_src != null)
                imgBuilder.MergeAttribute("data-src", url.Content(data_src));
            if (alt != null)
                imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("loading", "lazy");

            string classes_img_string = "";
            foreach (string cssclass in classes_img)
            {
                if (cssclass != null)
                    classes_img_string += " " + cssclass;
            }
            imgBuilder.MergeAttribute("class", classes_img_string);
            string imgHtml;
            using (StringWriter writera = new StringWriter())
            {
                imgBuilder.WriteTo(writera, HtmlEncoder.Default);
                imgHtml = writera.ToString();
            }

            // build the <a> tag
            TagBuilder anchorBuilder = new TagBuilder("a");
            anchorBuilder.TagRenderMode = TagRenderMode.Normal;
            if (route != "")
                anchorBuilder.MergeAttribute("href", url.RouteUrl(route, routeValues));
            else
                anchorBuilder.MergeAttribute("href", "#/");
            if (classes_a != null)
            {
                string classes_a_string = "";
                foreach (string cssclass in classes_a)
                {
                    if (cssclass != null)
                        classes_a_string += " " + cssclass;
                }
                anchorBuilder.MergeAttribute("class", classes_a_string);
            }
            anchorBuilder.InnerHtml.SetHtmlContent(imgHtml); // include the <img> tag inside
            string anchorHtml;
            using (StringWriter writerb = new StringWriter())
            {
                anchorBuilder.WriteTo(writerb, HtmlEncoder.Default);
                anchorHtml = writerb.ToString();
            }
            return new HtmlString(anchorHtml);// new HtmlString(anchorHtml);

        }
        public static IHtmlContent ActionCheckBox(this IHtmlHelper html, /*IUrlHelper url, */string name, bool isChecked, string action, string controllerName, object routeValues, string[] classes = null)
        {
            UrlHelperFactory urlHelperFactory = (UrlHelperFactory)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var url = urlHelperFactory.GetUrlHelper(html.ViewContext);

            // build the <img> tag
            TagBuilder checkBuilder = new TagBuilder("input");
            checkBuilder.MergeAttribute("type", "checkbox");
            if (name != null)
            {
                checkBuilder.MergeAttribute("data-dat", name);
                checkBuilder.MergeAttribute("id", name);
            }

            if (isChecked)
                checkBuilder.MergeAttribute("checked", "checked");

            string classes_string = "";
            foreach (string cssclass in classes)
            {
                if (cssclass != null)
                    classes_string += " " + cssclass;
            }
            checkBuilder.MergeAttribute("class", classes_string);
            if (routeValues != null)
                checkBuilder.MergeAttribute("onclick", "location.href='" + url.Action(action, controllerName, routeValues) + "'");// + "?c=" + routeValues);
            else
                checkBuilder.MergeAttribute("onclick", "actionFunc(this)");
            string checkHtml;
            using (StringWriter writer = new StringWriter())
            {
                checkBuilder.WriteTo(writer, HtmlEncoder.Default);
                checkHtml = writer.ToString();
            }
            return new HtmlString(checkHtml);
        }
        public static IHtmlContent DisplayWithBreaksFor(this IHtmlHelper html, string text)
        {
            if (String.IsNullOrEmpty(text))
                return new HtmlString("");

            text = text.Replace(Environment.NewLine, "<br />");
            text = text.Replace("\n", "<br />");
            IHtmlContent model = html.Raw(text);

            return model;
        }

        

        public static string HtmlEncode(string html)
        {

            //var httpUtil = new HttpServerUtilityWrapper(HttpContext.Current.Server);
            //string encoded = httpUtil.HtmlEncode(html).Replace(Environment.NewLine, "<br />");

            string encoded = WebUtility.HtmlEncode(html);
            if (String.IsNullOrEmpty(encoded))
                return "";

            //return MvcHtmlString.Create(encoded);
            return encoded;
        }
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectAsJson<T>(this ISession session, string key) where T : new()
        {
            var value = session.GetString(key);
            return value == null ? new T() : JsonConvert.DeserializeObject<T>(value);
        }

        public static void ResetTableIndex(this DbContext context, string table, int? reseedTo = 1) //where T : class
        {
            /*context.Database.ExecuteSqlCommand(
                $"DBCC CHECKIDENT('{context.GetTableName<T>()}',RESEED{(reseedTo != null ? "," + reseedTo : "")});" +
                $"DBCC CHECKIDENT('{context.GetTableName<T>()}',RESEED);");*/

            context.Database.ExecuteSqlRaw($"ALTER TABLE {table} ALTER COLUMN \"Id\" RESTART WITH {reseedTo}");
        }

        //public static string GetTableName<T>(this DbContext context) where T : class
        //{
        //    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //    return objectContext.GetTableName<T>();
        //}

        //public static string GetTableName<T>(this ObjectContext context) where T : class
        //{
        //    var sql = context.CreateObjectSet<T>().ToTraceString();
        //    var regex = new Regex(@"FROM\s+(?<table>.+)\s+AS");
        //    var match = regex.Match(sql);
        //    var table = match.Groups["table"].Value;
        //    return table;
        //}
    }
}