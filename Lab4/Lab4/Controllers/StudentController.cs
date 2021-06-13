using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<List<students>> Get()
        {
            XmlStudent student = new XmlStudent();
            XmlGrade grades = new XmlGrade();
            List<student> studentsList = new List<student>();
            List<grade> oceny = new List<grade>();
            List<student> studenci = new List<student>();
            foreach(var x in student.getStudents())
            {
                studenci = x.student;
            }
            foreach(var x in grades.getGrades())
            {
                oceny = x.grade;
            }
            foreach(var x in studenci)
            {
                List<grade> ocenyStudenta = new List<grade>();
                foreach(var y in oceny)
                {
                    if(y.indexNumber == x.numerIndeksu)
                    {
                        ocenyStudenta.Add(y);
                    }
                }
                studentsList.Add(new student() { grades = ocenyStudenta, lastName = x.lastName, name = x.name, numerIndeksu = x.numerIndeksu });
            }

            return Ok(new students() { student = studentsList });
        }
        [HttpGet("{idSubject}/{indexNumber}")]
        public ActionResult<List<grade>> GetStudentSubjectGrade(int idSubject, int indexNumber)
        {
            List<grade> grades = new List<grade>();
            var oceny = from n in new XmlGrade().getGradesNew() where n.idSubject == idSubject && n.indexNumber == indexNumber select n;
            if (oceny.Count() < 1)
                return NotFound();
            
            foreach(var x in oceny)
            {
                grades.Add(x);
            }
            return Ok(grades);
        }
        [HttpGet("{idSubject}")]
        public ActionResult<List<grade>> GetStudentsGrades(int idSubject)
        {
            var oceny = from n in new XmlGrade().getGradesNew() where n.idSubject == idSubject select n;
            if (oceny.Count() < 1)
                return NotFound();

            return Ok(oceny);
        }
        [HttpPost]
        public ActionResult Post([FromBody]students studentsList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            XmlStudent xmlStudent = new XmlStudent();
            List<student> studenci = new List<student>();
            foreach(var x in xmlStudent.getStudents())
            {
                studenci = x.student;
            }
            foreach (student x in studentsList.student)
            {
                int ile = (from n in studenci where n.numerIndeksu == x.numerIndeksu select n).Count();
                if (ile > 0)
                    return BadRequest(new Information() { content = "Istnieje student o podanym indexie !", type = "Error" });
                else
                    if (xmlStudent.addStudent(x))
                        return Ok(x);

                return BadRequest(new Information() { content = "Nie udało się dodać studenta!", type = "Error" });
            }

            return Ok(studenci);
        }
    }
}
