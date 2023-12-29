using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace test_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Data.CustoemrContext _context;

        public CustomersController(Data.CustoemrContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Data.Customer>> GetAll()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            return await _context.Customers.ToArrayAsync();
        }

        [HttpPost]
        public async Task<Data.Customer> Add([FromBody] Data.Customer c)
        {
            Data.Customer cu = new();
            cu.CustomerName = c.CustomerName;
            await _context.Customers.AddAsync(cu);
            await _context.SaveChangesAsync();
            return cu;
        }
    }
}