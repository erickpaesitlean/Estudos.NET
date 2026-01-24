namespace Paciente.Domain.Entities
{
    public class PacienteEntity
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Cpf { get; private set; }

        protected PacienteEntity()
        {
            Nome = string.Empty;
            Cpf = string.Empty;
        } // EF

        public PacienteEntity(string nome, DateTime dataNascimento, string cpf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
        }
    }
}
