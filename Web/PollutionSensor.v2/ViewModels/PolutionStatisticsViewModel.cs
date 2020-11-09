using System;
using System.ComponentModel.DataAnnotations;

namespace PollutionSensor.v2.ViewModels
{
    public class PollutionStatisticsViewModel
    {
        public Int64 Id { get; set; }
        public int Hour { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string SSID { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal PM10 { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]

        public decimal PM25 { get; set; }

        public int TotalEntries { get; set; }

        public string Pm25Color
        {
            get
            {
                if (PM25 < 60)
                    return "green";
                if (PM25 >= 60 && PM25 < 100)
                    return "orange";
                return "red";
            }
        }

        public string Pm10Color
        {
            get
            {
                if (PM10 < 60)
                    return "green";
                if (PM10 >= 60 && PM10 < 100)
                    return "orange";
                return "red";
            }
        }
    }
}
