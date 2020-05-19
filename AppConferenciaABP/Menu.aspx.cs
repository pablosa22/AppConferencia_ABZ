using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btEnviaPedidoConferencia_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("EnviaPedidoConferencia.aspx");
        }

        protected void btbtPainelAlmoxarifado_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("PainelAlmoxarifado.aspx");
        }

        protected void btbtPainelConferencia_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("PainelConferencia.aspx");
        }

        protected void btbtPainelFaturamento_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("PainelFaturamento.aspx");
        }

        protected void btGrafico_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("Grafico.aspx");
        }

        protected void btIndustria_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("PainelOSIndustria.aspx");
        }

        protected void btFaturarCarregamento_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("FaturarCarga.aspx");
        }

        protected void btConferenciaCarga_Onclik(object sender, EventArgs e)
        {
            Response.Redirect("PainelCarregamento.aspx");
        }

    }
}