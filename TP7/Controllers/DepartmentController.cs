using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP7.Models;
using TP7.Models.Repository;

namespace TP7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartementRepository repository;

        public DepartmentController(IDepartementRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            List<Departement> departements
                = await repository.GetDepartement();
           
                return Ok(departements);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
           Departement departements
                = await repository.GetDepartementbyID(id);

            return Ok(departements);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Departement departement)
        {
           Departement dep= await repository.AddDepartement(departement);
            if (dep == null)
                return BadRequest("Problem");
            return CreatedAtAction(nameof(GetByID),
                new { id = dep.DepartementID }, dep);
        }
    }
}
