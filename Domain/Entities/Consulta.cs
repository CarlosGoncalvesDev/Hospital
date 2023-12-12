using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Consulta
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public DateTime Date { get; set; }
        public string Descricao { get; set; }

    }
}

