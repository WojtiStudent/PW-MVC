using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spychalski.Perfumes.Models;

namespace Spychalski.Perfumes.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DataContext _context;
        public BrandsController(DataContext context)
        {
            _context = context;
        }
        // GET: BrandsController
        public async Task<IActionResult> Index(string? brandName)
        {
            IQueryable<Brand> brands = GetBrands(brandName);

            return View(await brands.ToListAsync());
        }

        // GET: BrandsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await GetBrand(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            
            if (BrandNameExists(brand) == true)
            {
                ModelState.AddModelError("Name", "Brand with this name already exist.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: BrandsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }
            
            var brand = await GetBrand(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return NotFound();
            }

            bool IsBrandNameExist = BrandNameExists(brand);
            if (IsBrandNameExist == true)
            {
                ModelState.AddModelError("Name", "Brand with this name already exist.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandId))
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
            return View(brand);
        }

        // GET: BrandsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await GetBrand(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: BrandsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'DataContext.Brands'  is null.");
            }
            var brand = await GetBrand(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }

        private bool BrandNameExists(Brand brand)
        {
            return _context.Brands.Any(x => x.Name == brand.Name && x.BrandId != brand.BrandId);
        }

        private IQueryable<Brand> GetBrands(string? brandName)
        {
            var brands = from b in _context.Brands select b;
            if (brandName != null)
            {
                brands = brands.Where(b => b.Name.Contains(brandName));
            }
            return brands;
        }

        private async Task<Brand?> GetBrand(int? id)
        {
            return await _context.Brands
                .FindAsync(id);
        }

    }
}
