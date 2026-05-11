using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CopaApi.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int EstadioId { get; set; }//FK

        public Estadio EstadioIdNavegacao { get; set; }

        // N:N
        public List<JogoSelecao> JogoSelecoes { get; set; }
            = new List<JogoSelecao>();
    }
}