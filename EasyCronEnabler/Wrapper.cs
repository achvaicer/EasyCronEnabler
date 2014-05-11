using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyCronEnabler
{
    public class Wrapper
    {
        public string status { get; set; }
        public CronJob[] cron_jobs { get; set; }
    }
}
