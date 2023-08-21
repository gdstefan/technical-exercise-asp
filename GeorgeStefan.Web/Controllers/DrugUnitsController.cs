using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeorgeStefan.DataModel;
using GeorgeStefan.Domain;

namespace GeorgeStefan.Web.Controllers
{
    public class DrugUnitsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DepotInventoryService _depotInventoryService;

        public DrugUnitsController(AppDbContext context, DepotInventoryService depotInventoryService)
        {
            _context = context;
            _depotInventoryService = depotInventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DrugUnits.Include(d => d.Depot).Include(d => d.DrugType);
            return View(await appDbContext.ToListAsync());
        }

        public ActionResult Grouped()
        {
            var groupedUnits = _context.DrugUnits.ToList().ToGroupedDrugUnits();
            return View(groupedUnits);
        }

        public ActionResult Associate()
		{
            ViewBag.DepotNames = new SelectList(_context.Depots, "DepotId", "DepotName");
            return View();
		}

		[HttpPost]
		public ActionResult Associate([Bind("LowerBound,UpperBound,DepotId")] RangeModel range)
		{
            if(ModelState.IsValid)
			{
                _depotInventoryService.AssociateDrugs(range.DepotId, range.LowerBound, range.UpperBound);
                return RedirectToAction(nameof(Index));
            }

            return View();
		}

        public ActionResult Disassociate()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Disassociate([Bind("LowerBound,UpperBound")] RangeModel range)
        {
            if (ModelState.IsValid)
            {
                _depotInventoryService.DisassociateDrugs(range.LowerBound, range.UpperBound);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugUnit = await _context.DrugUnits
                .Include(d => d.Depot)
                .Include(d => d.DrugType)
                .FirstOrDefaultAsync(m => m.DrugUnitId == id);
            if (drugUnit == null)
            {
                return NotFound();
            }

            return View(drugUnit);
        }

        public IActionResult Create()
        {
            ViewData["DepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId");
            ViewData["DrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrugUnitId,PickNumber,DrugTypeId,DepotId")] DrugUnit drugUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId", drugUnit.DepotId);
            ViewData["DrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId", drugUnit.DrugTypeId);
            return View(drugUnit);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugUnit = await _context.DrugUnits.FindAsync(id);
            if (drugUnit == null)
            {
                return NotFound();
            }
            ViewData["DepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId", drugUnit.DepotId);
            ViewData["DrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId", drugUnit.DrugTypeId);
            return View(drugUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("DrugUnitId,PickNumber,DrugTypeId,DepotId")] DrugUnit drugUnit)
        {
            if (id != drugUnit.DrugUnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugUnitExists(drugUnit.DrugUnitId))
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
            ViewData["DepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId", drugUnit.DepotId);
            ViewData["DrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId", drugUnit.DrugTypeId);
            return View(drugUnit);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugUnit = await _context.DrugUnits
                .Include(d => d.Depot)
                .Include(d => d.DrugType)
                .FirstOrDefaultAsync(m => m.DrugUnitId == id);
            if (drugUnit == null)
            {
                return NotFound();
            }

            return View(drugUnit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var drugUnit = await _context.DrugUnits.FindAsync(id);
            _context.DrugUnits.Remove(drugUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugUnitExists(int? id)
        {
            return _context.DrugUnits.Any(e => e.DrugUnitId == id);
        }
    }
}
