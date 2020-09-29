using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class PainelCargaMix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            this.GridView1.DataSource = nn.ListaCargasMix();
            this.GridView1.DataBind();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.Cells[6].Text == "SIM")
                {
                    row.Cells[6].BackColor = ColorTranslator.FromHtml("#D2691E");
                }
            }


        }

        //selecionar linha na grid
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        //continuação...selecionar linha na grid
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    String numcar = row.Cells[0].Text;
                    String conferido = row.Cells[6].Text;                 
                    long carregamento = Convert.ToInt64(numcar);

                    string status = "N";
                    if (conferido.Equals("N&#195;O")) 
                    {
                        status = "S";
                        row.Cells[6].BackColor = ColorTranslator.FromHtml("#D2691E");
                    }                  
                    MudarStatusDeConferencia(carregamento, status);
                    this.GridView1.DataSource = nn.ListaCargasMix();
                    this.GridView1.DataBind();                   
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click na linha para mudar a conferencia.";
                }
            }            
        }

        protected void MudarStatusDeConferencia(long numcar, string status) 
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try 
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand($"UPDATE PCCARREG SET BALCAOBAIXADO = '{status}' WHERE NUMCAR = {numcar}", cnn);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                throw ex; 
            }
            finally 
            {
                cnn.Close();
            }
        }
    }
}