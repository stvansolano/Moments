// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    using Microsoft.AspNet.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    [Route("/[controller]")]
    public class UsersController : Controller
    {
        public UsersController()
        {
            Repository = new UserRepository();
        }

        public UserRepository Repository { get; private set; }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var result = await Repository.GetAll();
            if (result.Any() == false)
            {
                return new User[] { new User { Id = "-1", Name = "No users yet", SendMoment = false} };
            }
            return result;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] User model)
        {
            //var model = JsonConvert.DeserializeObject<User>(json);
            Repository.Add(model);

            Repository.Execute();

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}