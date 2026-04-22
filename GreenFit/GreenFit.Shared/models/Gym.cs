using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Shared.Models
{
    public class Gym
    {
        public int Id { get; set; }
        public string name { get; set; }
        public  string latitude { get; set; }
        public string longitude { get; set; }
        public String[] attrezzi { get; set; } = new string[0];

        public String[] immagini { get; set; } = new String[0];

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
