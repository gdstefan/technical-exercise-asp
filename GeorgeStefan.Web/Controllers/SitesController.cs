using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeorgeStefan.DataModel;
using GeorgeStefan.Domain.DbService;
using GeorgeStefan.Domain;

namespace GeorgeStefan.Web.Controllers
{
	public class SitesController : Controller
	{
		private readonly AppDbContext _context;
		private readonly SiteInventoryDbHandler _siteHandler;

		public SitesController(AppDbContext context, SiteDistributionService distributionService)
		{
			_context = context;
			_siteHandler = new SiteInventoryDbHandler(_context, distributionService);
		}

		public async Task<IActionResult> Index()
		{
			var appDbContext = _context.Sites.Include(s => s.Country);
			return View(await appDbContext.ToListAsync());
		}

		public IActionResult RequestDrugs(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			ViewBag.DrugCodes = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeName");
			ViewBag.SiteNames = new SelectList(_context.Sites, "SiteId", "SiteName");

			var request = new SiteDrugRequest { SiteId = id };
			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> RequestDrugs([Bind("SiteId,DrugTypeId,RequestedQuantity")] SiteDrugRequest request)
		{
			if (ModelState.IsValid)
			{
				_siteHandler.UpdateSiteInventory(request.SiteId, request.DrugTypeId, request.RequestedQuantity);
				return RedirectToAction(nameof(Index));
			}
			var site = await _context.Sites
			   .FirstOrDefaultAsync(m => m.SiteId == request.SiteId);

			return View(site);
		}


		// GET: Sites/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var site = await _context.Sites
				.Include(s => s.Country)
				.FirstOrDefaultAsync(m => m.SiteId == id);
			if (site == null)
			{
				return NotFound();
			}

			return View(site);
		}

		// GET: Sites/Create
		public IActionResult Create()
		{
			ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryId", "CountryId");
			return View();
		}

		// POST: Sites/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("SiteId,SiteName,CountryCode")] Site site)
		{
			if (ModelState.IsValid)
			{
				_context.Add(site);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryId", "CountryId", site.CountryCode);
			return View(site);
		}

		// GET: Sites/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var site = await _context.Sites.FindAsync(id);
			if (site == null)
			{
				return NotFound();
			}
			ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryId", "CountryId", site.CountryCode);
			return View(site);
		}

		// POST: Sites/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("SiteId,SiteName,CountryCode")] Site site)
		{
			if (id != site.SiteId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(site);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SiteExists(site.SiteId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryId", "CountryId", site.CountryCode);
			return View(site);
		}

		// GET: Sites/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var site = await _context.Sites
				.Include(s => s.Country)
				.FirstOrDefaultAsync(m => m.SiteId == id);
			if (site == null)
			{
				return NotFound();
			}

			return View(site);
		}

		// POST: Sites/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var site = await _context.Sites.FindAsync(id);
			_context.Sites.Remove(site);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SiteExists(string id)
		{
			return _context.Sites.Any(e => e.SiteId == id);
		}
	}
}
