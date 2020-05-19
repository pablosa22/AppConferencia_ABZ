using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppConferenciaABP
{
    public class Corte
    {        
        public long NumCar { get; set; }
        public long Pedido { get; set; }
        public int CodFunc { get; set; }
        public string Conferente { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal QtCorte { get; set; }
        public decimal QtIncluir { get; set; }
    }
}