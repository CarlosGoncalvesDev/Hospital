using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicoController : ControllerBase
    {
        private readonly UserDbContext _context;

        public MedicoController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var medicos = await connection.QueryAsync<Medico>("SELECT * FROM Medicos");
                return Ok(medicos);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var medico = await connection.QueryFirstOrDefaultAsync<Medico>("SELECT * FROM Medicos WHERE Id = @Id", new { Id = id });
                if (medico == null)
                {
                    return NotFound();
                }
                return Ok(medico);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Medico medico)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var id = await connection.QuerySingleAsync<int>("INSERT INTO Medicos (Nome, Especialidade) VALUES (@Nome, @Especialidade); SELECT SCOPE_IDENTITY()", medico);
                medico.Id = id;
                return CreatedAtAction(nameof(Get), new { id }, medico);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Medico medico)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var affectedRows = await connection.ExecuteAsync("UPDATE Medicos SET Nome = @Nome, Especialidade = @Especialidade WHERE Id = @Id", medico);
                if (affectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Medicos WHERE Id = @Id", new { Id = id });
                if (affectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }

    }

}
