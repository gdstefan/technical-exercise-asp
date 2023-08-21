using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
    public class DrugType
    {
        [Key]
        public string DrugTypeId { get; set; }
        public string DrugTypeName { get; set; }
        public float WeightInPounds { get; set; }

        public ICollection<DrugUnit> DrugUnits { get; set; }
    }
}
