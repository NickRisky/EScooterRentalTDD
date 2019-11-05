using ScooterRentalLibrary.Interfaces;
using ScooterRentalLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalLibrary
{
    public class RentalCompany : IRentalCompany
    {
        public string Name => name;
        private string name { get; set; }
        private IRepository _repository { get; set; }
        private IRentalService _rentalService { get; set; }
        private IReportService _reportService { get; set; }
        
        public RentalCompany(string newName, IRepository repository, IRentalService rentalService, IReportService reportService)
        {
            name = newName;
            this._repository = repository;
            this._rentalService = rentalService;
            this._reportService = reportService;
        }
        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            var result = _reportService.CalculateIncome(includeNotCompletedRentals, year);
            return result;
        }

        public void StartRent(string id)
        {
            _rentalService.Rent(id);
        }

        public decimal EndRent(string id)
        {
            return _rentalService.StopRent(id);
        }
    }
}
