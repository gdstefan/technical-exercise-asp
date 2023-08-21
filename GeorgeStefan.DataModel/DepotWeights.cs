using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
	public class DepotWeights
	{
		public Depot Depot { get; set; }
		public DrugType DrugType { get; set; }
		public float WeightInKg { get; set; }
	}
}
