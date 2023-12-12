using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PacienteController : ControllerBase
    {
        private readonly UserDbContext _context;

        public PacienteController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var pacientes = await connection.QueryAsync<Paciente>("SELECT * FROM Pacientes");
                return Ok(pacientes);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var paciente = await connection.QueryFirstOrDefaultAsync<Paciente>("SELECT * FROM Pacientes WHERE Id = @Id", new { Id = id });
                if (paciente == null)
                {
                    return NotFound();
                }
                return Ok(paciente);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Paciente paciente)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var id = await connection.QuerySingleAsync<int>("INSERT INTO Pacientes (Nome, DataNascimento) VALUES (@Nome, @DataNascimento); SELECT SCOPE_IDENTITY()", paciente);
                paciente.Id = id;
                return CreatedAtAction(nameof(Get), new { id }, paciente);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Paciente paciente)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var affectedRows = await connection.ExecuteAsync("UPDATE Pacientes SET Nome = @Nome, DataNascimento = @DataNascimento WHERE Id = @Id", paciente);
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
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Pacientes WHERE Id = @Id", new { Id = id });
                if (affectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
    }

}
