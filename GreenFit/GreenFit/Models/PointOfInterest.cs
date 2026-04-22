using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Models
{
    internal class PointOfInterest
    {
        string latitude { get; set; }
        string longitude { get; set; }
        string id { get; set; }

        public PointOfInterest(string latitude, string longitude, string id)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            this.id = id;
        }
    }
}
