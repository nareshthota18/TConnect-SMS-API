using RSMS.Common.Models;
using RSMS.Data.Models.LookupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface ISupplierManager
    {
        Task<SupplierDTO?> GetByIdAsync(int id);
        Task<IEnumerable<SupplierDTO>> GetAllAsync();
        Task<SupplierDTO> AddAsync(SupplierDTO supplier);
        Task<SupplierDTO> UpdateAsync(SupplierDTO supplier);
        Task<bool> DeleteAsync(int id);
    }
}
