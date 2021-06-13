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
    public partial class subjects
    {
        private List<subject> subjectField = new List<subject>();

        [System.Xml.Serialization.XmlElementAttribute("subject")]
        public List<subject> subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class subject
    {
        private int _idInstructor;
        public int id 
        {
            get;
            set;
        }
        public string name { get; set; }
        public int type { get; set; }
        public int idInstructor 
        {
            get
            {
                return this._idInstructor;
            } 
            set 
            {
                int czyIstnieje = (from n in new XmlInstructor().getInstructors() where n.id == value select n).Count();
                if (czyIstnieje < 1)
                    throw new Exception("Instruktor o takim ID nie istnieje !");
                else
                    this._idInstructor = value;
            } 
        }
    }
}
