using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class IncluirCarga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DesabilitarBotoes();
        }

        protected void DesabilitarBotoes()
        {
            btIncluirNova.Visible = false;
            TextBoxNovoCar.Visible = false;
            TextBoxSeq.Visible = false;
            TextBoxObs.Visible = false;
            btExcluir.Visible = false;
            btConfirmar.Visible = false;
        }

        //Carregamento não existe ou já faturado
        protected int ExisteCarregamentoDeRef(long numcar)
        {
            int ExisteCarregamento = 0;
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT COUNT(NUMCAR)QT FROM PCCARREG C WHERE C.NUMCAR =:numcar AND C.DTFAT IS NULL", cnn);
                cmd.Parameters.Add(new OracleParameter("NUMCAR", numcar));
                OracleDataReader rdr = cmd.ExecuteReader();

                Produto produto = null;
                while (rdr.Read())
                {
                    produto = new Produto();
                    ExisteCarregamento = Convert.ToInt32(rdr["QT"].ToString());
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
            return ExisteCarregamento;
        }

        //Validar se a sequencia de entrega já foi iniciada
        protected int ValidarSeqEntrega(long numcar, int numseq)
        {
            int ValidarSeqEntrega = 0;
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT COUNT(*)QT FROM PCPEDC C WHERE C.NUMCAR =:numcar AND C.NUMSEQENTREGA =:numseq AND CODFUNCSEP IS NULL AND C.DTFAT IS NULL", cnn);
                cmd.Parameters.Add(new OracleParameter("NUMCAR", numcar));
                cmd.Parameters.Add(new OracleParameter("NUMSEQENTREGA", numcar));
                OracleDataReader rdr = cmd.ExecuteReader();

                Produto produto = null;
                while (rdr.Read())
                {
                    produto = new Produto();
                    ValidarSeqEntrega = Convert.ToInt32(rdr["QT"].ToString());
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
            return ValidarSeqEntrega;
        }

        protected void btConfirmar_Click(object sender, EventArgs e)
        {
            long refCarregamento = string.IsNullOrEmpty(TextBoxRefCar.Text) ? 0 : Convert.ToInt64(TextBoxRefCar.Text);
            long novoCarregamento = string.IsNullOrEmpty(TextBoxNovoCar.Text) ? 0 : Convert.ToInt64(TextBoxNovoCar.Text);
            int sequencia = string.IsNullOrEmpty(TextBoxSeq.Text) ? 0 : Convert.ToInt32(TextBoxSeq.Text);
            int achouCarregamento = ExisteCarregamentoDeRef(refCarregamento);
            int validaSeq = ValidarSeqEntrega(refCarregamento, sequencia);
            string obs = TextBoxObs.Text;

            if (refCarregamento == 0 || novoCarregamento == 0 || sequencia == 0)
            {
                String mensagem = "Erro! Verifique os demais campos preenchidos";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem + "');", true);
            }
            else if (achouCarregamento == 0)
            {
                String mensagem = "Erro! Carregamento informado como referência já faturado ou não existe.";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem + "');", true);
            }
            else if (validaSeq == 0)
            {
                String mensagem = "Erro! Carregamento pode ser incluído sequência já iniciada.";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem + "');", true);
            }
            else if ((refCarregamento != 0 || novoCarregamento != 0 || sequencia != 0) &&
                        (achouCarregamento != 0) && (validaSeq != 0))
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                nn.InserirNovaCargaSeqEntrega(novoCarregamento, refCarregamento, sequencia, obs);
                String mensagem = "Favor confirmar no painel de carregamento se o mesmo foi adicionado na sequência";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem + "');", true);
            }
        }
    }
}