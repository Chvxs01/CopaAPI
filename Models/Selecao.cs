using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaHAS.Models;

namespace CopaApi.Models
{
    public class Selecao
    {
        public int Id { get; set; }
        public string Pais { get; set; } = string.Empty;

        //1:N
        public List <Jogador> Jogadores { get; set; }

        //1:1
        public Tecnico Tecnico { get; set; }

        //N:N
        public List<JogoSelecao> JogoSelecoes { get; set; }
            = new List<JogoSelecao>();
    }

}