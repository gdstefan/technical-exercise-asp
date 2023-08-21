using GeorgeStefan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain
{
	public class DepotInventoryService : IDepotInventoryService
	{
		private readonly AppDbContext _context;

		public DepotInventoryService(AppDbContext context)
		{
			_context = context;
		}
		public void AssociateDrugs(string depotId, int startPickNumber, int endPickNumber)
		{
			_context.DrugUnits
				.Where(du => du.PickNumber >= startPickNumber && du.PickNumber <= endPickNumber)
				.ToList()
				.ForEach(du => du.DepotId = depotId);

			_context.SaveChanges();
		}

		public void DisassociateDrugs(int startPickNumber, int endPickNumber)
		{
			_context.DrugUnits
				.Where(du => du.PickNumber >= startPickNumber && du.PickNumber <= endPickNumber)
				.ToList()
				.ForEach(du => du.DepotId = null);

			_context.SaveChanges();
		}

		public Dictionary<string, Dictionary<string, float>> GetTotalWeightByTypeInKg()
		{

			var grouping = _context.DrugUnits
				.Where(unit => unit.DepotId != null)
				.AsEnumerable()
				.GroupBy(unit => unit.DepotId)
				.ToDictionary(
					x => x.Key,
					x => x.GroupBy(unit => unit.DrugTypeId)
						  .ToDictionary(
								typeGroup => typeGroup.Key,
								typeGroup => PoundsToKg(typeGroup.Key, typeGroup.Count())
								)
						  );

			return grouping;
		}

		private float PoundsToKg(string type, int drugCount)
		{
			float conversionRate = 1 / 2.2f;
			return (float)Math.Round(GetDrugTypeWeight(type) * drugCount * conversionRate, 2);
		}

		private float GetDrugTypeWeight(string id)
		{
			return _context.DrugTypes.First(type => type.DrugTypeId == id).WeightInPounds;
		}
	}
}
