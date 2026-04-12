using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Models
{
    internal class Utente
    {
        public string? nome { get; set; }
        public string? cognome { get; set; }
        public string? email { get; set; }
        public bool isLoggedIn { get; set; }

        public Utente(bool isLoggedIn) { 
            this.isLoggedIn = isLoggedIn;
        }

        
    }
}
