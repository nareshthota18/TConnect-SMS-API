using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class SuppilerRepository : ISuppilerRepository
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public SuppilerRepository(ISupplierService supplierService,IMapper mapper) 
        { 
            _supplierService = supplierService;
            _mapper = mapper;
        }
        public Task<SupplierDTO> AddAsync(SupplierDTO supplier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SupplierDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SupplierDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SupplierDTO> UpdateAsync(SupplierDTO supplier)
        {
            throw new NotImplementedException();
        }
    }
}
