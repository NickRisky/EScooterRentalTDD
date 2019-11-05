using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalLibrary.Interfaces
{
    public interface IReportService
    {
        /// <summary> 
        /// Calculate rental company income from all rentals
        /// </summary> 
        /// <param name="includeNotCompletedRentals">Include not completed rentals(true/false)</param>         
        /// <returns>total sum</returns>
        decimal CalculateIncome(bool includeNotCompletedRentals, int? year);        
    }
}
