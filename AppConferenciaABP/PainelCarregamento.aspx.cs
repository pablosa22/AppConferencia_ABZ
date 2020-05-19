using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class PainelCarregamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            this.GridView1.DataSource = nn.ListarCarregamentoParaConferencia();
            this.GridView1.DataBind();            
            this.GridView2.DataSource = nn.ListarItensPainelComDivergencia();
            this.GridView2.DataBind();

        }
    }
}