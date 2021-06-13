using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAI_Laboratorium.Class
{
    class Root
    {
        public string table { get; set; }
        public string no { get; set; }
        public string tradingDate { get; set; }
        public string effectiveDate { get; set; }
        public List<Branch> branches { get; set; }
    }
}
