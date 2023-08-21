using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
    public class DepotCorrelation
    {
        public string DepotName { get; set; }
        public string CountryName { get; set; }
        public string DrugTypeName { get; set; }
        public int DrugUnitId { get; set; }
        public int PickNumber { get; set; }
    }
}
