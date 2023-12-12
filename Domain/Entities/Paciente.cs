using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public double Peso { get; set; }
        public List<Consulta> Consultas { get; set; }

    }
}
