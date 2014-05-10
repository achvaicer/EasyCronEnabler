using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyCronEnabler
{
    internal class CronJob
    {
        public int cron_job_id { get; set; }
        public string cron_job_name { get; set; }
        public int user_id { get; set; }
        public string url { get; set; }
        public string cron_expression { get; set; }
        public int number_failed_time { get; set; }
        public int engine_occupied { get; set; }
        public int log_output_length { get; set; }
        public int email_me { get; set; }
        public bool status { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
