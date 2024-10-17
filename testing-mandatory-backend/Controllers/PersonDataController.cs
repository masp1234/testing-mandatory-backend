using Microsoft.AspNetCore.Mvc;

namespace testing_mandatory_backend.Controllers
{
    [ApiController]
    [Route("api/person-data")]
    public class PersonDataController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {

            
            return ("person data");
        }

    }

    [ApiController]
    [Route("api/cpr/")]
    public class CprController : ControllerBase
    {
        [HttpGet("{gender}")]
        public string Get(string gender)
        {

            
            return ("cpr: " + gender);
        }

    }
}