using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<List<subject>> Get()
        {
            return new XmlSubject().getSubjects();
        }
        [HttpGet("{id}")]
        public ActionResult<List<subject>> GetByID(int id)
        {
            var przedmioty = from n in new XmlSubject().getSubjects() where n.idInstructor == id select n;
            if (przedmioty.Count() < 1)
                return NotFound();
            else
                return Ok(przedmioty);
        }

        // odnjadowanie tylko tych przedmiotów dla których jest wpisana ocena
        [HttpGet("exists")]
        public ActionResult<List<subject>> GetExists()
        {
            var listaPrzedmiotow = new XmlSubject().getSubjects();
            List<subject> przedmioty = new List<subject>();
            foreach(var x in new XmlGrade().getGradesNew())
            {
                var przedmioty2 = from n in listaPrzedmiotow where n.id == x.idSubject select n;
                foreach(var y in przedmioty2)
                {
                    przedmioty.Add(y);
                }
            }

            if (przedmioty.Count < 1)
                return NotFound();
            else
                return Ok(przedmioty);
        }
        [HttpPost]
        public ActionResult Post([FromBody]subjects subjectsList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Information() { content = "Błędnie podany typ danych !", type = "Error" });
            }
            XmlSubject xmlSubject = new XmlSubject();
            try
            {
                foreach(var x in subjectsList.subject)
                {
                    int count = (from n in xmlSubject.getSubjects() where n.id == x.id select n).Count();
                    if (count < 1)
                    {
                        if (xmlSubject.addSubject(x))
                            continue;
                    } 
                    else
                        return BadRequest(new Information() { content = "Istnieje przedmiot o podanym ID !", type = "Error" });
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new Information() { content = "Nie udało się dodać przedmiotu!", type = "Error" });
            }
        }
    }
}
