using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeorgeStefan.DataModel;

namespace GeorgeStefan.Web.Controllers
{
    public class DrugTypesController : Controller
    {
        private readonly AppDbContext _context;

        public DrugTypesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DrugTypes.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes
                .FirstOrDefaultAsync(m => m.DrugTypeId == id);
            if (drugType == null)
            {
                return NotFound();
            }

            return View(drugType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrugTypeId,DrugTypeName,WeightInPounds")] DrugType drugType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugType);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes.FindAsync(id);
            if (drugType == null)
            {
                return NotFound();
            }
            return View(drugType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DrugTypeId,DrugTypeName,WeightInPounds")] DrugType drugType)
        {
            if (id != drugType.DrugTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugTypeExists(drugType.DrugTypeId))
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
            return View(drugType);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes
                .FirstOrDefaultAsync(m => m.DrugTypeId == id);
            if (drugType == null)
            {
                return NotFound();
            }

            return View(drugType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var drugType = await _context.DrugTypes.FindAsync(id);
            _context.DrugTypes.Remove(drugType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugTypeExists(string id)
        {
            return _context.DrugTypes.Any(e => e.DrugTypeId == id);
        }
    }
}
