using GeorgeStefan.DataModel;
using GeorgeStefan.Domain.DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain
{
	public class SiteDistributionService : ISiteDistributionService
	{
		private readonly AppDbContext _context;

		public SiteDistributionService(AppDbContext context)
		{
			_context = context;
		}
		public IEnumerable<DrugUnit> GetRequestedDrugUnits(string drugCode, int quantity)
		{
			var result = _context.DrugUnits.Where(du => du.DrugTypeId == drugCode && du.DepotId == null);

			return result
				.OrderBy(du => du.PickNumber)
				.Take(quantity)
				.ToList();
		}
	}
}
