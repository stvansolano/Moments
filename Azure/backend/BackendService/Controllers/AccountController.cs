// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendService.Controllers
{
    using Microsoft.AspNet.Mvc;

    [RequireHttps]
    [Route("/[controller]")]
    public class AccountController : Controller
    {
        [HttpPost]
        public bool Login()
        {
            return true;
        }

        // GET api/values/5
        [HttpPost]
        public void SignOut()
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return true;
        }
    }
}