using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVChargingStation.Domain.Entities
{
    public class Location : BaseEntity
    {
        // Station name = Station A, B, ..
        public string Name { get; set; }           
        public string Address { get; set; }


        // Kinh độ, vĩ độ (lưu trên map)
        public decimal Latitude { get; set; }   
        public decimal Longitude { get; set; }   

        // Navigation Property
        public ICollection<Station> Stations { get; set; } 
    }
}
