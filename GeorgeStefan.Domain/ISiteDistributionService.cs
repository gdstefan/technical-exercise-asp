using GeorgeStefan.DataModel;
using System.Collections.Generic;

namespace GeorgeStefan.Domain.DbService
{
	public interface ISiteDistributionService
	{
		IEnumerable<DrugUnit> GetRequestedDrugUnits(string drugCode, int quantity);
	}
}