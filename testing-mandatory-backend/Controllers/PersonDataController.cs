using Microsoft.AspNetCore.Mvc;
using testing_mandatory_backend.Models;
using testing_mandatory_backend.Services;

namespace testing_mandatory_backend.Controllers
{
    [ApiController]
    public class PersonDataController : ControllerBase
    {
        private readonly IPersonDataService _personDataService;
        public PersonDataController(IPersonDataService personDataService)
        {
            _personDataService = personDataService;
        }

        [Route("api/name-gender")]
        [HttpGet]
        public ActionResult<NameAndGender> GetNameAndGender()
        {
            try
            {
                var nameAndGender = _personDataService.CreateNameAndGender();
                return Ok(nameAndGender); 
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, "Controller: Could not create name and gender.");
            }
        }


        [Route("api/name-gender-dob")]
        [HttpGet]
        public ActionResult<NameGenderAndBirthDate> GetNameAndGenderAndBirthDate()
        {
            try
            {
                var nameAndGenderAndBirthDate = _personDataService.CreateNameAndGenderAndBirthDate();
                return Ok(nameAndGenderAndBirthDate); 
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, "Controller: Could not create name, gender and birhdate.");
            }
        }


        [Route("api/address")]
        [HttpGet]
        public ActionResult<FakeAddress> GetAddress()
        {
            try
            {
                var address = _personDataService.CreateAddress();
                return Ok(address); 
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, "Controller: Could not create Address.");
            }
        }

        [Route("api/phone")]
        [HttpGet]
        public ActionResult GetPhoneNumber()
        {
            try
            {
                var phone = _personDataService.CreatePhoneNumber();
                return Ok(new {phoneNumber = phone}); 
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, "Controller: Could not create Phone Number.");
            }
        }

        
        [Route("api/person")]
        [HttpGet]
        public ActionResult <PersonData> GetPersonData()
        {
            try
            {
                var PersonData = _personDataService.CreatePersonData();
                return Ok(PersonData); 
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, "Controller: Could not create Person.");
            }
        }

        
    
        [Route("api/persons")]
        [HttpGet]
        public ActionResult<List<PersonData>> GetBulkPersonData([FromQuery] string n)
        {
            try
            {
                if (string.IsNullOrEmpty(n) || !int.TryParse(n, out int amount))
                {
                    return BadRequest("Invalid input. Please provide a valid number.");
                }

                if (amount < 2 || amount > 100)
                {
                    return UnprocessableEntity("The number of persons must be between 2 and 100.");
                }

                List<PersonData> personData = _personDataService.GenerateBulkPersonData(amount);
                
                return Ok(personData); 
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                return StatusCode(StatusCodes.Status500InternalServerError, "Controller: Could not create Bulk Person data.");
            }
        }

        
    }

}