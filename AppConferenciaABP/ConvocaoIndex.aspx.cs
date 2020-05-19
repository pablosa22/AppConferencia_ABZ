using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class ConvocaoIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            int matricula = Convert.ToInt32(Request.QueryString["mat"]);
            matricula = 912;
            long pedido = PedidoAtribuido(matricula);
            if(pedido != 0)
            {
                Response.Redirect("ConvocacaoAtiva.aspx");
            }
        }

        protected long PedidoAtribuido(int matricula)
        {
            long pedido = 0;
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT NUMPED FROM PCPEDC WHERE CODFUNCEXPARQOL ="+matricula, cnn);
                cmd.Parameters.Add(new OracleParameter("CODFUNCEXPARQOL", matricula));
                OracleDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {                    
                    pedido = Convert.ToInt64(rdr["NUMPED"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return pedido;
        }


    }
}