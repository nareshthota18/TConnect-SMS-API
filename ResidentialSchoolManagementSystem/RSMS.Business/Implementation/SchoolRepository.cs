using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Repositories.Implementation
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly RSMSDbContext _context;

        public SchoolRepository(RSMSDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RSHostel hostel)
        {
            hostel.Id = Guid.NewGuid();
            _context.RSHostels.Add(hostel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RSHostel>> GetAllAsync(Guid UserId)
        {
            return await _context.RSHostels.Where(x => x.CreatedBy == UserId).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var sc = await _context.RSHostels.FindAsync(id);
            if (sc == null) return false;

            _context.RSHostels.Remove(sc);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
