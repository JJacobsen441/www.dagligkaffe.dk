using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public class Elem
    {
        private int counter = 400;
        private bool first = true;
        public string Guid { get; set; }
        public string Place { get; set; }
        private Elem()
        {
        }
        public Elem(string place, string guid)
        {
            Guid = guid;
            Place = place;
        }
        public async void Wait()
        {
            Line l = Access.GetLine(this.Place);

            if (l == null)//burde ikke kunne ske!
            {
                Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), "Queue > line NULL", "");
                throw new Exception();
            }
            if (counter <= 0)
            {
                l?.GetThrough(this);
                return;// throw new Exception();
            }
            if (!l.IsInLine(this))
                l.PutInLine(this);

            if (!l.IsFirst(this.Guid))
            {
                await Task.Delay(500);
                if (first)
                    Admin.Notification.Run(Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), Settings.Basic.EMAIL_MAIL(), "Queue > " + this.Guid, "");
                first = false;
                counter--;
                Wait();
            }
        }
        public void Pass()
        {
            Line l = Access.GetLine(this.Place);
            l?.GetThrough(this);
        }
    }
    public class Line
    {
        public DateTime last;
        public List<string> q = new List<string>();
        public bool IsFirst(string guid)
        {
            if (q.Count == 0 || q[0] == guid)
                return true;
            return false;
        }
        public void PutInLine(Elem elem)
        {
            Reset();
            q.Add(elem.Guid);
            last = DateTime.Now;
        }
        public void GetThrough(Elem elem)
        {
            if (q.Contains(elem.Guid))
                q.Remove(elem.Guid);
        }
        private void Reset()
        {
            if (last == DateTime.MinValue)
                return;
            DateTime now = DateTime.Now;
            if (now.Subtract(last).TotalSeconds > 30.0f)
                q = new List<string>();
        }
        public bool IsInLine(Elem elem)
        {
            return q.Contains(elem.Guid);
        }
        public bool IsEmpty()
        {
            return q.Count == 0;
        }
    }
    public class Access
    {
        public static Dictionary<string, Line> qs = new Dictionary<string, Line>();
        public Elem e;
        private Access() { }
        public Access(string place, string guid)
        {
            e = new Elem(place, guid);
            if (!qs.ContainsKey(place))
                qs.Add(place, new Line());
        }

        public void Queue() =>
            e.Wait();// Line.PutInLine(e);

        public void UnQueue()
        {
            e.Pass();// Line.GetThrough(e);
            Line l = GetLine(e.Place);
            if (l != null)
            {
                if (l.IsEmpty())
                    qs.Remove(e.Place);
            }
        }

        public static Line GetLine(string place)
        {
            if (!qs.ContainsKey(place))
                qs[place] = new Line();
            return qs[place];
            //return null;
        }
    }
}
