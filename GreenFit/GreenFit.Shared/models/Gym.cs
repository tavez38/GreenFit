using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GreenFit.Shared.Models
{
    [Index(nameof(latitude), nameof(longitude), IsUnique = true)]
    public class Gym
    {
        [Key]
        public int Id { get; set; }

        public string name { get; set; }

        public  string latitude { get; set; }

        public string longitude { get; set; }

        public string[] attrezzi { get; set; } = new string[0];

        public String[] immagini { get; set; } = new String[0];

        public Gym(string name, string latitude, string longitude, string[] attrezzi)
        {
            
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
            this.attrezzi = attrezzi;
        }

        public Gym() { }
    }
}
