using System;

namespace PolutionSensor.v2.Models
{
    public class Entry
    {
        public long EntryId { get; set; }
        public double Pm10 { get; set; }
        public double Pm25 { get; set; }
        public string SSID { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime LocalDateTime
        {
            get
            {
                return InsertDateTime;
            }
        }

        public DateTime LocalDate
        {
            get
            {
                return InsertDateTime.Date;
            }
        }
    }
}
