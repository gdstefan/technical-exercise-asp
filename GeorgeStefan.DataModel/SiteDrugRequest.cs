using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
	public class SiteDrugRequest
	{
		public string SiteId { get; set; }
		public int RequestedQuantity { get; set; }
		public string DrugTypeId { get; set; }
	}
}
