using GeorgeStefan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain
{
	public interface IDepotInventoryService
	{
		void AssociateDrugs(string depotId, int startPickNumber, int endPickNumber);
		void DisassociateDrugs(int startPickNumber, int endPickNumber);

		Dictionary<string, Dictionary<string, float>> GetTotalWeightByTypeInKg();
	}
}
