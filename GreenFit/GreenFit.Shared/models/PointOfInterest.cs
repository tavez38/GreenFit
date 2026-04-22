using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Models
{
    public class PointOfInterest
    {
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }


        public PointOfInterest(int id, double latitude, double longitude, string name)
        {
            this.id = id;
            this.latitude = latitude;
            this.longitude = longitude;
            this.name = name;
        }
    }
}
