using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeorgeStefan.DataModel;
using GeorgeStefan.Domain;

namespace GeorgeStefan.Web.Controllers
{
	public class DepotsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DepotInventoryService _depotInventoryService;

        public DepotsController(AppDbContext context, DepotInventoryService depotInventoryService)
        {
            _context = context;
            _depotInventoryService = depotInventoryService;
        }

        public IActionResult Weight()
		{
            var weights = _depotInventoryService.GetTotalWeightByTypeInKg();
            return View(weights);
		}

        public async Task<IActionResult> Index()
        {
            return View(await _context.Depots.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots
                .FirstOrDefaultAsync(m => m.DepotId == id);
            if (depot == null)
            {
                return NotFound();
            }

            return View(depot);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepotId,DepotName")] Depot depot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depot);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots.FindAsync(id);
            if (depot == null)
            {
                return NotFound();
            }
            return View(depot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DepotId,DepotName")] Depot depot)
        {
            if (id != depot.DepotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepotExists(depot.DepotId))
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
            return View(depot);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots
                .FirstOrDefaultAsync(m => m.DepotId == id);
            if (depot == null)
            {
                return NotFound();
            }

            return View(depot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var depot = await _context.Depots.FindAsync(id);
            _context.Depots.Remove(depot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepotExists(string id)
        {
            return _context.Depots.Any(e => e.DepotId == id);
        }
    }
}
