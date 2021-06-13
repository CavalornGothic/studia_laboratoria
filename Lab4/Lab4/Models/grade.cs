using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class grades
    {
        private List<grade> gradeField = new List<grade>();
        [System.Xml.Serialization.XmlElementAttribute("grade")]
        public List<grade> grade
        {
            get { return this.gradeField; }
            set { this.gradeField = value; }
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class grade
    {
        private int _indexNumber;
        private int _idSubject;
        public int id { get; set; }
        public int indexNumber 
        { 
            get
            {
                return this._indexNumber;
            }
            set
            {
                int czyIstnieje = (from n in new XmlStudent().getStudentsNew() where n.numerIndeksu == value select n).Count();
                if (czyIstnieje < 1)
                    throw new Exception("Nie ma takiego studenta o podanym indeksie!");
                else
                    this._indexNumber = value;
            }
        }
        public int idSubject 
        { 
            get
            {
                return this._idSubject;
            }
            set
            {
                int czyIstnieje = (from n in new XmlSubject().getSubjects() where n.id == value select n).Count();
                if (czyIstnieje < 1)
                    throw new Exception("Nie ma przedmiotu o takim ID!");
                else
                    this._idSubject = value;
            }
        }
        public double score { get; set; }
        public DateTime scoreDate { get; set; }
        public string description { get; set; }
    }
}
