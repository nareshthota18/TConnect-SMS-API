using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface ISuppilerRepository
    {
        Task<SupplierDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<SupplierDTO>> GetAllAsync();
        Task<SupplierDTO> AddAsync(SupplierDTO supplier);
        Task<SupplierDTO> UpdateAsync(SupplierDTO supplier);
        Task<bool> DeleteAsync(Guid id);
    }
}
