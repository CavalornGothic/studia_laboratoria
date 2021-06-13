using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Controllers
{
    [Route("api/instructor")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<List<instructor>> Get()
        {
            return new XmlInstructor().getInstructors();
        }
        [HttpPost]
        public ActionResult Post([FromBody]instructors instructorList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Information() { content = "Błędnie podany typ danych !", type = "Error" });
            }
            XmlInstructor xmlInstructor = new XmlInstructor();
            try
            {
                foreach(var x in instructorList.instructor)
                {
                    int count = (from n in xmlInstructor.getInstructors() where n.id == x.id select n).Count();
                    if (count < 1)
                    {
                        if (xmlInstructor.addInstructor(x))
                            continue;
                    } 
                    else
                        return BadRequest(new Information() { content = "Istnieje wykładowca o podanym ID !", type = "Error" });
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new Information() { content = "Nie udało się dodać wykładowcy!", type = "Error" });
            }
        }
    }
}
