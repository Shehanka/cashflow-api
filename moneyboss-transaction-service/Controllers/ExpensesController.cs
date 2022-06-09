using Microsoft.AspNetCore.Mvc;
using moneyboss_transaction_service.Models;

namespace moneyboss_transaction_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : Controller
    {
        private static List<Expenses> expenses = new List<Expenses>
        {
            new Expenses
            {
                Id = 1,
                PaymentTo = "Bro",
                Description = "Food",
                Category = "Bank",
                UserId = 001,
                Amount = 500,
                CreatedDate = DateTime.Now,
            }
        };

        private readonly DbConnection _connection;

        public ExpensesController(DbConnection connection)
        {
            _connection = connection;
        }

        // GET /expenses
        [HttpGet]
        public async Task<ActionResult<List<Expenses>>> Get()
        {
            return Ok(await _connection.Expense.ToListAsync());
        }

        // GET /expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expenses>> Get(int id)
        {
            var exp = await _connection.Expense.FindAsync(id);
            if (exp == null)
                return BadRequest("Expense object not found!");
            return Ok(exp);
        }

        // POST /expenses
        [HttpPost]
        public async Task<ActionResult<List<Expenses>>> AddExpense(Expenses expenses)
        {
            _connection.Expense.Add(expenses);
            await _connection.SaveChangesAsync();

            return Ok(await _connection.Expense.ToListAsync());
        }

        // PUT /incomes
        [HttpPut]
        public async Task<ActionResult<List<Expenses>>> UpdateExpenses(Expenses request)
        {
            var dbExp = await _connection.Expense.FindAsync(request.Id);
            if (dbExp == null)
                return BadRequest("Income object not found!");

            dbExp.PaymentTo = request.PaymentTo;
            dbExp.Amount = request.Amount;
            dbExp.CreatedDate = DateTime.Now;
            dbExp.Category = request.Category;

            await _connection.SaveChangesAsync();

            return Ok(await _connection.Expense.ToListAsync());
        }

        // DELETE /expenses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Expenses>>> Delete(int id)
        {
            var dbExp = await _connection.Expense.FindAsync(id);
            if (dbExp == null)
                return BadRequest("Expense object not found!");

            _connection.Expense.Remove(dbExp);
            await _connection.SaveChangesAsync();

            return Ok(await _connection.Expense.ToListAsync());
        }

        // GET /expenses/total
        [HttpGet("expenses/total")]
        public async Task<ActionResult<decimal>> GetTotal()
        {
            return Ok(await _connection.Expense.SumAsync(x => x.Amount));
        }

    }
}
