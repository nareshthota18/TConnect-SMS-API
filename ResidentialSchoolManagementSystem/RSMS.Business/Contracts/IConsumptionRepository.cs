using RSMS.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Repositories.Contracts
{
    public interface IConsumptionRepository
    {
        Task AddAsync(ConsumptionConfig entity);
        Task UpdateAsync(ConsumptionConfig entity);
        Task<bool> DeleteAsync(Guid id);
        Task<ConsumptionConfig?> GetByIdAsync(Guid id);
        Task<IEnumerable<ConsumptionConfig>> GetAllAsync(Guid hostelId);
    }
}
