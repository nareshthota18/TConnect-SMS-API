using RSMS.Business.Contracts;
using RSMS.Common.Models;

namespace RSMS.Business.Implementation
{
    public class SuppilerManager : ISupplierManager
    {
        public Task<SupplierDTO> AddAsync(SupplierDTO supplier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SupplierDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SupplierDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SupplierDTO> UpdateAsync(SupplierDTO supplier)
        {
            throw new NotImplementedException();
        }
    }
}
