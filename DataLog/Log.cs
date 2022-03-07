using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLog
{
    public sealed class Log
    {
        private static readonly Lazy<Log> lazy =
            new Lazy<Log>(() => new Log());

        public static Log Instance { get { return lazy.Value; } }

        private Log()
        {
        }

        public static void AddEntry(Exception ex)
        {

        }
    }
}
