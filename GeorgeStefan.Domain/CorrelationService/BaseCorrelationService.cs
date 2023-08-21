using GeorgeStefan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.Domain.CorrelationService
{
	public abstract class BaseCorrelationService<T>
	{
		protected readonly AppDbContext context;

		protected BaseCorrelationService(AppDbContext context)
		{
			this.context = context;
		}

		public abstract T CorrelateData();
	}
}
