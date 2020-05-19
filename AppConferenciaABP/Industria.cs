using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppConferenciaABP
{
    public class Industria
    {
        public int Filial { get; set; }
        public long NumCar { get; set; }
        public long Pedido { get; set; }        
        public string RCA { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Qt { get; set; }
        public string Obs { get; set; }
        public string Obs1 { get; set; }
        public string Obs2 { get; set; }
    }
}