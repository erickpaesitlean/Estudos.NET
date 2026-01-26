namespace Paciente.Domain.Entities
{
    public class ConvenioEntity
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Codico { get; private set; }
        public  ICollection<PacienteEntity> Pacientes { get; private set; }


        public ConvenioEntity(string nome, int codico, ICollection<PacienteEntity> pacientes)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Codico = codico;
            Pacientes = pacientes;
        }
    }
}
