using Microsoft.AspNetCore.Mvc;
using moneyboss_transaction_service.Models;

namespace moneyboss_transaction_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : Controller
    {
        private static List<Income> incomes = new List<Income>
        {
            new Income
            {
                Id = 2,
                PaymentFrom = "Dad",
                Description = "For Eat",
                Category = "Pocket Money",
                UserId = 001,
                Amount = 500,
                CreatedDate = DateTime.Now,
            }
        };

        private readonly DbConnection _connection;

        public IncomeController(DbConnection connection)
        {
            _connection = connection;
        }

        // GET /incomes
        [HttpGet]
        public async Task<ActionResult<List<Income>>> Get()
        {
            return Ok(await _connection.Incomes.ToListAsync());
        }

        // GET /incomes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> Get(int id)
        {
            var inco = await _connection.Incomes.FindAsync(id);
            if (inco == null)
                return BadRequest("Income object not found!");
            return Ok(inco);
        }

        // POST /incomes
        [HttpPost]
        public async Task<ActionResult<List<Income>>> AddIncome(Income income)
        {
            _connection.Incomes.Add(income);
            await _connection.SaveChangesAsync();

            return Ok(await _connection.Incomes.ToListAsync());
        }

        // PUT /incomes
        [HttpPut]
        public async Task<ActionResult<List<Income>>> UpdateIncome(Income request)
        {
            var dbInco = await _connection.Incomes.FindAsync(request.Id);
            if (dbInco == null)
                return BadRequest("Income object not found!");

            dbInco.PaymentFrom = request.PaymentFrom;
            dbInco.Amount = request.Amount;
            dbInco.CreatedDate = DateTime.Now;
            dbInco.Category = request.Category;

            await _connection.SaveChangesAsync();

            return Ok(await _connection.Incomes.ToListAsync());
        }

        // DELETE /incomes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Income>>> Delete(int id)
        {
            var dbInco = await _connection.Incomes.FindAsync(id);
            if (dbInco == null)
                return BadRequest("Income object not found!");

            _connection.Incomes.Remove(dbInco);
            await _connection.SaveChangesAsync();

            return Ok(await _connection.Incomes.ToListAsync());
        }

        // GET /incomes/total
        [HttpGet("incomes/total")]
        public async Task<ActionResult<decimal>> GetTotal()
        {
            return Ok(await _connection.Incomes.SumAsync(x => x.Amount));
        }

        // GET /balance/total
        [HttpGet("balance/total")]
        public async Task<ActionResult<decimal>> GetBalance()
        {
            var incTot = await _connection.Incomes.SumAsync(x => x.Amount);
            var expTot = await _connection.Expense.SumAsync(x => x.Amount);

            var bal = incTot - expTot;

            return Ok(bal);
        }
    }
}
