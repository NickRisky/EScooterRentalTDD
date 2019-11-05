using ScooterRentalLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalLibrary.Interfaces
{
    public interface IRentalService
    {
        /// <summary> 
        /// Rent a scooter 
        /// </summary> 
        /// <param name="id">Unique ID of the scooter</param>         
        /// <returns>result of success rent</returns>
        bool Rent(string id);

        /// <summary>
        /// Stop Rent 
        /// </summary> 
        /// <param name="id">Unique ID of the scooter</param> 
        /// <returns>total rental price of trip</returns>
        decimal StopRent(string id);

        /// <summary> 
        /// List of rental records
        /// </summary> 
        /// <returns>Return a list of available rentals, completed and not</returns> 
        IList<RentalRecord> GetRentalRecords();
                
    }
}
