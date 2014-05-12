using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using NLog;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace EasyCronEnabler
{
    public partial class EasyCronEnabler : ServiceBase
    {
        private static Thread _thread;
        private RestClient _client;
        private static readonly string _token = ConfigurationManager.AppSettings["token"];
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public EasyCronEnabler()
        {
            InitializeComponent();
            _client = new RestClient("https://www.easycron.com/rest/");
            _client.AddHandler("text/html", new JsonSerializer());
        }

        protected override void OnStart(string[] args)
        {
            _thread = new Thread(LoopCheck);
            _thread.Start();
        }

        protected override void OnStop()
        {
            _thread.Abort();
        }

        internal void LoopCheck()
        {
            while (true)
            {
                var jobs = GetCronJobs();
                foreach (var job in jobs.Where(x => !x.Enabled))
                {
                    var enabled = Enable(job.cron_job_id);
                    if (enabled)
                        _logger.Info("Cron job {0} enabled", job.cron_job_name);
                    else
                        _logger.Error("Error when enabling cron job {0}", job.cron_job_name);
                }
                Thread.Sleep(1800000);
            }
        }

        private IEnumerable<CronJob> GetCronJobs()
        {
            var request = new RestRequest("list");
            request.AddParameter("token", _token);

            var response = _client.Get<Wrapper>(request);
            return response.Data.cron_jobs;
        }

        private bool Enable(int id)
        {
            var request = new RestRequest("enable");
            request.AddParameter("token", _token);
            request.AddParameter("id", id);
            var response = _client.Get<Wrapper>(request);
            return response.Data.status == "success";
        }


    }
}
