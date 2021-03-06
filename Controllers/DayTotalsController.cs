﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManger.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayTotalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DayTotalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DayTotal DayTotal { get; set; }

        // GET: api/DayTotals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayTotal>>> GetDayTotal(int date)
        {
            var dateTime = DateTime.UtcNow.Date;
            date += 1;
            var time = DateTime.UtcNow.Hour;
            if (time >= 0 && time < 8)
            {
                date -= 1;
                //Trace.WriteLine("NICE");
            }

            for (int index = 0; index <= 6; index++)
            {
                var temp = await _context.DayTotal.FirstOrDefaultAsync(d =>
                d.Date == dateTime.AddDays(date - index).AddHours(-8).ToString("dd/MM/yyyy") &&
                d.Owner == User.Identity.Name);


                //Trace.WriteLine("Hello Worsld!", index.ToString());

                if (temp == null)
                {

                    var temp2 = new DayTotal { };
                    temp2.TotalCarbs = 0;
                    temp2.TotalFats = 0;
                    temp2.TotalProtein = 0;
                    temp2.Owner = User.Identity.Name;

                    temp2.Date = dateTime.AddDays((date) - index).AddHours(-8).ToString("dd/MM/yyyy");
                    temp2.RealDate = dateTime.AddDays(date - index).AddHours(-8);

                    _context.DayTotal.Add(temp2);
                    await _context.SaveChangesAsync();
                }

            }

            var dbSet = await _context.DayTotal.Where(d => (
                d.RealDate == dateTime.AddDays(date - 6).AddHours(-8) ||
                d.RealDate == dateTime.AddDays(date - 5).AddHours(-8) ||
                d.RealDate == dateTime.AddDays(date - 4).AddHours(-8) ||
                d.RealDate == dateTime.AddDays(date - 3).AddHours(-8) ||
                d.RealDate == dateTime.AddDays(date - 2).AddHours(-8) ||
                d.RealDate == dateTime.AddDays(date - 1).AddHours(-8) ||
                d.RealDate == dateTime.AddDays(date).AddHours(-8)) && 
                d.Owner == User.Identity.Name)
                .OrderByDescending(d => d.RealDate)
                .ToListAsync();

            for (int index = 0; index <= 6; index++)
            {
                var temp = await _context.DayTotal.FirstOrDefaultAsync(d =>
                d.Date == dateTime.AddDays(date - index).AddHours(-8).ToString("dd/MM/yyyy") &&
                d.Owner == User.Identity.Name);

                if (temp.TotalCarbs + temp.TotalFats + temp.TotalProtein == 0)
                {
                    _context.DayTotal.Remove(temp);
                    await _context.SaveChangesAsync();
                }
            }

            return dbSet;
        }

        // GET: api/DayTotals/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<DayTotal>> GetDayTotal(int id)
        {
            var dayTotal = await _context.DayTotal.FindAsync(id);

            if (dayTotal == null)
            {
                return NotFound();
            }

            return dayTotal;
        }*/

        // PUT: api/DayTotals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDayTotal(int id, DayTotal dayTotal)
        {
            if (id != dayTotal.DayTotalID)
            {
                return BadRequest();
            }

            _context.Entry(dayTotal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayTotalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DayTotals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DayTotal>> PostDayTotal(DayTotal dayTotal)
        {
            _context.DayTotal.Add(dayTotal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDayTotal", new { id = dayTotal.DayTotalID }, dayTotal);
        }

        // DELETE: api/DayTotals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DayTotal>> DeleteDayTotal(int id)
        {
            var dayTotal = await _context.DayTotal.FindAsync(id);
            if (dayTotal == null)
            {
                return NotFound();
            }

            _context.DayTotal.Remove(dayTotal);
            await _context.SaveChangesAsync();

            return dayTotal;
        }

        private bool DayTotalExists(int id)
        {
            return _context.DayTotal.Any(e => e.DayTotalID == id);
        }
    }
}