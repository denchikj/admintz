using BillingAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BillingAdmin
{
    /// <summary>
    /// Сводное описание для ProxyHandler
    /// </summary>
    public class ProxyHandler : IHttpHandler
    {
		Entities db = new Entities();

		public void ProcessRequest(HttpContext context)
		{
            try
            {
				if (context.Request.QueryString["name"] == null)
				{
					return;
				}
				string idrecord = context.Request.QueryString["name"].Trim();
				Statistic_pbx statistic_pbx = db.Statistic_pbx.Where((Statistic_pbx c) => c.UNIQUEID.Contains(idrecord)).FirstOrDefault();
				if (idrecord.Contains("m_"))
				{
					string str = statistic_pbx.recordingfile.Split('/').Last();
					string address = "http://twintel.ru/records/" + str;
					WebClient webClient = new WebClient();
					MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(address));
					context.Response.ContentType = "audio/mpeg";
					if (context.Request.QueryString["attachment"] != null)
					{
						context.Response.AddHeader("Content-Disposition", "attachment;filename=record-" + statistic_pbx.src_num.Replace("+", "") + "-" + ((statistic_pbx.dst_num.Length > 11) ? statistic_pbx.dst_num.Remove(0, 2) : statistic_pbx.dst_num) + statistic_pbx.start.ToString() + ".wav");
						context.Response.ContentType = "application/force-dowload";
					}
					context.Response.BinaryWrite(memoryStream.ToArray());
					return;
				}
				if (idrecord.Contains('_'))
				{
					string address2 = "http://pbx.twintel.ru//pbxcore/api/cdr/playback?view=" + statistic_pbx.recordingfile;
					WebClient webClient2 = new WebClient();
					MemoryStream memoryStream2 = new MemoryStream(webClient2.DownloadData(address2));
					context.Response.ContentType = "audio/mpeg";
					if (context.Request.QueryString["attachment"] != null)
					{
						context.Response.AddHeader("Content-Disposition", "attachment;filename=record-" + statistic_pbx.src_num.Replace("+", "") + "-" + ((statistic_pbx.dst_num.Length > 11) ? statistic_pbx.dst_num.Remove(0, 2) : statistic_pbx.dst_num) + statistic_pbx.start.ToString() + ".wav");
						context.Response.ContentType = "application/force-dowload";
					}
					context.Response.BinaryWrite(memoryStream2.ToArray());
					return;
				}


				string str2 = ConfigurationManager.AppSettings["OktellServerUrl"].ToString();
				string address3 = str2 + "/execsvcscriptplain/?name=lkrecordbyid&startparam=%3Cparam1%3E" + idrecord + "%3C/param1%3E";
				string userName = ConfigurationManager.AppSettings["login"].ToString();
				string password = ConfigurationManager.AppSettings["passwordRecords"].ToString();
				WebClient webClient3 = new WebClient
				{
					Credentials = new NetworkCredential(userName, password)
				};
				string text = webClient3.DownloadString(address3).Trim();
				string address4 = str2 + "/download/files?path=" + text;
				MemoryStream memoryStream3 = new MemoryStream(webClient3.DownloadData(address4));
				context.Response.ContentType = "audio/mpeg";
				if (context.Request.QueryString["attachment"] != null)
				{
					context.Response.AddHeader("Content-Disposition", "attachment;filename=" + text.Replace("mix_", "").Split('\\').Last());
					context.Response.ContentType = "application/force-dowload";
				}
				context.Response.BinaryWrite(memoryStream3.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

		public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}