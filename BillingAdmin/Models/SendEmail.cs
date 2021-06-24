// BillingClient.App_Services.SendEmail
using BillingAdmin.Models;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using SendPulse.API;
using SendPulse.API.Models;
using System.Collections.Generic;
using System.Collections;

public static class SendEmail
{
    private static string EmailFrom = "mail@100tz.ru";
    private static string EmailFromPassword = "osh83f{W";

    //static string  tgPrefix = ConfigurationManager.AppSettings["TELEGRAMM-PREFIX"].ToString();
    //static string  tgSuffix = ConfigurationManager.AppSettings["TELEGRAMM-SUFFIX"].ToString();

    //private static SmtpClient smtpClient = new SmtpClient("smtp.masterhost.ru", 25);
    //private static SmtpClient smtpClient = new SmtpClient("smtp.yandex.ru", 587) {
    //    EnableSsl = true, 
    //    DeliveryMethod = SmtpDeliveryMethod.Network, Timeout=1000 
    //};
    private static SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClients"].ToString(), int.Parse(ConfigurationManager.AppSettings["SmtpPort"]))
    {
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        Timeout = 1000 * 50,
        Credentials = new NetworkCredential(EmailFrom, EmailFromPassword)
    };

    private static void smtpSend(MailMessage mail)
    {
        try
        {
            smtpClient.Send(mail);
        }
        catch
        {
            smtpClient = new SmtpClient("smtp.mail.ru", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 1000 * 50,
                Credentials = new NetworkCredential(EmailFrom, EmailFromPassword)
            };

            try
            {
                smtpClient.Send(mail);
            }
            catch
            {
                smtpClient = new SmtpClient("smtp.yandex.ru", 25)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 1000 * 50,
                    Credentials = new NetworkCredential(EmailFrom, EmailFromPassword)
                };

                smtpClient.Send(mail);
            }
        }
    }

    static Entities db = new Entities();

    //public static void SendFeedbackRequests(FormFeedbackRequests formFeedbackRequests, HttpContextBase context)
    //{
    //    MailMessage mail = new MailMessage();
    //    mail.Headers.Add("Precedence", "bulk");
    //    mail.From = new MailAddress(EmailFrom, "100ТЗ", Encoding.UTF8);
    //    mail.To.Add(new MailAddress(EmailFrom,null, Encoding.UTF8));
    //    mail.Subject = "Запрос с сайта";
    //    mail.Body = "ФИО:" + formFeedbackRequests.Name + Environment.NewLine + "email:" + formFeedbackRequests.Email + Environment.NewLine + "Телефон:" + formFeedbackRequests.Phone + Environment.NewLine + "Комментарий:" + formFeedbackRequests.Comment + Environment.NewLine;

    //    foreach (var currentFile in formFeedbackRequests.File)
    //    {

    //        mail.Attachments.Add(new Attachment(currentFile.InputStream, currentFile.FileName));
    //    }

    //    smtpSend(mail);
    //    //var t = Task.Run(() =>
    //    //{
    //    //    try
    //    //    {
    //    //        smtpClient.SendAsync(mail, null);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {

    //    //        // throw;
    //    //    }
    //    //}
    //    //   );
    //    SendAnswering(formFeedbackRequests.Email, context);
    //}

    //private static void SendAnswering(string email, HttpContextBase context)
    //{
    //    smtpClient.Credentials = new NetworkCredential(EmailFrom, EmailFromPassword);
    //    MailMessage mail = new MailMessage();
    //    mail.Headers.Add("Precedence", "bulk");

    //    mail.From = new MailAddress(EmailFrom, "100ТЗ", Encoding.UTF8);
    //    mail.To.Add(new MailAddress(email, null, Encoding.UTF8));
    //    mail.Subject = "100ТЗ.ru: Вами оставлена заявка на сайте";
    //    mail.IsBodyHtml = true;
    //    string path = context.Server.MapPath("~/Email_Shablon/Answering.html");
    //    mail.Body = File.ReadAllText(path);
    //    //var t = Task.Run(() =>
    //    //{
    //    //    try
    //    //    {
    //    //        smtpClient.SendAsync(mail, null);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {

    //    //        // throw;
    //    //    }
    //    //}
    //    //   );
    //    smtpSend(mail);
    //}

    //public static async Task SendLetters(string email, HttpContextBase context, string contactName, long accountId, string password, int type, bool isValid = true)
    //{      

        
    //    string SystemMode = ConfigurationManager.AppSettings["SystemMode"].ToString();
        
    //    if (SystemMode == "TEST")
    //    {
    //        email = ConfigurationManager.AppSettings["testEmail"].ToString();            
    //    }

    //    string Subject = "";


    //    if (type == 3 && contactName != "Уважаемый партнер")
    //    {
    //        string contactNameMidldle = contactName.Substring(contactName.IndexOf(' '), contactName.Length - contactName.Substring(0, contactName.IndexOf(' ')).Length);
    //        string[] fio = contactName.Split(' ');
    //        string contactNameShort = fio[0] + " " + fio[1].Substring(0, 1) + "." + fio[2].Substring(0, 1) + ".";
            
    //        Subject = "Получатель: " + contactNameShort + " НМЦК по ГПЗ на zakupki.gov";
    //        contactName = contactNameMidldle;

    //       // string path2 = context.Server.MapPath("~/Email_Shablon/Priglashenie2.html");

    //    }
    //    else if (type == 4 && contactName != "Уважаемый партнер")
    //    {
    //        Subject = "Приглашение Поставщика на платформу прямых контрактов 100TZ.ru";
    //        //string path2 = context.Server.MapPath("~/Email_Shablon/Priglashenie1.html");
    //    }
    //    else
    //    {
    //        Subject = "Приглашение Поставщика на платформу прямых контрактов 100TZ.ru";
    //        //string path = context.Server.MapPath("~/Email_Shablon/Priglashenie1.html");           
    //    }

           

    //    if (isValid)
    //    {

    //        using (var sendPulse = new SendPulseService(ConfigurationManager.AppSettings["clientID"].ToString(), ConfigurationManager.AppSettings["clientSecret"].ToString()))
    //        {
    //            var result = await sendPulse.SendEmailAsync(new TemplateEmailData
    //            {
    //                Subject = "Новый заказ на платформе прямых контрактов 100ТЗ",
    //                From = new EmailAddress
    //                {
    //                    Name = "100 ТЗ Платформа прямых контрактов",
    //                    Address = "mail@100tz.ru"
    //                },
    //                To = new List<EmailAddress>
    //                {
    //                    new EmailAddress
    //                    {
    //                        Name = contactName,
    //                        Address = email.Trim()
    //                    }
    //                },
    //                TemplateID = "300156",
    //                TemplateVariables = new Dictionary<string, string>
    //                {
    //                    { "CONTACTNAME", contactName },
    //                    { "EMAIL", email },
    //                    { "PASSWORD", password },
    //                    { "unsubscribe_url", "<a href=\"{{unsubscribe}}\">Отписаться</a>" }
    //                }
    //            }); ;

                
                

    //            if (!result.ContainsKey("error"))
    //            {

    //                var id = result.GetValue("id").ToString();

    //                Logs log = new Logs
    //                {
    //                    accountId = accountId,
    //                    content = "Отправлено приглашение на почту " + email,
    //                    logType = 2,
    //                    date = DateTime.Now
    //                };

    //                db.Logs.Add(log);

    //                db.Logs_Messages.Add(new Logs_Messages()
    //                {
    //                    Logs = log,
    //                    messageId = id,
    //                    messagesCount = 1,
    //                    id = Guid.NewGuid()
    //                });

    //                db.SaveChanges();
    //            }
    //        }

    //    }


    //}

    public static async Task SendPassword(string email, HttpContextBase context, string contactName, long accountId, string password, bool isValid = true)
    { 
       
        string SystemMode = ConfigurationManager.AppSettings["SystemMode"].ToString();
        if (SystemMode == "TEST")
        {
            email = ConfigurationManager.AppSettings["testEmail"].ToString();
           
        }


        string Subject = "Доступ к платформе прямых контрактов 100ТЗ";
                     

        if (isValid)
        {

            using (var sendPulse = new SendPulseService(ConfigurationManager.AppSettings["clientID"].ToString(), ConfigurationManager.AppSettings["clientSecret"].ToString()))
            {
                var result = await sendPulse.SendEmailAsync(new TemplateEmailData
                {
                    Subject = Subject,
                    From = new EmailAddress
                    {
                        Name = "100 ТЗ Платформа прямых контрактов",
                        Address = "mail@100tz.ru"
                    },
                    To = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Name = contactName,
                            Address = email.Trim()
                        }
                    },
                    TemplateID = "300150",
                    TemplateVariables = new Dictionary<string, string>
                    {
                        { "CONTACTNAME", contactName },
                        { "EMAIL", email },
                        { "PASSWORD", password },
                        { "unsubscribe_url", "<a href=\"{{unsubscribe}}\">Отписаться</a>" }
                    }
                }); ;

                              

                if (!result.ContainsKey("error"))
                {

                    var id = result.GetValue("id").ToString();

                    //Logs log = new Logs
                    //{
                    //    accountId = accountId,
                    //    content = "Отправлены доступы на почту " + email + " " + contactName,
                    //    logType = 3,
                    //    date = DateTime.Now
                    //};

                    //db.Logs.Add(log);

                    //db.Logs_Messages.Add(new Logs_Messages()
                    //{
                    //    Logs = log,
                    //    messageId = id,
                    //    messagesCount = 1,
                    //    id = Guid.NewGuid()
                    //});

                    db.SaveChanges();
                }
            }

        }
    }

    //public static async Task SendNewsByButton(string email, HttpContextBase context, string contactName, long accountId, string contentHTML, string contentTEXT, bool isValid, string file="")
    //{
        

    //    string SystemMode = ConfigurationManager.AppSettings["SystemMode"].ToString();
    //    if (SystemMode == "TEST")
    //    {
    //        email = ConfigurationManager.AppSettings["testEmail"].ToString();
    //    }


    //    if (SystemMode != "TEST")
    //    {
    //        //  email = "odan@bk.ru";

    //        smtpClient.Credentials = new NetworkCredential(EmailFrom, EmailFromPassword);
    //        smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;

    //        MailMessage mail = new MailMessage();
    //        mail.Headers.Add("Precedence", "bulk");

    //        mail.From = new MailAddress(EmailFrom, "100ТЗ Платформа прямых контрактов", Encoding.UTF8);
    //        // email = "odanis@yandex.ru";
    //        // mail.To.Add(new MailAddress(email, null, Encoding.UTF8));

    //        Entities db = new Entities();
    //        var prj = db.Projects.Where(c => c.accountId == accountId);

    //        if (prj.Count() > 0)
    //        {
    //            var prjId = prj.FirstOrDefault().id;
    //            var services = db.Projects_Services.Where(c => c.projectId == prjId && c.serviceId == 34);

    //            if (services.Count() > 0)
    //            {
    //                foreach (var item in services)
    //                {
    //                    var chatId = item.Services_Data.Where(c => c.preferenceId == 12).FirstOrDefault().preferenceValue.Trim();
    //                    //chatId = chatId.Replace("@etlgr.com", "").Trim();

    //                    if (!chatId.ToString().All(Char.IsLetter))
    //                    {
    //                        mail.To.Add(new MailAddress(tgPrefix + chatId.ToString() + "@" + tgSuffix, null, Encoding.UTF8));
    //                    }

    //                }


    //            }

    //            if (mail.To.Count() > 0)
    //            {
    //                smtpClient.Send(mail);
    //            }
    //        }
    //    }

    //        if (isValid)
    //        {


    //            using (var sendPulse = new SendPulseService(ConfigurationManager.AppSettings["clientID"].ToString(), ConfigurationManager.AppSettings["clientSecret"].ToString()))
    //            {
    //                var result = await sendPulse.SendEmailAsync(new TemplateEmailData
    //                {
    //                    Subject = "Новый заказ на платформе прямых контрактов 100ТЗ",
    //                    From = new EmailAddress
    //                    {
    //                        Name = "100 ТЗ Платформа прямых контрактов",
    //                        Address = "mail@100tz.ru"
    //                    }, 
    //                    To = new List<EmailAddress>
    //                    {
    //                        new EmailAddress
    //                        {
    //                            Name = contactName,
    //                            Address = email.Trim()
    //                        }
    //                    },
    //                    TemplateID = "257249",
    //                    TemplateVariables = new Dictionary<string, string>
    //                    {
    //                        { "LINKXLS",  "<a href=\""+file+"\">Скачать запрос на поставку товаров в формате Excel</a>" },
    //                        { "POSITIONS", contentHTML },
    //                        { "unsubscribe_url", "<a href=\"{{unsubscribe}}\">Отписаться</a>" }
    //                    }
    //                }); ;

                    
    //                db.SaveChanges();

    //                if (!result.ContainsKey("error"))
    //                {

    //                    var id = result.GetValue("id").ToString();

    //                    Logs log = new Logs
    //                    {
    //                        accountId = accountId,
    //                        date = DateTime.Now,
    //                        content = "Перечень новых позиций отправлен для " + contactName + " на: " + email + "\r\n " + contentTEXT,
    //                        logType = 4
    //                    };                       

    //                    db.Logs.Add(log);

    //                    db.Logs_Messages.Add(new Logs_Messages()
    //                    {
    //                        Logs = log,
    //                        messageId = id,
    //                        messagesCount = 1,
    //                        id=Guid.NewGuid()
    //                    });

    //                    db.SaveChanges();
    //                }

    //            }

    //        }
    //        else
    //        {
    //            db.Logs.Add(new Logs
    //            {
    //                accountId = accountId,
    //                date = DateTime.Now,
    //                content = "Перечень новых позиций отправлен для " + db.Accounts.Find(accountId).FullLegalName + "В личный кабинет. Емайл " + email + " не подтвержден. \r\n " + contentTEXT,
    //                logType = 4
    //            });
    //            db.SaveChanges();
    //        }

        
    //}
    //public static void SendLog(string email, string text)
    //{
    //    string SystemMode = ConfigurationManager.AppSettings["SystemMode"].ToString();

    //    if (email == "")
    //    {
    //        if (SystemMode == "TEST")
    //        {
    //            email = ConfigurationManager.AppSettings["TELEGRAMM-EMAIL-TEST"].ToString();
    //        }
    //        else
    //        {
    //            email = ConfigurationManager.AppSettings["TELEGRAMM-EMAIL"].ToString();
    //        }
    //    }

    //    smtpClient.Credentials = new NetworkCredential(EmailFrom, EmailFromPassword);
    //    MailMessage mail = new MailMessage();
    //    mail.Headers.Add("Precedence", "bulk");

    //    mail.From = new MailAddress(EmailFrom, "100ТЗ", Encoding.UTF8);
    //    mail.To.Add(new MailAddress(email, null, Encoding.UTF8));
    //    mail.Subject = "Лог";
    //    mail.IsBodyHtml = false;
    //    mail.Body = text;
    //    var t = Task.Run(() =>
    //    {
    //        try
    //        {
    //            smtpClient.SendAsync(mail, null);
    //        }
    //        catch (Exception ex)
    //        {

    //            // throw;
    //        }
    //    }
    //       );

    //   // smtpClient.Send(mail);

    //}
    //public static async Task SendLetter(string email, HttpContextBase context, string contactName, long accountId, string password, int type, bool isValid=true)
    //{
       
    //    string SystemMode = ConfigurationManager.AppSettings["SystemMode"].ToString();
    //    if (SystemMode == "TEST")
    //    {
    //        email = ConfigurationManager.AppSettings["testEmail"].ToString();         
    //    }

        

    //    if (type == 4)
    //    {
    //        string Subject = "Пояснения по работе платформы прямых контрактов 100TZ.ru";          

    //        if (isValid)
    //        {

    //            using (var sendPulse = new SendPulseService(ConfigurationManager.AppSettings["clientID"].ToString(), ConfigurationManager.AppSettings["clientSecret"].ToString()))
    //            {
    //                var result = await sendPulse.SendEmailAsync(new TemplateEmailData
    //                {
    //                    Subject = Subject,
    //                    From = new EmailAddress
    //                    {
    //                        Name = "100 ТЗ Платформа прямых контрактов",
    //                        Address = "mail@100tz.ru"
    //                    },
    //                    To = new List<EmailAddress>
    //                {
    //                    new EmailAddress
    //                    {
    //                        Name = contactName,
    //                        Address = email.Trim()
    //                    }
    //                },
    //                    TemplateID = "300160",
    //                    TemplateVariables = new Dictionary<string, string>
    //                {
    //                    { "CONTACTNAME", contactName },                        
    //                    { "unsubscribe_url", "<a href=\"{{unsubscribe}}\">Отписаться</a>" }
    //                }
    //                }); ;

                   

    //                if (!result.ContainsKey("error"))
    //                {

    //                    var id = result.GetValue("id").ToString();

    //                    Logs log = new Logs
    //                    {
    //                        accountId = accountId,
    //                        content = "Отправлено письмо №2 на почту " + email,
    //                        logType = 2,
    //                        date = DateTime.Now
    //                    };

    //                    db.Logs.Add(log);

    //                    db.Logs_Messages.Add(new Logs_Messages()
    //                    {
    //                        Logs = log,
    //                        messageId = id,
    //                        messagesCount = 1,
    //                        id = Guid.NewGuid()
    //                    });

    //                    db.SaveChanges();
    //                }
    //            }

    //        }
    //    }
      
    //}

    //public static async Task SendNews(List<EmailAddress> emails, string content, long newsId, bool isValid = true)
    //{

    //    string SystemMode = ConfigurationManager.AppSettings["SystemMode"].ToString();
    //    if (SystemMode == "TEST")
    //    {
    //        emails = new List<EmailAddress>() {
    //            new EmailAddress(){
    //                 Address = ConfigurationManager.AppSettings["testEmail"].ToString()
    //            }
    //        }; 
    //    }

        
    //        string Subject = "Новости платформы прямых контрактов 100TZ.ru";

    //        if (isValid)
    //        {

    //            using (var sendPulse = new SendPulseService(ConfigurationManager.AppSettings["clientID"].ToString(), ConfigurationManager.AppSettings["clientSecret"].ToString()))
    //            {
                
    //                var result = await sendPulse.SendEmailAsync(new TemplateEmailData
    //                {
    //                    Subject = Subject,
    //                    From = new EmailAddress
    //                    {
    //                        Name = "100 ТЗ Платформа прямых контрактов",
    //                        Address = "mail@100tz.ru"
    //                    },
    //                    To = emails,
    //                    TemplateID = "300324",
    //                    TemplateVariables = new Dictionary<string, string>
    //                {
    //                    { "CONTENT", content },
    //                    { "unsubscribe_url", "<a href=\"{{unsubscribe}}\">Отписаться</a>" }
    //                }
    //                });

                
    //            if (!result.ContainsKey("error"))
    //            {

    //                var id = result.GetValue("id").ToString();

    //                Logs log = new Logs
    //                {
    //                    accountId = 0,
    //                    content = "Новость " + newsId + " отправлена по адресам " + string.Concat(emails.Select(c => (c.Address + ";")).ToArray()),
    //                    logType = 2,
    //                    date = DateTime.Now
    //                };

    //                db.Logs.Add(log);

    //                db.Logs_Messages.Add(new Logs_Messages()
    //                {
    //                    Logs = log,
    //                    messageId = id,
    //                    messagesCount = emails.Count(),
    //                    id = Guid.NewGuid()
    //                });

    //                db.SaveChanges(); 
    //            }

    //            }   

    //    }


    //}
  
}
