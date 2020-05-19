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
    public partial class PainelOSIndustria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int ac = Convert.ToInt32(Request.QueryString["acesso"]);
            if (Quantidade() > 0 && ac == 0)
            {
                Response.Redirect("ConvocacaoAtiva.aspx");
            }
            else
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                this.GridView1.DataSource = nn.ListarProducaoSolicitada();
                this.GridView1.DataBind();
            }            
        }

        //selecionar linha na grid
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Duplo click para excluir do painel";
            }
        }

        //continuação...selecionar linha na grid
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    String pedido = row.Cells[2].Text;
                    String codigoProduto = row.Cells[4].Text;
                    String Descricao = row.Cells[5].Text;

                    
                    long numped = Convert.ToInt64(pedido);
                    int codprod = Convert.ToInt32(codigoProduto);
                    ConferenciaAlmoxarifado(numped, codprod);
                    Atualizar();
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void ConferenciaAlmoxarifado(long numped, int codprod)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (codprod != 0 && numped != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDI SET CODFUNCCONF2 = 1 WHERE NUMPED =:numped AND CODPROD =:codprod", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                    cmd.Parameters.Add(new OracleParameter("CODPROD", codprod));
                    cmd.ExecuteNonQuery();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }


        public int Quantidade()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            int count = 0;
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT C.CODFILIAL, G.NUMCAR, C.NUMPED, SUBSTR(E.NOME,1,INSTR(E.NOME,' ')-1) RCA, P.CODPROD, P.DESCRICAO, SUM(NVL(I.QT, 0))QT, C.OBS, C.OBS1, C.OBS2 " +
                    " FROM PCPRODUT P, PCPEDC C, PCPEDI I, PCCARREG G, PCEMPR E WHERE P.CODPROD = I.CODPROD AND C.NUMPED = I.NUMPED AND E.MATRICULA = C.CODUSUR AND G.NUMCAR = C.NUMCAR AND C.CODFILIAL IN(1) " +
                    " AND I.CODFUNCCONF2 IS NULL AND P.CODPROD IN(6461, 6463, 6398, 5534) AND C.DATA > TRUNC(SYSDATE) - 130 AND C.DTINICIALSEP IS NOT NULL AND C.DTFINALSEP IS NULL " +
                    " GROUP BY C.CODFILIAL, I.TIPOENTREGA, G.NUMCAR, C.NUMPED, P.CODPROD, E.NOME, P.DESCRICAO, C.OBS, C.OBS1, C.OBS2 UNION ALL " +
                    "       SELECT C.CODFILIAL, G.NUMCAR, C.NUMPED, SUBSTR(E.NOME, 1, INSTR(E.NOME, ' ') - 1) RCA, P.CODPROD, P.DESCRICAO, SUM(NVL(I.QT, 0)) as qt, C.OBS, C.OBS1, C.OBS2 FROM PCPRODUT P, PCPEDC C, PCPEDI I, PCCARREG G, PCEMPR E " +
                    " WHERE P.CODPROD = I.CODPROD AND E.MATRICULA = C.CODUSUR AND C.NUMPED = I.NUMPED AND G.NUMCAR = C.NUMCAR AND C.CODFILIAL IN(1) AND I.CODFUNCCONF2 IS NULL AND P.CODPROD IN(5484, 5490) AND C.DATA > TRUNC(SYSDATE) - 130 AND C.DTINICIALSEP IS NOT NULL " +
                    " AND C.DTFINALSEP IS NULL GROUP BY C.CODFILIAL, I.TIPOENTREGA, G.NUMCAR, C.NUMPED, P.CODPROD, E.NOME, P.DESCRICAO, C.OBS, C.OBS1, C.OBS2 ", cnn);
                cmd.BindByName = true;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    count = count + 1;
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
            return count;
        }

        public void Atualizar()
        {
            Response.Redirect("PainelOSIndustria.aspx?acesso="+0);
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            this.GridView1.DataSource = nn.ListarProducaoSolicitada();
            this.GridView1.DataBind();
        }

    }
}