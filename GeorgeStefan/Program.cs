using GeorgeStefan.DataModel;
using GeorgeStefan.Domain;
using GeorgeStefan.Domain.CorrelationService;
using GeorgeStefan.Domain.DbService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeorgeStefan
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var context = new AppDbContext())
			{
				SeedData(context);
			}
			//using (var context = new AppDbContext())
			//{
			//	SeedData(context);
			//	var depotInventoryService = new DepotInventoryService();

				//	Console.WriteLine("Initial state:");
				//	depotInventoryService.DisplayDrugUnitDepotAssociations(context.DrugUnits.ToList());

				//	depotInventoryService.AssociateDrugs(context, "1", 1, 14);
				//	depotInventoryService.AssociateDrugs(context, "2", 15, 16);

				//	Console.WriteLine("\nAfter association:");
				//	depotInventoryService.DisplayDrugUnitDepotAssociations(context.DrugUnits.ToList());

				//	depotInventoryService.DisassociateDrugs(context, 1, 5);

				//	Console.WriteLine("\nAfter disassociation:");
				//	depotInventoryService.DisplayDrugUnitDepotAssociations(context.DrugUnits.ToList());

				//	var unitsByDrugType = context.DrugUnits.ToList().ToGroupedDrugUnits();
				//	DisplayUnitsByDrugType(unitsByDrugType);

				//	var correlationService = new DepotCorrelationService(context);

				//	List<DepotCorrelation> correlatedData = correlationService.CorrelateData();
				//	DisplayCorrelatedData(correlatedData);


				//	string siteId = "249";
				//	string drugCode = "10";
				//	int quantity = 6;

				//	var siteDistributionService = new SiteDistributionService(context);
				//	IEnumerable<DrugUnit> requestedDrugs = siteDistributionService.GetRequestedDrugUnits(siteId, drugCode, quantity);

				//	Console.WriteLine($"\nSite {siteId} requested {quantity} of drug type {drugCode}");
				//	foreach (var drug in requestedDrugs)
				//	{
				//		Console.WriteLine($"Drug {drug.DrugUnitId} - Type {drug.DrugTypeId}");
				//	}

				//	var siteInventoryDbHandler = new SiteInventoryDbHandler(context, siteDistributionService);
				//	siteInventoryDbHandler.UpdateSiteInventory("371", drugCode, quantity);

				//	depotInventoryService.DisplayDrugUnitDepotAssociations(context.DrugUnits.ToList());
				//	Console.Write("\n\nPress RETURN to continue...");
				//	Console.Read();
				//}
		}

		public static void DisplayUnitsByDrugType(Dictionary<string, List<DrugUnit>> unitsByDrugType)
		{
			Console.WriteLine("\nDrug units groupd by drug type: ");
			foreach (var group in unitsByDrugType)
			{
				Console.WriteLine($"\nDrug Type {group.Key}");
				foreach (var value in group.Value)
				{
					Console.WriteLine($"\tDrug {value.DrugUnitId} (Pick {value.PickNumber})");
				}
			}
		}

		public static void DisplayCorrelatedData(List<DepotCorrelation> correlatedData)
		{
			Console.WriteLine("Correlated Data:");
			correlatedData
				.ForEach(data => Console.WriteLine($"Depot: {data.DepotName}, Country: {data.CountryName}, Drug Type: {data.DrugTypeName}, Drug Unit Id: {data.DrugUnitId}, Pick Number: {data.PickNumber}"));
		}

		public static void SeedData(AppDbContext context)
		{
			context.PopulateIfEmpty(context.Depots, () =>
			{
				context.Add(new Depot { DepotId = "1", DepotName = "Depot X" });
				context.Add(new Depot { DepotId = "2", DepotName = "Depot Y" });
				context.Add(new Depot { DepotId = "3", DepotName = "Depot Z" });
			});

			context.PopulateIfEmpty(context.Countries, () =>
			{
				context.Add(new Country { CountryId = "US", CountryName = "United States", DepotId = "1" });
				context.Add(new Country { CountryId = "CA", CountryName = "Canada", DepotId = "2" });
			});

			context.PopulateIfEmpty(context.Sites, () =>
			{
				context.Add(new Site { CountryCode = "US", SiteId = "249", SiteName = "US_SITE" });
				context.Add(new Site { CountryCode = "CA", SiteId = "371", SiteName = "CA_SITE" });
			});

			context.PopulateIfEmpty(context.DrugTypes, () =>
			{
				context.Add(new DrugType { DrugTypeId = "10", DrugTypeName = "Painkiller" });
				context.Add(new DrugType { DrugTypeId = "20", DrugTypeName = "Antibiotic" });
				context.Add(new DrugType { DrugTypeId = "30", DrugTypeName = "Antiviral" });
			});

			context.PopulateIfEmpty(context.DrugUnits, () =>
			{
				var drugTypes = context.DrugTypes.ToList();
				var list = Enumerable.Range(1, 21)
				.Select(it =>
					 new DrugUnit
					 {
						 PickNumber = it,
						 DrugTypeId = drugTypes[it % drugTypes.Count].DrugTypeId,
					 })
				.ToList();

				context.DrugUnits.AddRange(list);
			});
		}
	}
}
