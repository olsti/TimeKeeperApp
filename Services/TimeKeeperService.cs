using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using NPoco;
using NPoco.Linq;
using TimeKeeperApp.Data;
using TimeKeeperApp.Models;

namespace TimeKeeperApp.Services
{
    public class TimeKeeperService(DbAcccess dbAccess)
    {
        private readonly DbAcccess DbAcccess = dbAccess;


        public async Task<List<TimeRecord>> GetTimeRecordsAsync()
        {
            IQueryProvider<TimeRecord> query = DbAcccess.DB.Query<TimeRecord>();

            query = query.OrderByDescending(i => i.UpdatedAt);

            var list = await query.ToListAsync();
            list.ForEach(record => record = EvaluateRecord(record));
            return list;
        }

        public async Task<List<TimeRecord>> GetTimeRecordAsync(DateTime from, DateTime until)
        {
            IQueryProvider<TimeRecord> query = DbAcccess.DB.Query<TimeRecord>();

            query.Where(i => i.UpdatedAt >= from && i.UpdatedAt <= until);

            query = query.OrderByDescending(i => i.UpdatedAt);

            var list = await query.ToListAsync();
            list.ForEach(record => record = EvaluateRecord(record));
            return list;
        }

        private static TimeRecord EvaluateRecord(TimeRecord record)
        {
            if (record.EndTime < record.StartTime)
            {
                //invalid value
                record.IsValid = false;
                return record;
            }
            record.WorkTime = (record.EndTime - record.StartTime).TotalHours - record.BreakTime;
            return record;
        }

        public async Task<TimeRecord> AddAsync(TimeRecord record)
        {
            record.Id = Guid.NewGuid();
            record.CreatedAt = DateTime.Now;
            record.UpdatedAt = DateTime.Now;
            record.IsValid = record.EndTime >= record.StartTime; //same time is allowed in case of a breaktime record
            await DbAcccess.DB.InsertAsync(record);
            return record;
        }

        public async Task<TimeRecord> UpdateAsync(TimeRecord record)
        {
            using var transaction = DbAcccess.DB.GetTransaction();
            var existing = await DbAcccess.DB.Query<TimeRecord>().Where(r => r.Id == record.Id).SingleOrDefaultAsync() ?? throw new Exception("TimeRecord does not exist, update not possible");
            existing.UpdatedAt = DateTime.Now;
            existing.StartTime = record.StartTime;
            existing.EndTime = record.EndTime;
            existing.BreakTime = record.BreakTime;
            existing.Note = record.Note;
            record.IsValid = record.EndTime >= record.StartTime; //same time is allowed in case of a breaktime record
            await DbAcccess.DB.UpdateAsync(existing);
            transaction.Complete();

            return existing;
        }

        public async Task InvalidateRecordAsync(Guid recordId)
        {
            using var transaction = DbAcccess.DB.GetTransaction();
            var existing = await DbAcccess.DB.Query<TimeRecord>().Where(r => r.Id == recordId).SingleOrDefaultAsync() ?? throw new Exception("TimeRecord does not exist, update not possible");
            existing.IsValid = false;
            await DbAcccess.DB.UpdateAsync(existing, rc => rc.IsValid);
            transaction.Complete();
        }
    }
}