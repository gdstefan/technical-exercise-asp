using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
    public class Depot
    {
        [Key]
        public string DepotId { get; set; }
        public string DepotName { get; set; }

        public ICollection<Country> Countries { get; set; }
        public ICollection<DrugUnit> DrugUnits { get; set; }
    }
}
