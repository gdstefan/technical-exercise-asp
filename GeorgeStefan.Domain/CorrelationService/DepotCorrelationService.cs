using GeorgeStefan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain.CorrelationService
{
	public class DepotCorrelationService : BaseCorrelationService<List<DepotCorrelation>>
	{
		public DepotCorrelationService(AppDbContext context) : base(context) { }

		public override List<DepotCorrelation> CorrelateData()
		{
			#region Only With SelectMany
			//var result = context.Depots
			//	.ToList()
			//	.SelectMany(depot =>
			//	{
			//		var drugUnits = context.DrugUnits.Where(du => du.DepotId == depot.DepotId);
			//		var countries = context.Countries.Where(c => c.DepotId == depot.DepotId);

			//		return drugUnits
			//		.SelectMany(
			//			du => countries,
			//			(drugUnit, country) => new DepotCorrelation
			//			{
			//				DepotName = depot.DepotName ?? "-",
			//				CountryName = country.CountryName ?? "-",
			//				DrugTypeName = drugUnit.DrugType.DrugTypeName,
			//				DrugUnitId = drugUnit.DrugUnitId ?? 0,
			//				PickNumber = drugUnit.PickNumber ?? 0
			//			}
			//		 )
			//		.ToList();
			//	})
			//	.ToList();

			#endregion

			var drugCorrelations = context.Depots
				.GroupJoin(
					context.DrugUnits,
					depot => depot.DepotId,
					du => du.DepotId,
					(depot, drugUnits) => new { Depot = depot, DrugUnits = drugUnits })
				.SelectMany(
					x => x.DrugUnits.DefaultIfEmpty(),
					(x, unit) => new { Depot = x.Depot, DrugUnit = unit });

			var correlations = drugCorrelations
				.GroupJoin(
					context.Countries,
					x => x.Depot.DepotId,
					country => country.DepotId,
					(res, countries) => new { Depot = res.Depot, DrugUnit = res.DrugUnit, Countries = countries })
				.SelectMany(
					x => x.Countries.DefaultIfEmpty(),
					(x, country) => new DepotCorrelation
					{
						DepotName = x.Depot.DepotName,
						CountryName = country != null ? country.CountryName : "-",
						DrugTypeName = x.DrugUnit != null ? x.DrugUnit.DrugType.DrugTypeName : "-",
						DrugUnitId = x.DrugUnit != null ? (x.DrugUnit.DrugUnitId ?? 0) : 0,
						PickNumber = x.DrugUnit != null ? (x.DrugUnit.PickNumber ?? 0) : 0
					});

			return correlations.ToList();
		}
	}
}
