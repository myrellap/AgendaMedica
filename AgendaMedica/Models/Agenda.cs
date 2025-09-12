namespace AgendaMedica.Models
{
    public class Agenda
    {
        public Guid AgendaId { get; set; } // Chave primaria da Tabela PK
        public Guid PacienteId { get; set; } // Chave estrangeira FK
        public Paciente? Paciente { get; set; } // Objeto referente a chave acima
        public Guid MedicoId { get; set; } // Chave estrangeira FK
        public Medico? Medico { get; set; } // Objeto referente a chave acima
        public DateTime DataConsulta { get; set; }
        public string? Status { get; set; }

    }
}
