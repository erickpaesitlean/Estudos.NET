using Paciente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paciente.Domain.Interfaces
{
    public interface IConvenioRepository
    {
        Task<IEnumerable<ConvenioEntity>> GetAllAsync();
        Task<ConvenioEntity> GetByIdAsync(int id);
        Task AddAsync(ConvenioEntity convenio);
        Task UpdateAsync(ConvenioEntity convenio);
        Task DeleteAsync(int id);
    }
}
