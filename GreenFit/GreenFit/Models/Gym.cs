using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Models
{
    internal class Gym
    {
        public int Id { get; set; }
        public string name { get; set; }
        public  string latitude { get; set; }
        public string longitude { get; set; }
        public AttrezziPalestre attrezzi { get; set; } = new AttrezziPalestre();

        public Gym(int id, string name, string latitude, string longitude)
        {
            Id = id;
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public Gym() { }
    }
}
