using GeorgeStefan.Domain.CorrelationService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GeorgeStefan.Web.Controllers
{
	public class DepotUnitsController : Controller
	{
		private readonly DepotCorrelationService _correlationService;

		public DepotUnitsController(DepotCorrelationService correlationService)
		{
			_correlationService = correlationService;
		}

		public ActionResult Index()
		{
			var correlations = _correlationService
				.CorrelateData()
				.OrderBy(c => c.DepotName)
				.ThenBy(c => c.PickNumber);

			return View(correlations);
		}
	}
}
