using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spychalski.Perfumes.Models;

namespace Spychalski.Perfumes.Controllers
{
    public class PerfumesController : Controller
    {
        private readonly DataContext _context;
        public PerfumesController(DataContext context)
        {
            _context = context;
        }
        // GET: PerfumesController
        public async Task<IActionResult> Index(string? perfumeName, StatusType? statusType, int? brandId)
        {
            IQueryable<Perfume> perfumes = GetPerfumes(perfumeName, statusType, brandId);
            ViewBag.BrandId = GetBrandsSelectList();
            
            return View(await perfumes.ToListAsync());
        }

        // GET: PerfumesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Perfumes == null)
            {
                return NotFound();
            }

            var perfume = await GetPerfume(id);
            if (perfume == null)
            {
                return NotFound();
            }

            return View(perfume);
        }

        // GET: PerfumesController/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = GetBrandsSelectList();
            return View();
        }

        // POST: PerfumesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Perfume perfume)
        {
            if (PerfumeExists(perfume) == true)
            {
                ModelState.AddModelError("Name", "Perfume with this name and from this brand already exist.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(perfume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.BrandId = GetBrandsSelectList();
            return View(perfume);
        }

        // GET: PerfumesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Perfumes == null)
            {
                return NotFound();
            }

            var perfume = await GetPerfume(id);
            if (perfume == null)
            {
                return NotFound();
            }

            ViewBag.BrandId = GetBrandsSelectList();
            return View(perfume);
        }

        // POST: PerfumesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Perfume perfume)
        {
            if (id != perfume.PerfumeId)
            {
                return NotFound();
            }

            bool IsPerfumeExist = PerfumeExists(perfume);
            if (IsPerfumeExist == true)
            {
                ModelState.AddModelError("Name", "Perfume with this name and from this brand already exist.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfumeExists(perfume.PerfumeId))
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

            ViewBag.BrandId = GetBrandsSelectList();
            return View(perfume);
        }

        // GET: PerfumesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Perfumes == null)
            {
                return NotFound();
            }

            var perfume = await GetPerfume(id);
            if (perfume == null)
            {
                return NotFound();
            }

            return View(perfume);
        }

        // POST: PerfumesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perfumes == null)
            {
                return Problem("Entity set 'DataContext.Perfumes'  is null.");
            }
            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume != null)
            {
                _context.Perfumes.Remove(perfume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PerfumeExists(int id)
        {
            return _context.Perfumes.Any(e => e.PerfumeId == id);
        }

        private bool PerfumeExists(Perfume perfume)
        {
            return _context.Perfumes.Any(x => x.Name == perfume.Name && x.BrandId == perfume.BrandId && x.PerfumeId != perfume.PerfumeId);
        }
        
        private List<SelectListItem> GetBrandsSelectList()
        {
            var brands = _context.Brands.ToList();
            var brandsList = new List<SelectListItem>();
            for (int i = 0; i < brands.Count; i++)
            {
                brandsList.Add(new SelectListItem(brands[i].Name, brands[i].BrandId.ToString()));
            }
            return brandsList;
        }

        private IQueryable<Perfume> GetPerfumes(string? perfumeName, StatusType? statusType, int? brandId)
        {
            var perfumes = from p in _context.Perfumes.Include(e => e.Brand)
                           select p;
            if (perfumeName != null)
            {
                perfumes = perfumes.Where(p => p.Name.Contains(perfumeName));
            }
            if (statusType != null)
            {
                perfumes = perfumes.Where(p => p.Status == statusType);
            }
            if (brandId != null)
            {
                perfumes = perfumes.Where(p => p.BrandId == brandId);
            }
            
            return perfumes;
        }

        private async Task<Perfume?> GetPerfume(int? id)
        {
           return await _context.Perfumes.Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.PerfumeId == id);
        }
    }
}
