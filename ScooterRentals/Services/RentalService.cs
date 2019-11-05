using ScooterRentalLibrary.Entities;
using ScooterRentalLibrary.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace ScooterRentalLibrary
{
    public class RentalService : IRentalService
    {

        IRepository repository;
        public RentalService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Rent(string scooterId)
        {
            if (string.IsNullOrWhiteSpace(scooterId))
                throw new ArgumentNullException(nameof(scooterId));

            var scooter = repository.GetScooterById(scooterId);            
            if (!scooter.IsRented)
            {
                return repository.AddRentalRecord(scooter);   
            }
            return false;
        }

        public decimal StopRent(string scooterId)
        {
            if (string.IsNullOrWhiteSpace(scooterId))
                throw new ArgumentNullException();
            var scooter = repository.GetScooterById(scooterId);
            if (scooter == null)
                throw new ScooterNotFoundException(scooterId);

            if (!scooter.IsRented)
                throw new ScooterNotRentedException(scooterId);
            var record = repository.GetRecordsByScooterId(scooterId)?.LastOrDefault(a => a.Total == 0m);
            if (record == null)
                throw new RentalRecordNotFoundException(scooterId);
            record.EndTime = DateTime.Now;
            record.Total = RentalRecord.GetPriceForRent(record);                    
            scooter.IsRented = false;
            return record.Total;
        }

        public IList<RentalRecord> GetRentalRecords() => repository.GetRentalRecords();
    }
}
