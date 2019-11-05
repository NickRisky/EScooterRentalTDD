using ScooterRentalLibrary.Entities;
using ScooterRentalLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRentalLibrary
{    
    public sealed class ScooterService : IScooterService
    {
        IRepository repository;        
        public ScooterService(IRepository repository) 
        {
            this.repository = repository;
        }

        public void AddScooter(string id, decimal pricePerMinute) => repository.AddScooter(id, pricePerMinute);

        public Scooter GetScooterById(string scooterId) => repository.GetScooterById(scooterId);

        public IList<Scooter> GetScooters() => repository.GetScooters();

        public void RemoveScooter(string id) => repository.RemoveScooter(id);
    }
}
