using ScooterRentalLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScooterRentalLibrary.Entities;

namespace ScooterRentalLibrary.Services
{
    public class ReportService : IReportService
    {        
        IRepository repository { get; set; }
        public ReportService(IRepository repository)
        {
            this.repository = repository;
        }
        public decimal CalculateIncome(bool includeNotCompletedRentals, int? year)
        {
            var records = repository.GetRentalRecords().ToList();
            //remove all records that not belongs to year param
            if (year != null)
                 records.RemoveAll(a => a.EndTime.Year != year);
            if (!includeNotCompletedRentals)
            {
                return records.Sum(a => a.Total);
            }   
            else
            {
                var sum = records.Sum(a => a.Total);
                foreach (var item in records.FindAll(a => a.Total == 0m))
                {
                    item.EndTime = DateTime.Now;
                    sum += RentalRecord.GetPriceForRent(item);
                }
                return sum;
            }            
        }        
    }
}
