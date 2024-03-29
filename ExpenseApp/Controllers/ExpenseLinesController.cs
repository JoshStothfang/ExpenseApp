using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Data;
using ExpenseApp.Models;

namespace ExpenseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseLinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseLinesController(AppDbContext context)
        {
            _context = context;
        }

        //Recalculate ExpenseTotal
        private async Task RecalculateExpenseTotal(int expenseId)
        {
            var total = (from el in _context.ExpenseLines
                         join i in _context.Items
                            on el.ItemId equals i.Id
                         where el.ExpenseId == expenseId
                         select new
                         {
                             LineTotal = el.Quantity * i.Price
                         }).Sum(x => x.LineTotal);
            var expense = await _context.Expenses.FindAsync(expenseId);
            expense!.Total = total;
            await _context.SaveChangesAsync();
        }

        // GET: api/ExpenseLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseLine>>> GetExpenseLines()
        {
          if (_context.ExpenseLines == null)
          {
              return NotFound();
          }
            return await _context.ExpenseLines.ToListAsync();
        }

        // GET: api/ExpenseLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseLine>> GetExpenseLine(int id)
        {
          if (_context.ExpenseLines == null)
          {
              return NotFound();
          }
            var expenseLine = await _context.ExpenseLines.FindAsync(id);

            if (expenseLine == null)
            {
                return NotFound();
            }

            return expenseLine;
        }

        // PUT: api/ExpenseLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseLine(int id, ExpenseLine expenseLine)
        {
            if (id != expenseLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenseLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await RecalculateExpenseTotal(expenseLine.ExpenseId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseLineExists(id))
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

        // POST: api/ExpenseLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseLine>> PostExpenseLine(ExpenseLine expenseLine)
        {
          if (_context.ExpenseLines == null)
          {
              return Problem("Entity set 'AppDbContext.ExpenseLines'  is null.");
          }
            _context.ExpenseLines.Add(expenseLine);
            await _context.SaveChangesAsync();
            await RecalculateExpenseTotal(expenseLine.ExpenseId);

            return CreatedAtAction("GetExpenseLine", new { id = expenseLine.Id }, expenseLine);
        }

        // DELETE: api/ExpenseLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseLine(int id)
        {
            if (_context.ExpenseLines == null)
            {
                return NotFound();
            }
            var expenseLine = await _context.ExpenseLines.FindAsync(id);
            if (expenseLine == null)
            {
                return NotFound();
            }
            var expId = expenseLine.ExpenseId;
            _context.ExpenseLines.Remove(expenseLine);
            await _context.SaveChangesAsync();
            await RecalculateExpenseTotal(expId);

            return NoContent();
        }

        private bool ExpenseLineExists(int id)
        {
            return (_context.ExpenseLines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
