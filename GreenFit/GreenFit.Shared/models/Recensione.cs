
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFit.Shared.Models
{
    public class Recensione
    {
        public String titolo { get; set; }
        public String descrizione { get; set; }
        public String mail { get; set; }
        public int idPalestra { get; set; }
        public Valutazioni voto { get; set; }

        public Recensione(String titolo, String descrizione, String utente, int gym, Valutazioni voto) { 
            this.titolo = titolo;
            this.descrizione = descrizione;
            this.mail = utente;
            this.idPalestra = gym;
            this.voto = voto;
        }
    }
}
