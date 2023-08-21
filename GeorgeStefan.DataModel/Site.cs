using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
	public class Site
	{
		[Key]
		public string SiteId { get; set; }
		public string SiteName { get; set; }
		public string CountryCode { get; set; }

		public Country Country { get; set; }

	}
}
