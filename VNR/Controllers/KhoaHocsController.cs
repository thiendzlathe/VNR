using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VNR.Models;

namespace VNR.Controllers
{
    public class KhoaHocsController : Controller
    {
        private readonly VnrInternShipContext _context;

        public KhoaHocsController(VnrInternShipContext context)
        {
            _context = context;
        }

        // GET: KhoaHocs
        //[HttpGet]
        public async Task<IActionResult> Index()
        {
              return View(await _context.KhoaHocs.ToListAsync());
        }

        // GET: KhoaHocs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.KhoaHocs == null)
            {
                return NotFound();
            }
            var monhoc = _context.MonHocs.Include(m => m.KhoaHoc).Where(m=>m.KhoaHocId==id);
            ViewBag.ListMonHoc = monhoc.ToList();
            //var monhoc = _context.MonHocs.Include(m => m.KhoaHoc).Where(m => m.KhoaHocId == id);
            if (monhoc == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: KhoaHocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhoaHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenKhoaHoc")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoaHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        // GET: KhoaHocs/Edit/5
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null || _context.KhoaHocs == null)
            {
                return NotFound();
            }

            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            return View(khoaHoc);
        }

        // POST: KhoaHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenKhoaHoc")] KhoaHoc khoaHoc)
        {
            if (id != khoaHoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaHocExists(khoaHoc.Id))
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
            return View(khoaHoc);
        }

        // GET: KhoaHocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KhoaHocs == null)
            {
                return NotFound();
            }

            var khoaHoc = await _context.KhoaHocs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoaHoc == null)
            {
                return NotFound();
            }

            return View(khoaHoc);
        }

        // POST: KhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KhoaHocs == null)
            {
                return Problem("Entity set 'VnrInternShipContext.KhoaHocs'  is null.");
            }
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc != null)
            {
                _context.KhoaHocs.Remove(khoaHoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaHocExists(int id)
        {
          return _context.KhoaHocs.Any(e => e.Id == id);
        }
    }
}
