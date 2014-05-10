using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyCronEnabler
{
    internal class Wrapper
    {
        internal string status { get; set; }
        internal CronJob[] cron_jobs { get; set; }
    }
}
