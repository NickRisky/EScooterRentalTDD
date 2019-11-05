using ScooterRentalLibrary.Entities;
using ScooterRentalLibrary.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScooterRentalLibrary.Services
{
    //Mock Repository for testing
    public sealed class MockRepository : IRepository
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static MockRepository() { }
        private MockRepository() 
        {
            // Mock data for test            
            AddScooter("1", 10);
            AddScooter("2", 12);
            AddScooter("3", 15);           
        }
        public static MockRepository Instance { get; } = new MockRepository();
        private static int NextId;
        private ConcurrentDictionary<int, RentalRecord> RentalRecords { get; set; } = new ConcurrentDictionary<int, RentalRecord>();
        private ConcurrentDictionary<string, Scooter> ScootersContainer { get; set; } = new ConcurrentDictionary<string, Scooter>();

        public IList<Scooter> GetScooters()
        {                    
            return ScootersContainer.Select(kvp => kvp.Value).ToList();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (!ScootersContainer.ContainsKey(id))
            {
                ScootersContainer.TryAdd(id, new Scooter(id, pricePerMinute));
            }
        }

        public Scooter GetScooterById(string scooterId)
        {
            if (string.IsNullOrWhiteSpace(scooterId))
                throw new ArgumentNullException(nameof(scooterId));
            if (ScootersContainer.TryGetValue(scooterId, out var result))
            {
                return result;
            }
            return null;
        }

        public void RemoveScooter(string scooterId)
        {
            ScootersContainer.TryRemove(scooterId, out var result);
        }

        public IList<RentalRecord> GetRentalRecords() => RentalRecords?.Select(kvp => kvp.Value).ToList();

        public bool AddRentalRecord(Scooter scooter)
        {
            if (RentalRecords.TryAdd(Interlocked.Increment(ref NextId), new RentalRecord(scooter)))
            {
                scooter.IsRented = true;
                return true;
            }
            return false;
        }

        public RentalRecord GetRentalRecordById(int id)
        {
            if (RentalRecords.TryGetValue(id, out var result))
            {
                return result;
            }
            return null;
        }

        public IList<RentalRecord> GetRecordsByScooterId(string scooterId) => RentalRecords?.Values.Where(a => a.ScooterId == scooterId).ToList();
    }
}
