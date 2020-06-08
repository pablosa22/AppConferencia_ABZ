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
            DesabilitarOpcaoAjudantes();
        }
        
        protected void btPesquisar_Click(object sender, EventArgs e)
        {
            ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
            int matricula = string.IsNullOrEmpty(TextBoxCodConferente.Text) ? 0 : Convert.ToInt32(TextBoxCodConferente.Text);
            long numero = string.IsNullOrEmpty(TextBoxNumero.Text) ? 0 : Convert.ToInt32(TextBoxNumero.Text);
            int opcao = Convert.ToInt32(DropDownList1.SelectedItem.Value);
            int qtlinhas = TotalDeLinhasConferencia(numero, opcao);
            int qtIniciado = CarregamentoIniciado(numero, opcao);

            if (matricula != 0 && opcao == 2 && qtlinhas > 0) //cupom
            {
                /*
                lbConferente.Text = nn.ConfirmaMatricula(matricula).Nome;
                lbPedido.Text = "Localizado com sucesso!";
                HabilitarBotoes();
                */
            }
            else if (matricula != 0 && opcao == 3 && qtlinhas > 0)//pedido
            {
                 /*
                lbConferente.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarBotoes();
                lbPedido.Text = "Localizado com sucesso!";
                 */
            }
            else if (matricula != 0 && opcao == 4 && qtlinhas > 0) // carga
            {
                lbConferente.Text = nn.ConfirmaMatricula(matricula).Nome;
                lbPedido.Text = "Localizado com sucesso!";
                if (qtIniciado == 0)
                {
                    HabilitarBotoes();
                    btFinalizar.Visible = false;
                }
                else 
                {                    
                    HabilitarOpcaoAjudantes();
                    btConfirmar.Visible = false;
                    btFinalizar.Visible = true;
                    lbPedido.Text = "Localizado com sucesso!";
                }
            }
            else
            {
                lbPedido.Visible = true;
                lbPedido.Text = "Número não foi localizado ou Já Finalizado!";
            }

        }

        protected void DesabilitarOpcaoAjudantes() 
        {
            TextBoxAJ1.Visible = false;
            lb1.Visible = false;
            bt1.Visible = false;
            Txt1.Visible = false;

            TextBoxAJ2.Visible = false;
            Label1.Visible = false;
            bt2.Visible = false;
            Txt2.Visible = false;

            TextBoxAJ3.Visible = false;
            Label2.Visible = false;
            bt3.Visible = false;
            Text3.Visible = false;

            TextBoxAJ4.Visible = false;
            Label3.Visible = false;
            bt4.Visible = false;
            Text4.Visible = false;

            TextBoxAJ5.Visible = false;
            Label4.Visible = false;
            bt5.Visible = false;
            Text5.Visible = false;

            TextBoxAJ6.Visible = false;
            Label5.Visible = false;
            bt6.Visible = false;
            Text6.Visible = false;

            TextBoxAJ7.Visible = false;
            Label6.Visible = false;
            bt7.Visible = false;
            Text7.Visible = false;

            TextBoxAJ8.Visible = false;
            Label7.Visible = false;
            bt8.Visible = false;
            Text8.Visible = false;

            TextBoxAJ9.Visible = false;
            Label8.Visible = false;
            bt9.Visible = false;
            Text9.Visible = false;

            TextBoxAJ10.Visible = false;
            Label9.Visible = false;
            bt10.Visible = false;
            Text10.Visible = false;

            LabelAtraso.Visible = false;
            TextBoxObs.Visible = false;
        }

        protected void HabilitarOpcaoAjudantes() 
        {
            TextBoxAJ1.Visible = true;
            lb1.Visible = true;
            bt1.Visible = true;
            Txt1.Visible = true;

            TextBoxAJ2.Visible = true;
            Label1.Visible = true;
            bt2.Visible = true;
            Txt2.Visible = true;

            TextBoxAJ3.Visible = true;
            Label2.Visible = true;
            bt3.Visible = true;
            Text3.Visible = true;

            TextBoxAJ4.Visible = true;
            Label3.Visible = true;
            bt4.Visible = true;
            Text4.Visible = true;

            TextBoxAJ5.Visible = true;
            Label4.Visible = true;
            bt5.Visible = true;
            Text5.Visible = true;

            TextBoxAJ6.Visible = true;
            Label5.Visible = true;
            bt6.Visible = true;
            Text6.Visible = true;

            TextBoxAJ7.Visible = true;
            Label6.Visible = true;
            bt7.Visible = true;
            Text7.Visible = true;

            TextBoxAJ8.Visible = true;
            Label7.Visible = true;
            bt8.Visible = true;
            Text8.Visible = true;

            TextBoxAJ9.Visible = true;
            Label8.Visible = true;
            bt9.Visible = true;
            Text9.Visible = true;

            TextBoxAJ10.Visible = true;
            Label9.Visible = true;
            bt10.Visible = true;
            Text10.Visible = true;

            LabelAtraso.Visible = true;
            TextBoxObs.Visible = true;
        }

        protected void bt1_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ1.Text) ? 0 : Convert.ToInt32(TextBoxAJ1.Text);
            if (matricula != 0) 
            { 
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Txt1.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt2_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ2.Text) ? 0 : Convert.ToInt32(TextBoxAJ2.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Txt2.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt3_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ3.Text) ? 0 : Convert.ToInt32(TextBoxAJ3.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text3.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt4_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ4.Text) ? 0 : Convert.ToInt32(TextBoxAJ4.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text4.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt5_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ5.Text) ? 0 : Convert.ToInt32(TextBoxAJ5.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text5.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt6_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ6.Text) ? 0 : Convert.ToInt32(TextBoxAJ6.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text6.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt7_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ7.Text) ? 0 : Convert.ToInt32(TextBoxAJ7.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text7.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt8_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ8.Text) ? 0 : Convert.ToInt32(TextBoxAJ8.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text8.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt9_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ9.Text) ? 0 : Convert.ToInt32(TextBoxAJ9.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text9.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
            }
        }

        protected void bt10_Click(object sender, EventArgs e)
        {
            int matricula = string.IsNullOrEmpty(TextBoxAJ10.Text) ? 0 : Convert.ToInt32(TextBoxAJ10.Text);
            if (matricula != 0)
            {
                ServiceReference3.WebService1SoapClient nn = new ServiceReference3.WebService1SoapClient();
                Text10.Text = nn.ConfirmaMatricula(matricula).Nome;
                HabilitarOpcaoAjudantes();
                btFinalizar.Visible = true;
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
                /*
                nn.ConfirmaConferenciaAutomatica(numero, matricula, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Iniciado e Finalizado conferencia! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
                */
            }
            else if (matricula != 0 && opcao == 3)//pedido
            {
                /*
                nn.ConfirmaConferenciaAutomatica(numero, matricula, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Iniciado a conferencia! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
                */
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
            long numero = string.IsNullOrEmpty(TextBoxNumero.Text) ? 0 : Convert.ToInt32(TextBoxNumero.Text);
            int opcao = Convert.ToInt32(DropDownList1.SelectedItem.Value); 

            int conferente = string.IsNullOrEmpty(TextBoxCodConferente.Text) ? 0 : Convert.ToInt32(TextBoxCodConferente.Text);
            int aj1 = string.IsNullOrEmpty(TextBoxAJ1.Text) ? 0 : Convert.ToInt32(TextBoxAJ1.Text);
            int aj2 = string.IsNullOrEmpty(TextBoxAJ2.Text) ? 0 : Convert.ToInt32(TextBoxAJ2.Text);
            int aj3 = string.IsNullOrEmpty(TextBoxAJ3.Text) ? 0 : Convert.ToInt32(TextBoxAJ3.Text);
            int aj4 = string.IsNullOrEmpty(TextBoxAJ4.Text) ? 0 : Convert.ToInt32(TextBoxAJ4.Text);
            int aj5 = string.IsNullOrEmpty(TextBoxAJ5.Text) ? 0 : Convert.ToInt32(TextBoxAJ5.Text);
            int aj6 = string.IsNullOrEmpty(TextBoxAJ6.Text) ? 0 : Convert.ToInt32(TextBoxAJ6.Text);
            int aj7 = string.IsNullOrEmpty(TextBoxAJ7.Text) ? 0 : Convert.ToInt32(TextBoxAJ7.Text);
            int aj8 = string.IsNullOrEmpty(TextBoxAJ8.Text) ? 0 : Convert.ToInt32(TextBoxAJ8.Text);
            int aj9 = string.IsNullOrEmpty(TextBoxAJ9.Text) ? 0 : Convert.ToInt32(TextBoxAJ9.Text);
            int aj10 = string.IsNullOrEmpty(TextBoxAJ10.Text) ? 0 : Convert.ToInt32(TextBoxAJ10.Text);
            string obs = TextBoxObs.Text;

            if (conferente != 0 && opcao == 2) //cupom
            {
                /*
                nn.FinalizarConferenciaAutomatica(numero, opcao);                
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Conferencia finalizada! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
                */
            }
            else if (conferente != 0 && opcao == 3)//pedido
            {
                /*
                nn.FinalizarConferenciaAutomatica(numero, opcao);
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Conferencia finalizada! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
                */
            }
            else if (conferente != 0 && opcao == 4) // carga
            {
                nn.FinalizarConferenciaAutomatica(numero, opcao);
                nn.IncluirAjudantes(numero, conferente, aj1, aj2, aj3, aj4, aj5, aj6, aj7, aj8, aj9, aj10, obs);
                this.GridView2.DataSource = nn.ListaPedidosParaConferencia(ordemConf);
                this.GridView2.DataBind();
                String mensagem1 = "Conferencia finalizada! de N° " + numero;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MensagemDeAlert", "alert('" + mensagem1 + "');", true);
                LimparCampos();
                DesabilitarOpcaoAjudantes();
                DesabilitarBotoes();
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
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(NUMPED)qt FROM PCPEDC C WHERE NUMNOTA ='{numped}' AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120 AND DTFINALSEP IS NULL", cnn);
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
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(NUMPED)QT FROM PCPEDC WHERE NUMPED ='{numped}' AND POSICAO NOT IN('C') AND DTFINALSEP IS NULL ", cnn);
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
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(DISTINCT NUMCAR)qt FROM PCPEDC WHERE NUMCAR ='{numped}' AND POSICAO NOT IN ('C') AND DTFINALSEP IS NULL ", cnn);
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


        protected int CarregamentoIniciado(long numped, int opcao)
        {
            int TotLinhas = 0;
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            try
            {
                cnn.Open();
                if (opcao == 2)
                {
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(NUMPED)qt FROM PCPEDC C WHERE NUMNOTA ='{numped}' AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120 AND DTINICIALSEP IS NOT NULL AND DTFINALSEP IS NULL", cnn);
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
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(NUMPED)QT FROM PCPEDC WHERE NUMPED ='{numped}' AND POSICAO NOT IN('C') AND DTINICIALSEP IS NOT NULL AND DTFINALSEP IS NULL ", cnn);
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
                    OracleCommand cmd = new OracleCommand($"SELECT COUNT(DISTINCT NUMCAR)qt FROM PCPEDC WHERE NUMCAR ='{numped}' AND POSICAO NOT IN ('C') AND DTINICIALSEP IS NOT NULL AND DTFINALSEP IS NULL ", cnn);
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