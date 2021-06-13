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
    public partial class students
    {
        private List<student> studentField = new List<student>();

        [System.Xml.Serialization.XmlElementAttribute("student")]
        public List<student> student
        {
            get
            {
                return this.studentField;
            }
            set
            {
                this.studentField = value;
            }
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class student
    {
        public int numerIndeksu { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public List<grade> grades { get; set; }
    }

    public class test
    {
        public string name { get; set; }
    }
}
