// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    using Microsoft.AspNet.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("/[controller]")]
    public class UsersController : Controller
    {
        public UsersController()
        {
            Repository = new UserRepository();
        }

        public UserRepository Repository { get; private set; }

        public IEnumerable<User> Get()
        {
            var result = Repository.GetAll();
            if (result.Any() == false)
            {
                return new User[] { new User { Id = "-1", Name = "No users yet", SendMoment = false} };
            }
            return result;
        }

        [HttpPost]
        public ActionResult Insert(User model)
        {
            var result = Repository.Add(model);

            if (result)
            {
                return Created(Url.Link("User", 1), model);
            }
            return HttpBadRequest();
        }
    }
}