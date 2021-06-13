using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Controllers
{
    [Route("api/grade")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<List<grades>> Get()
        {
            XmlGrade xmlGrade = new XmlGrade();
            return xmlGrade.getGrades();
        }
        [HttpGet("subject/{id}")]
        public ActionResult<List<grade>> GetSubjectGrades(int id)
        {
            var oceny = from n in new XmlGrade().getGradesNew() where n.idSubject == id select n;
            if (oceny.Count() < 1)
                return NotFound();

            return Ok(oceny);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            XmlGrade xmlGrade = new XmlGrade();
            try
            {
                if(xmlGrade.deleteGrade(id))
                    return Ok();
                else
                    return StatusCode(400);
            }
            catch
            {
                return StatusCode(400);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromForm] string description)
        {
            try
            {
                XmlGrade xmlGrade = new XmlGrade();
                int czyIstnieje = (from n in xmlGrade.getGradesNew() where n.id == id select n).Count();
                if (czyIstnieje < 1)
                    return BadRequest("Nie ma oceny o takim ID!");

                if (xmlGrade.updateDescription(id, description))
                    return Ok("Zaktualizowano zasób");
                else
                    return BadRequest("Nie udało się zaktualizować zasobu!");
            }
            catch
            {
                return BadRequest("Nie udało się zaktualizować zasobu!");
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody]grades gradeData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Information() { content = "Błędnie podany typ danych !", type = "Error" });
            }
            XmlGrade xmlGrade = new XmlGrade();
            try
            {
                foreach (var x in gradeData.grade)
                {
                    int count = (from n in xmlGrade.getGradesNew() where n.id == x.id select n).Count();
                    if (count < 1)
                    {
                        if (xmlGrade.addGrade(x))
                            continue;
                    }
                    else
                        return BadRequest(new Information() { content = "Istnieje ocena o podanym ID !", type = "Error" });
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new Information() { content = "Nie udało się dodać oceny!", type = "Error" });
            }
        }
    }
}
