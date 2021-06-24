using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BillingAdmin.Models.Site
{
    public class FormFeedbackRequests
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public IEnumerable<HttpPostedFileBase> File { get; set; }

        static private Entities db = new Entities();
        static public async Task SaveRequest(FormFeedbackRequests formFeedbackRequests)
        {
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    //await Save(GetFeedbackRequest(formFeedbackRequests));
                    //transact.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    var err = ex.EntityValidationErrors.Select(e => e.ValidationErrors);
                    transact.Rollback();
                }
            }
        }

        //static private FeedbackRequest GetFeedbackRequest(FormFeedbackRequests formFeedbackRequests)
        //{
        //    return new FeedbackRequest {
        //        Id = Guid.NewGuid(),
        //        Name = formFeedbackRequests.Name,
        //        Phone = formFeedbackRequests.Phone,
        //        Email = formFeedbackRequests.Email,
        //        Comment = formFeedbackRequests.Comment,
        //        DateRequest = DateTime.Now,
        //        IsSent = false
        //    };
        //}
        //static private async Task Save(FeedbackRequest feedbackRequest)
        //{
        //    //db.FeedbackRequests.Add(feedbackRequest);
        //    //await db.SaveChangesAsync();
        //}
    }
}