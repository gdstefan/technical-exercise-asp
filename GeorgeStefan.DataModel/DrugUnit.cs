using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
    public class DrugUnit
    {
        [Key]
        public int? DrugUnitId { get; set; }
        public int? PickNumber { get; set; }
        public string DrugTypeId { get; set; }
        public string DepotId { get; set; }

        public Depot Depot { get; set; }
        public DrugType DrugType { get; set; }
    }
}
