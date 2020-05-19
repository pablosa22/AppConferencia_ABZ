using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppConferenciaABP
{
    public class Carregamento
    {       
        public DateTime DtMontagem { get; set; }
        public long NumCar { get; set; }
        public int CodFunc { get; set; }
        public string Conferente { get; set; }
        public int CodVeiculo { get; set; }
        public string Placa { get; set; }
        public string Destino { get; set; }
        public int CodMot { get; set; }
        public int QtPedidos { get; set; }
        public int QtConcluido { get; set; }
    }
}