// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    using Microsoft.AspNet.Mvc;
    using System.Linq;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    [Route("/[controller]")]
    public class MomentsController : Controller
    {
        protected MomentRepository Repository { get; private set; }
        protected CloudContext CloudContext { get; set; }

        public MomentsController(CloudContext context, MomentRepository repository)
        {
            Repository = repository;
            CloudContext = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetSentToMe(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return HttpBadRequest();
            }
            var result = await Repository.FindSentTo(userId);

            if (result.Any() == false)
            {
                result = new Moment[] { new Moment { Id = "No moments yet!" } };
            }
            return Json(result);
        }

        [HttpPost]
        public HttpStatusCodeResult Post([FromBody]MomentBody body)
        {
            var recipients = body.Recipients;

            if (body.IsValid() == false)
            {
                return HttpBadRequest();
            }
            if (body.ContainsAttachedContent)
            {
                body.Url = StoreImageBlob(body.Attached);
            }

            var validRecipients = body.SanitizeRecipients();
            if (validRecipients.Any() == false)
            {
                return HttpBadRequest();
            }
            foreach (var user in validRecipients)
            {
                var moment = new Moment
                {
                    MomentUrl = body.Url ?? string.Empty,
                    SenderUserId = body.SenderId,
                    //SenderName = body.SenderName,
                    SenderProfileImage = body.SenderProfileImage ?? string.Empty,
                    RecipientUserId = user,
                    DisplayTime = body.DisplayTime,
                    TimeSent = DateTime.Now
                };

                Repository.Add(moment);
            }
            Repository.Commit();

            return new HttpStatusCodeResult((int)HttpStatusCode.Created);
        }

        private string StoreImageBlob(string attached)
        {
            return string.Empty;
        }
    }
}