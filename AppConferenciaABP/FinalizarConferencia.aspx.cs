using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    
    public partial class FinalizarConferencia : System.Web.UI.Page
    {
        int ordemConf = 98;
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
            this.GridView2.DataBind();
            DesabilitarBotoes();
        }
        
        protected void btPesquisar_Click(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            int matricula = string.IsNullOrEmpty(TextBoxCodConferente.Text) ? 0 : Convert.ToInt32(TextBoxCodConferente.Text);
            long numero = string.IsNullOrEmpty(TextBoxNumero.Text) ? 0 : Convert.ToInt32(TextBoxNumero.Text);
            int opcao = Convert.ToInt32(DropDownList1.SelectedItem.Value);
            int qtlinhas = TotalDeLinhasConferencia(numero, opcao);

            if (matricula != 0 && opcao == 2 && qtlinhas > 0) //cupom
            {
                lbConferente.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarBotoes();
                lbPedido.Text = "Localizado com sucesso!";
            }
            else if (matricula != 0 && opcao == 3 && qtlinhas > 0)//pedido
            {
                lbConferente.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarBotoes();
                lbPedido.Text = "Localizado com sucesso!";
            }
            else if (matricula != 0 && opcao == 4 && qtlinhas > 0) // carga
            {
                lbConferente.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarBotoes();
                lbPedido.Text = "Localizado com sucesso!";
            }
            else
            {
                lbPedido.Visible = true;
                lbPedido.Text = "Número não foi localizado!";
            }

        }
        

        protected void DesabilitarBotoes()
        {
            lbConferente.Visible = false;
            btConfirmar.Visible = false;
            btFinalizar.Visible = false;
            lbPedido.Visible = false;
        }

        protected void HabilitarBotoes()
        {
            lbConferente.Visible = true;
            btConfirmar.Visible = true;
            btFinalizar.Visible = true;
            lbPedido.Visible = true;
        }

        protected void LimparCampos()
        {
            TextBoxNumero.Text = "";
            TextBoxCodConferente.Text = "";
        }

        protected void btConfirmar_Click(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            int matricula = string.IsNullOrEmpty(TextBoxCodConferente.Text) ? 0 : Convert.ToInt32(TextBoxCodConferente.Text);
            long numero = string.IsNullOrEmpty(TextBoxNumero.Text) ? 0 : Convert.ToInt32(TextBoxNumero.Text);
            int opcao = Convert.ToInt32(DropDownList1.SelectedItem.Value);
            if (matricula != 0 && opcao == 2) //cupom
            {
                nn.ConfirmaConferenciaAutomatica(numero, matricula, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Iniciado e Finalizado conferencia! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
            }
            else if (matricula != 0 && opcao == 3)//pedido
            {
                nn.ConfirmaConferenciaAutomatica(numero, matricula, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Iniciado a conferencia! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
            }
            else if (matricula != 0 && opcao == 4) // carga
            {
                nn.ConfirmaConferenciaAutomatica(numero, matricula, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Iniciado a conferencia! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
            }
        }

        
        protected void btFinalizar_Click(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            int matricula = string.IsNullOrEmpty(TextBoxCodConferente.Text) ? 0 : Convert.ToInt32(TextBoxCodConferente.Text);
            long numero = string.IsNullOrEmpty(TextBoxNumero.Text) ? 0 : Convert.ToInt32(TextBoxNumero.Text);
            int opcao = Convert.ToInt32(DropDownList1.SelectedItem.Value);
            if (matricula != 0 && opcao == 2) //cupom
            {
                nn.FinalizarConferenciaAutomatica(numero, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Conferencia finalizada! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
            }
            else if (matricula != 0 && opcao == 3)//pedido
            {
                nn.FinalizarConferenciaAutomatica(numero, opcao);
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Conferencia finalizada! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
            }
            else if (matricula != 0 && opcao == 4) // carga
            {
                nn.FinalizarConferenciaAutomatica(numero, opcao);
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Conferencia finalizada! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
            }
        }
        

        protected int TotalDeLinhasConferencia(long numped, int opcao)
        {
            int TotLinhas = 0;
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            try
            {
                cnn.Open();
                if (opcao == 2)
                {
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(NUMPED)qt FROM PCPEDC C WHERE NUMNOTA ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120", cnn);
                    OracleDataReader rdr = cmd.ExecuteReader();

                    Produto produto = null;
                    while (rdr.Read())
                    {
                        produto = new Produto();
                        TotLinhas = Convert.ToInt32(rdr["qt"].ToString());
                    }
                    rdr.Close();
                }
                else if (opcao == 3)
                {
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(NUMPED)QT FROM PCPEDC WHERE NUMPED ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C')", cnn);
                    OracleDataReader rdr = cmd.ExecuteReader();

                    Produto produto = null;
                    while (rdr.Read())
                    {
                        produto = new Produto();
                        TotLinhas = Convert.ToInt32(rdr["qt"].ToString());
                    }
                    rdr.Close();
                }
                else if (opcao == 4)
                {
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(DISTINCT NUMCAR)qt FROM PCPEDC WHERE NUMCAR ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN ('C') ", cnn);
                    OracleDataReader rdr = cmd.ExecuteReader();

                    Produto produto = null;
                    while (rdr.Read())
                    {
                        produto = new Produto();
                        TotLinhas = Convert.ToInt32(rdr["qt"].ToString());
                    }
                    rdr.Close();
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
            return TotLinhas;
        }        
    }
    
}