using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public class Session
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ISession _session;
        public Session(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //_session = _httpContextAccessor.HttpContext.Session;
        }

        public string IsMobile
        {
            get
            {
                ISession _session = _httpContextAccessor.HttpContext.Session;
                if (_session.GetString("mob") != null)
                    return (string)_session.GetString("mob");
                return "none";
            }
            set
            {
                ISession _session = _httpContextAccessor.HttpContext.Session;
                string s = "";
                if (!string.IsNullOrEmpty(value))
                    s = value;
                _session.SetString("mob", s);
            }
        }

        
        public bool Cookie
        {
            get
            {
                ISession _session = _httpContextAccessor.HttpContext.Session;
                if (_session.GetString("cookie") != null)
                    return (string)_session.GetString("cookie") == "true" ? true : false;
                return false;
            }
            set
            {
                ISession _session = _httpContextAccessor.HttpContext.Session;
                _session.SetString("cookie", value == true ? "true" : "false");
            }
        }        
    }
}
