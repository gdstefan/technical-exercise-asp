using GeorgeStefan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain.DbService
{
	public class SiteInventoryDbHandler : ISiteInventoryDbHandler
	{
		private readonly ISiteDistributionService _distributionService;
		private readonly AppDbContext _context;

		public SiteInventoryDbHandler(AppDbContext context, ISiteDistributionService distributionService)
		{
			_context = context;
			_distributionService = distributionService;
		}
		public void UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity)
		{
			var drugUnits = _distributionService.GetRequestedDrugUnits(requestedDrugCode, requestedQuantity);
			var site = _context.Sites.First(s => s.SiteId == destinationSiteId);
			var country = _context.Countries.First(c => c.CountryId == site.CountryCode);
			var depotId = country.DepotId;

			drugUnits.ToList().ForEach(du => du.DepotId = depotId);

			_context.SaveChanges();
		}
	}
}
