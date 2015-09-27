// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendService.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Moments;

    [Route("/[controller]")]
    public class FriendsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<User> Get(bool getFriends, string userId)
        {
            return new User[] { new User { Id = "1234", Name = "Esteban Solano G." } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}