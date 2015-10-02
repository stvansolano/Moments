// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using System.Linq;

    [Route("/[controller]")]
    public class MomentsController : Controller
    {
        public MomentsController()
        {
            Repository = new MomentRepository();
        }

        public MomentRepository Repository { get; private set; }

        public IEnumerable<Moment> Get(string userId)
        {
            var result = Repository.Find(moment => moment.SenderUserId == userId);

            if (result.Any() == false)
            {
                return new Moment[] { new Moment { Id = "No receivers yet" } };
            }
            return result;
        }
    }
}