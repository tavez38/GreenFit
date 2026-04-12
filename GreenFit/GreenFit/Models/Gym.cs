using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Models
{
    internal class Gym
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Gym(int id, string name, string address, double latitude, double longitude)
        {
            Id = id;
            this.name = name;
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
