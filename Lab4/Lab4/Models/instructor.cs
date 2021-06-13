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
    public partial class instructors
    {
        private List<instructor> instructorField = new List<instructor>();

        [System.Xml.Serialization.XmlElementAttribute("instructor")]
        public List<instructor> instructor
        {
            get
            {
                return this.instructorField;
            }
            set
            {
                this.instructorField = value;
            }
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class instructor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }

    }
}
