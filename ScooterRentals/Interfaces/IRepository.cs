using ScooterRentalLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalLibrary.Interfaces
{
    public interface IRepository
    {
        IList<Scooter> GetScooters();
        void AddScooter(string id, decimal pricePerMinute);
        Scooter GetScooterById(string scooterId);
        void RemoveScooter(string scooterId);
        IList<RentalRecord> GetRentalRecords();
        bool AddRentalRecord(Scooter scooter);
        RentalRecord GetRentalRecordById(int id);
        IList<RentalRecord> GetRecordsByScooterId(string scooterId);
    }
}
