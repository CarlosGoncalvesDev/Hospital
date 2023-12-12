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
    [Authorize(Roles = "Medico")]
    public class ConsultaController : ControllerBase
    {
        private readonly UserDbContext _context;

        public ConsultaController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var consultas = await connection.QueryAsync<Consulta>("SELECT * FROM Consultas");
                return Ok(consultas);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var consulta = await connection.QueryFirstOrDefaultAsync<Consulta>("SELECT * FROM Consultas WHERE Id = @Id", new { Id = id });
                if (consulta == null)
                {
                    return NotFound();
                }

                var paciente = await connection.QueryFirstOrDefaultAsync<Paciente>("SELECT * FROM Pacientes WHERE Id = @Id", new { Id = consulta.PacienteId });
                consulta.Paciente = paciente;

                return Ok(consulta);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Consulta consulta)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var id = await connection.QuerySingleAsync<int>("INSERT INTO Consultas (DataConsulta, PacienteId, MedicoId) VALUES (@DataConsulta, @PacienteId, @MedicoId); SELECT SCOPE_IDENTITY()", consulta);
                consulta.Id = id;
                return CreatedAtAction(nameof(Get), new { id }, consulta);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Consulta consulta)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var affectedRows = await connection.ExecuteAsync("UPDATE Consultas SET DataConsulta = @DataConsulta, PacienteId = @PacienteId, MedicoId = @MedicoId WHERE Id = @Id", consulta);
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
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Consultas WHERE Id = @Id", new { Id = id });
                if (affectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
    }
}
