using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Employee_XML_Data.Data;
using Employee_XML_Data.Models;
using Employee_XML_Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_XML_Data.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get.{format}"), FormatFilter]
        public async Task<ActionResult<IEnumerable<Employee>>> GetDetails()
        {
            var employee = await _context.Employees.ToListAsync();
            var manager = await _context.Managers.ToListAsync();
            var result = (from emp in employee.AsQueryable()
                          join mgr in manager.AsQueryable()
                          on emp.Id equals mgr.ManagerId
                          select new Emp_Mgr_Details
                          {
                              EmployeeId = emp.Id,
                              FirstName = emp.FirstName,
                              LastName = emp.LastName,
                              Email = emp.Email,
                              Designation = emp.Designation,
                              MgrId = mgr.ManagerId,
                              Name = mgr.Name,
                              EmailId = mgr.EmailId,
                              Salary = mgr.Salary,
                              Location = mgr.Location
                          }).ToList();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return NotFound("No Employees Found");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.Employees.Include(x => x.Manager).FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Employee Details Not Found With This Id: {id}");
        }


        [HttpPost("post.{format}"), FormatFilter]
        //[Consumes("text/xml")]
        [OnDeserialized]
        public async Task<IActionResult> PostDetailsEmployee([FromBody] Emp_Mgr_Details details)
        {
            if (ModelState.IsValid)
            {
                int count = _context.Employees.Count(x => x.FirstName == details.FirstName);
                if (count == 0)
                {
                    Employee emp = new Employee();
                    emp.FirstName = details.FirstName;
                    emp.LastName = details.LastName;
                    emp.Email = details.Email;
                    emp.Designation = details.Designation;

                    Manager mgr = new Manager();
                    mgr.Name = details.Name;
                    mgr.EmailId = details.EmailId;
                    mgr.Salary = details.Salary;
                    mgr.Location = details.Location;
                    emp.Manager = mgr;
                    _context.Employees.Add(emp);
                    await _context.SaveChangesAsync();
                    
                    return Ok("Employee Details Saved Successfully!");
                }
                else
                {
                    return BadRequest($"{details.Email} is already exist!!");
                }
            }
            else
            {
                var message = string.Join(" \n ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetails(int id, Emp_Mgr_Details details)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            //_context.Entry(employee).State = EntityState.Modified;
            try
            {
                var emp = _context.Employees.FirstOrDefault(x => x.Id == id);
                var mgr = _context.Managers.FirstOrDefault(x => x.ManagerId == id);

                emp.FirstName = details.FirstName;
                emp.LastName = details.LastName;
                emp.Email = details.Email;
                emp.Designation = details.Designation;

                mgr.ManagerId = details.MgrId;
                mgr.Name = details.Name;
                mgr.EmailId = details.EmailId;
                mgr.Salary = details.Salary;
                mgr.Location = details.Location;
                emp.Manager = mgr;
                
                await _context.SaveChangesAsync();

                return Ok("Employee Detials are Updated Successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Emp_Mgr_Details>> DeleteProduct(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            var mgr = await _context.Managers.FindAsync(id);
            if (emp == null || mgr == null)
            {
                return NotFound();
            }
            _context.Managers.Remove(mgr);
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return Ok("Employee Details are Deleted Successfully!!");
        }
    }
}
