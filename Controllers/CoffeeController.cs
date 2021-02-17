using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using www.dagligkaffe.dk.Common;
using www.dagligkaffe.dk.Models;

namespace www.dagligkaffe.dk.Controllers
{
    public class CoffeeController : Controller
    {
        private IHttpContextAccessor httpcon;
        private ErrorHandler err = new ErrorHandler();
        private Statistics stats = new Statistics();
        private Access access;
        private Session session;
        public CoffeeController() 
        {
            httpcon = new HttpContextAccessor();
            session = new Session(httpcon);
            string guid = Guid.NewGuid().ToString();
            access = new Access("entry", guid);
        }
        public IActionResult Index()
        {
            try
            {
                access.Queue();
                ViewBag.Session = session;

                if (session.IsMobile == "none")
                    return View("IsMobile");

                List<Coffee> list = new List<Coffee>();
                using (dagligkaffedkContext db = new dagligkaffedkContext())
                {

                    Random r = new Random();

                    Coffee cof1 = db.Coffees.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
                    if (cof1 != null)
                        list.Add(cof1);

                    Coffee cof2 = db.Coffees.OrderByDescending(x => x.Bean).FirstOrDefault();
                    if (cof2 != null)
                        list.Add(cof2);

                    int n = r.Next(0, db.Coffees.Count());
                    Coffee cof3 = db.Coffees.ToList()[n];
                    if (cof3 != null)
                        list.Add(cof3);

                }
                return View(list);
            }
            catch (Exception e)
            {
                Statics.Log(err.HandleError(TYPE.COFFEE, ERROR.INDEX, e));
                TempData["err_msg"] = err.HandleError(TYPE.COFFEE, ERROR.INDEX, e);
                TempData["ErrorMessage"] = "";
                return ErrorPage();
            }
            finally
            {
                try { /*db?.Dispose(); */access.UnQueue(); }
                catch (Exception e)
                {
                    string subject = "Fejl i finally!";
                    string body = Extensions.HtmlEncode("");
                    Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);
                }
            }
        }
        public IActionResult History()
        {
            try
            {
                access.Queue();
                ViewBag.Session = session;

                if (session.IsMobile == "none")
                    return View("IsMobile");

                List<Coffee> list = new List<Coffee>();
                using (dagligkaffedkContext db = new dagligkaffedkContext())
                {
                    list = db.Coffees.OrderByDescending(x => x.CreatedOn).ToList();
                }
                return View(list);
            }
            catch (Exception e)
            {
                Statics.Log(err.HandleError(TYPE.COFFEE, ERROR.INDEX, e));
                TempData["err_msg"] = err.HandleError(TYPE.COFFEE, ERROR.INDEX, e);
                TempData["ErrorMessage"] = "";
                return ErrorPage();
            }
            finally
            {
                try { /*db?.Dispose(); */access.UnQueue(); }
                catch (Exception e)
                {
                    string subject = "Fejl i finally!";
                    string body = Extensions.HtmlEncode("");
                    Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);
                }
            }
        }
        public IActionResult Donate()
        {
            try
            {
                access.Queue();
                ViewBag.Session = session;

                if (session.IsMobile == "none")
                    return View("IsMobile");

                return View();
            }
            catch (Exception e)
            {
                Statics.Log(err.HandleError(TYPE.COFFEE, ERROR.INDEX, e));
                TempData["err_msg"] = err.HandleError(TYPE.COFFEE, ERROR.INDEX, e);
                TempData["ErrorMessage"] = "";
                return ErrorPage();
            }
            finally
            {
                try { /*db?.Dispose(); */access.UnQueue(); }
                catch (Exception e)
                {
                    string subject = "Fejl i finally!";
                    string body = Extensions.HtmlEncode("");
                    Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);
                }
            }
        }
        public IActionResult Message(string name, string message)
        {
            try
            {
                access.Queue();
                ViewBag.Session = session;

                if (session.IsMobile == "none")
                    return View("IsMobile");

                if (string.IsNullOrEmpty(name))
                    return RedirectToAction("Index", "Coffee");
                if (string.IsNullOrEmpty(message))
                    return RedirectToAction("Index", "Coffee");
                if (!IsValidEntry(name, message))
                    return RedirectToAction("Index", "Coffee");

                using (dagligkaffedkContext db = new dagligkaffedkContext())
                {
                    db.Coffees.Add(new Coffee() { Author = name, Storie = message, Title ="", CreatedOn = DateTime.Now, Bean = 0 });
                    db.SaveChanges();
                }
                
                //db.Coffees.FirstOrDefault();
                return RedirectToAction("Index", "Coffee");
            }
            catch (Exception e)
            {
                Statics.Log(err.HandleError(TYPE.COFFEE, ERROR.INDEX, e));
                TempData["err_msg"] = err.HandleError(TYPE.COFFEE, ERROR.INDEX, e);
                TempData["ErrorMessage"] = "";
                return ErrorPage();
            }
            finally
            {
                try { /*db?.Dispose(); */access.UnQueue(); }
                catch (Exception e)
                {
                    string subject = "Fejl i finally!";
                    string body = Extensions.HtmlEncode("");
                    Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);
                }
            }
        }
        private bool NotValid(string name, string content)
        {
            try
            {
                string ip = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
                Stats stats_res = stats.GetStatistics(ip);

                string msg;
                if (ip == Settings.Basic.IP() || Check.Generel.IsAdmin(ip))
                    msg = "Gæstebog(Ugyldig)! - Admin[" + stats_res.users_per_day + "]";
                else
                    msg = "Gæstebog(Ugyldig)! - Other[" + stats_res.users_per_day + "]";

                string subject = msg;
                string body = "IP: " + stats_res.ip + "<br />" +
                            //"date: " + date + "<br />" +
                            "name: " + name + "<br />" +
                            //"country: " + country + "<br />" +
                            "content: " + content + "<br />";
                Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool IsValidEntry(string name, string content)
        {
            try
            {
                bool ok3 = true, ok4 = true;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(content))
                    return NotValid(name, content);
                if (name.Length > 50 || content.Length > 300)
                    return NotValid(name, content);

                //DateTime res_date;
                //CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                //DateTimeStyles styles = DateTimeStyles.None;

                //ok1 = DateTime.TryParse(date, culture, styles, out res_date);
                //StringHelper.OnlyAlphanumeric(country, true, true, "", new char[0], out ok2);
                StringHelper.OnlyAlphanumeric(name, true, true, "br", Characters.Name(), out ok3);
                StringHelper.OnlyAlphanumeric(content, true, true, "br", Characters.All(true), out ok4);
                if (!(ok3 && ok4))
                    return NotValid(name, content);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [HttpPost]
        public JsonResult AddBean(string index)
        {
            try
            {
                if (string.IsNullOrEmpty(index))
                    return Json(new { success = false });
                int res;
                bool ok = int.TryParse(index, out res);
                if(!ok)
                    return Json(new { success = false });

                int updated = 0;
                using (dagligkaffedkContext db = new dagligkaffedkContext())
                {
                    Coffee c = db.Coffees.Where(x => x.Id == res).FirstOrDefault();
                    if(c == null)
                        return Json(new { success = false });
                    int b = c.Bean + 1;
                    c.Bean = b;
                    updated = b;
                    db.SaveChanges();
                }

                return Json(new { success = true, updated = "" + updated });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult IsMobile(string width)
        {
            //SetupCurrentUser();

            try
            {
                access.Queue();

                int res;
                bool ok = int.TryParse(width, out res);
                if(!ok)
                    return Json(new { success = false });

                //if(!ThisSession.IsMobile)
                session.IsMobile = res < 768 ? "true" : "false";
                string _res = session.IsMobile == "true" ? "mobile" : "not mobile";
                return Json(new { success = true, mobile = _res });
            }
            catch (Exception e)
            {
                string err_msg = err.HandleError(TYPE.COFFEE, ERROR.ISMOBILE, e);
                TempData["err_msg"] = err_msg;
                return AjaxErrorReturn("err");
            }
            finally
            {
                try { /*db?.Dispose();*/ access.UnQueue(); }
                catch (Exception e)
                {
                    string subject = "Fejl i finally!";
                    string body = Extensions.HtmlEncode("");
                    Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);
                }
            }
        }

        private JsonResult AjaxErrorReturn(string user_msg)
        {
            //access.UnQueue();
            string err_msg = TempData["err_msg"] as string;
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string admin = Check.Generel.IsAdmin(ip) ? " (ADMIN)" : "";

            if (err_msg.Contains("A-OK, DBNull."))
                return Json(new { success = false, res = user_msg });

            string subject = "AjaxErrorReturn > Coffee " + admin;
            string body = err_msg;
            Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);

            return Json(new { success = false, res = user_msg });
        }

        private ActionResult ErrorPage()
        {
            //access.UnQueue();

            //SetupCurrentUser();
            //CurrentUser user = CurrentUser.GetInstance(httpcon);
            //dao_person current_user = null;// user.GetCurrentUser(false, true, true);
            //ViewBag.CurrentUser = current_user;
            //ViewBag.Session = session;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;

            //string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string admin = Check.Generel.IsAdmin(ip) ? " (ADMIN)" : "";
            string err_msg = TempData["err_msg"] as string;

            if (string.IsNullOrEmpty((string)ViewBag.ErrorMessage) && string.IsNullOrEmpty(err_msg))
                return NotFound(StatusCodes.Status404NotFound);

            if (err_msg.Contains("A-OK, DBNull."))
                return NotFound(StatusCodes.Status404NotFound);

            string subject = "ErrorPage > Centr " + admin;
            string body = err_msg;
            Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), subject, body);

            return View("ErrorPage");
        }
    }
}
