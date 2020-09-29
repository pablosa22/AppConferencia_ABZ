using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class PainelConferencia1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            this.GridView1.DataSource = nn.ListaPedidosParaConferencia(99);
            this.GridView1.DataBind();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.Cells[5].Text == "SIM")
                {
                    row.Cells[11].BackColor = ColorTranslator.FromHtml("#D2691E");
                }
            }
        }
    }
}