using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
    public class Country
    {
        [Key]
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string DepotId { get; set; }

        public Depot Depot { get; set; }
    }
}
