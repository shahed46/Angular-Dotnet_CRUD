using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullstackDbContext _fullstackDbContext;
        public EmployeesController(FullstackDbContext fullstackDbContext)
        {
            _fullstackDbContext = fullstackDbContext;
        }


        [HttpGet]
        public async Task< IActionResult> GetAllEmployees()
        {
          var employees=  await _fullstackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            await _fullstackDbContext.Employees.AddAsync(employeeRequest);
            await _fullstackDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var employee= await _fullstackDbContext.Employees.FirstOrDefaultAsync(x=>x.id==id);
            if(employee==null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> uddateEmployee(int id, Employee updateRequest)
        {
            var employee=await _fullstackDbContext.Employees.FirstAsync(x=>x.id==id);
            if(employee==null)
            {
                return NotFound();
            }
            employee.name=updateRequest.name;
            employee.email=updateRequest.email; 
            employee.phone=updateRequest.phone;
            await _fullstackDbContext.SaveChangesAsync();
            return Ok(employee);

        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> deleteEmployee(int id)
        {
            var employee=await _fullstackDbContext.Employees.FindAsync(id);    
            if(employee==null)
            {
                return NotFound();
            }
            _fullstackDbContext.Employees.Remove(employee);
            await _fullstackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
