using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanityArchiver;

namespace SanityArchiver
{
    public sealed class History
    {
        public List<string> history = new List<string>();
        public int timesGoingBack = -1;
        private static History singleton = null;
        private static readonly object padlock = new object();

        History()
        {
        }

        public static History Singleton
        {
            get
            {
                lock (padlock)
                {
                    if (singleton == null)
                    {
                        singleton = new History();
                    }
                    return singleton;
                }
            }
        }

        

        public void AddToHistory(string path)
        {
            history.Add(path);
            timesGoingBack = -1;
        }

        public void ClearHistory()
        {
            history.Clear();
        }

        public string GoBackInHistory()
        {
            try
            {
                string previousPath = history[history.Count() + timesGoingBack-1];
                timesGoingBack--;
                return previousPath;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "E:\\";
            }
        }
    }
}
