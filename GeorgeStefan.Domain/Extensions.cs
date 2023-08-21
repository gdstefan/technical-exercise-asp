using GeorgeStefan.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain
{
	public static class Extensions
	{
		public static Dictionary<string, List<DrugUnit>> ToGroupedDrugUnits(this IList<DrugUnit> units)
		{
			return units
				.GroupBy(unit => unit.DrugTypeId)
				.ToDictionary(group => group.Key, group => group.ToList());
		}

		public static void PopulateIfEmpty<T>(this AppDbContext context, DbSet<T> dbSet, Action populateFunc) where T : class
		{
			if (!dbSet.Any())
			{
				populateFunc.Invoke();
				context.SaveChanges();
			}
		}
	}
}
