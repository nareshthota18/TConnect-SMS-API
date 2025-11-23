using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Repositories.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly RSMSDbContext _context;

        public AttendanceRepository(RSMSDbContext context)
        {
            _context = context;
        }

        // Student Attendance
        public async Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync()
        {
            return await _context.StudentAttendance
                .Include(a => a.Student) // <--- This is the key change
                .ToListAsync();
        }

        public async Task<StudentAttendance?> GetStudentAttendanceByIdAsync(Guid id) =>
            await _context.StudentAttendance.FindAsync(id);

        public async Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance att)
        {
            _context.StudentAttendance.Add(att);
            await _context.SaveChangesAsync();
            return att;
        }

        public async Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance att)
        {
            _context.StudentAttendance.Update(att);
            await _context.SaveChangesAsync();
            return att;
        }

        public async Task<bool> DeleteStudentAttendanceAsync(Guid id)
        {
            var att = await _context.StudentAttendance.FindAsync(id);
            if (att == null) return false;

            _context.StudentAttendance.Remove(att);
            await _context.SaveChangesAsync();
            return true;
        }

        // Staff Attendance
        public async Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync()
        {
            return await _context.StaffAttendance
                .Include(sa => sa.Staff)
                .ToListAsync();
        }

        public async Task<StaffAttendance?> GetStaffAttendanceByIdAsync(Guid id) =>
            await _context.StaffAttendance.FindAsync(id);

        public async Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance att)
        {
            _context.StaffAttendance.Add(att);
            await _context.SaveChangesAsync();
            return att;
        }

        public async Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance att)
        {
            _context.StaffAttendance.Update(att);
            await _context.SaveChangesAsync();
            return att;
        }

        public async Task<bool> DeleteStaffAttendanceAsync(Guid id)
        {
            var att = await _context.StaffAttendance.FindAsync(id);
            if (att == null) return false;

            _context.StaffAttendance.Remove(att);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StaffAttendance>> CreateStaffAttendanceList(List<StaffAttendance> att)
        {
            if (att == null || !att.Any())
            {
                return new List<StaffAttendance>();
            }

            // 1. Get all unique Staff IDs from the incoming batch.
            var incomingStaffIds = att.Select(a => a.StaffId).Distinct().ToList();

            // 2. Fetch ALL matching existing records from the database.
            // This is the CRITICAL FIX for the LINQ translation error: use Contains() on the ID list.
            // These entities are now TRACKED by the DbContext.
            var existingRecords = await _context.StaffAttendance
                .Where(sa => incomingStaffIds.Contains(sa.StaffId))
                .ToListAsync();

            // 3. Prepare container for entities that need to be added.
            var entitiesToAdd = new List<StaffAttendance>();

            // 4. Iterate and process: Update Tracked Entities or Mark for Insert.
            foreach (var incomingEntity in att)
            {
                // Find a match based on the composite key (StaffId + Date) in memory.
                var matchingExisting = existingRecords.FirstOrDefault(er =>
                    er.StaffId == incomingEntity.StaffId &&
                    er.AttendanceDate.Date == incomingEntity.AttendanceDate.Date); // Compare Date only

                if (matchingExisting != null)
                {
                    // UPDATE: Record exists and is ALREADY tracked.

                    // **CRITICAL FIX for Tracking Error:** Copy values from the incoming entity
                    // to the entity that is already tracked by the DbContext.
                    matchingExisting.Status = incomingEntity.Status;
                    matchingExisting.Remarks = incomingEntity.Remarks;
                    // NOTE: Copy all properties that need to be updated here!

                    // No need to call Attach/Modified/UpdateRange; the tracking handles the update.
                }
                else
                {
                    // INSERT: Record does not exist.
                    entitiesToAdd.Add(incomingEntity);

                    // Ensure Guid is empty for creation.
                    if (incomingEntity.Id != Guid.Empty)
                    {
                        incomingEntity.Id = Guid.Empty;
                    }
                }
            }

            // 5. Apply Inserts.
            if (entitiesToAdd.Any())
            {
                _context.StaffAttendance.AddRange(entitiesToAdd);
            }

            // 6. Save all changes (inserts and the tracked updates from step 4).
            await _context.SaveChangesAsync();

            return att;
        }

        public async Task<List<StudentAttendance>> CreateStudentAttendanceList(List<StudentAttendance> att)
        {
            if (att == null || !att.Any())
            {
                return new List<StudentAttendance>();
            }

            var incomingStudentIds = att.Select(a => a.StudentId).Distinct().ToList();

            // 1. Fetch ALL matching existing records and keep them TRACKED.
            var existingRecords = await _context.StudentAttendance
                .Where(sa => incomingStudentIds.Contains(sa.StudentId))
                .ToListAsync(); // <-- These entities are now TRACKED by the context

            // 2. Prepare containers for entities that definitely need to be added.
            var entitiesToAdd = new List<StudentAttendance>();

            // 3. Iterate and process: Update Tracked Entities or Mark for Insert.
            foreach (var incomingEntity in att)
            {
                // Find a match based on the composite key (StudentId, Date, Session)
                var matchingExisting = existingRecords.FirstOrDefault(er =>
                    er.StudentId == incomingEntity.StudentId &&
                    er.AttendanceDate.Date == incomingEntity.AttendanceDate.Date &&
                    er.Session == incomingEntity.Session);

                if (matchingExisting != null)
                {
                    // UPDATE: Record exists and is ALREADY tracked.

                    // **CRITICAL FIX:** Copy the new values from the incoming entity
                    // to the entity that is already tracked by the DbContext.
                    matchingExisting.Session = incomingEntity.Session;
                    matchingExisting.Remarks = incomingEntity.Remarks;
                    // ... copy all other properties you need to update ...

                    // We do NOT call Attach/Update/Modified here, because the tracking entity 
                    // is automatically marked as Modified when its properties change.
                }
                else
                {
                    // INSERT: Record does not exist.
                    entitiesToAdd.Add(incomingEntity);

                    // Ensure Guid is empty for creation.
                    if (incomingEntity.Id != Guid.Empty)
                    {
                        incomingEntity.Id = Guid.Empty;
                    }
                }
            }

            // 4. Apply Inserts.
            if (entitiesToAdd.Any())
            {
                _context.StudentAttendance.AddRange(entitiesToAdd);
            }

            // NOTE: The updates are handled by the change tracking in Step 3, 
            // so we don't need a separate loop for entitiesToUpdate.

            // 5. Save all changes (inserts and tracked updates).
            await _context.SaveChangesAsync();

            return att;
        }
    }
}
