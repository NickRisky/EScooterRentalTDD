using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalLibrary.Entities
{
    public class RentalRecord
    {
        /// <summary> 
        /// Unique ID of the scooter 
        /// </summary> 
        public string ScooterId { get; set; }
        /// <summary> 
        /// Rental start time
        /// </summary> 
        public DateTime StartTime { get; set; }
        /// <summary> 
        /// Rental stop time
        /// </summary> 
        public DateTime EndTime { get; set; }
        /// <summary> 
        /// Rental price per minute
        /// </summary> 
        public decimal Price { get; set; }
        /// <summary> 
        /// Total rental price
        /// </summary>         
        public decimal Total { get; set; }
        
        /// <summary> 
        /// Create new instance.
        /// </summary> 
        /// <param name="scooterId">ID of the scooter</param>      

        public RentalRecord(Scooter scooter)
        {
            ScooterId = scooter.Id;
            Price = scooter.PricePerMinute;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;            
        }

        public static decimal GetPriceForRent(RentalRecord record)
        {
            //if rental price more than 20 EUR, than stop counter (20 eur per day)
            var days = Math.Floor((record.EndTime - record.StartTime).TotalDays);
            var price = record.Price * (decimal)(record.EndTime - record.StartTime.AddDays(days)).TotalMinutes;
            if (price > 20m)
                price = 20m;
            price += (decimal)days * 20m;
            return price;
        }
    }

    [Serializable]
    class RentalRecordNotFoundException : Exception
    {
        public RentalRecordNotFoundException()
        {

        }

        public RentalRecordNotFoundException(string id)
            : base($"Rental record not found: {id}")
        {

        }
    }
}
