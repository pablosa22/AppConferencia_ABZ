using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AppConferenciaABP
{
    /// <summary>
    /// Descrição resumida de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        //AppCelular lista itens pra conferir
        [WebMethod]
        public List<Produto> ListaItensParaConferencia(long numped)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Produto> list = new List<Produto>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT NUMSEQ, CODPROD, DESCRICAO,  QTUNIDADE, QTSEPARADA FROM (SELECT P.CODPROD, P.DESCRICAO, I.NUMSEQ, NVL(SUM(I.QT / E.QTUNIT), 0) AS QTUNIDADE, NVL(I.QTSEPARADA, 0)AS QTSEPARADA " +
                    " FROM PCPEDI I, PCPRODUT P, PCEMBALAGEM E WHERE I.CODPROD = P.CODPROD AND E.CODPROD = P.CODPROD AND I.CODFILIALRETIRA = E.CODFILIAL  AND I.NUMPED =:numped " +
                    " GROUP BY P.CODPROD, P.DESCRICAO, I.NUMSEQ, I.QTSEPARADA ) WHERE QTUNIDADE <> QTSEPARADA GROUP BY NUMSEQ, CODPROD, DESCRICAO, QTUNIDADE, QTSEPARADA ORDER BY 1, 3 ASC ", cnn); 
                cmd.BindByName = true;
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                OracleDataReader rdr = cmd.ExecuteReader();

                Produto produto = null;                               
                while (rdr.Read())
                {
                    produto = new Produto();

                    produto.Seq = Convert.ToInt32(rdr["NUMSEQ"]);
                    produto.Codigo = Convert.ToInt32(rdr["CODPROD"]);
                    produto.Descricao = rdr["DESCRICAO"].ToString();
                    produto.Qt = Convert.ToDecimal(rdr["QTUNIDADE"]);
                    produto.QtSep = Convert.ToDecimal(rdr["QTSEPARADA"]);
                    list.Add(produto);                    
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
            return list;
        }

        //Painel pedidos em processo de separação
        [WebMethod]
        public List<PainelConferencia> ListaPedidosParaConferencia(int ordemConf)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<PainelConferencia> list = new List<PainelConferencia>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT C.CODFILIAL, C.NUMPED, NVL(C.NUMNOTA,0)NOTA, C.CODCLI, SUBSTR(L.CLIENTE, 1, INSTR(L.CLIENTE,' ')-1 )||SUBSTR(L.CLIENTE, INSTR(L.CLIENTE, ' '), INSTR(L.CLIENTE, ' ',2))CLIENTE, " +
                    " L.UFRG, C.OBS, C.OBS1, C.OBS2, SUBSTR(E.NOME,1,INSTR(E.NOME,' ')-1)CONFERENTE, I.PERC FROM PCPEDC C, PCCLIENT L, PCEMPR E, " +
                    " (SELECT I.NUMPED, ROUND(((COUNT(I.QTSEPARADA) * 100) / COUNT(I.QT)), 2) || '%' PERC FROM PCPEDI I GROUP BY I.NUMPED)I WHERE C.CODCLI = L.CODCLI AND E.MATRICULA(+) = C.CODFUNCSEP " +
                    $" AND I.NUMPED = C.NUMPED  AND DATA > TRUNC(SYSDATE) - 120 AND ORDEMCONF = '{ordemConf}' AND C.DTFINALSEP IS NULL ORDER BY C.DTIMPORTACAO ASC", cnn);
                cmd.BindByName = true;                
                OracleDataReader rdr = cmd.ExecuteReader();

                PainelConferencia painelConferencia = null;

                while (rdr.Read())
                {
                    painelConferencia = new PainelConferencia();

                    painelConferencia.Filial = Convert.ToInt32(rdr["CODFILIAL"]);
                    painelConferencia.Pedido = Convert.ToInt64(rdr["NUMPED"]);
                    painelConferencia.Nota = Convert.ToInt32(rdr["NOTA"]);
                    painelConferencia.Codigo = Convert.ToInt32(rdr["CODCLI"]);
                    painelConferencia.Cliente = rdr["CLIENTE"].ToString();
                    painelConferencia.UF = rdr["UFRG"].ToString();
                    painelConferencia.Obs = rdr["OBS"].ToString();
                    painelConferencia.Obs1 = rdr["OBS1"].ToString();
                    painelConferencia.Obs2 = rdr["OBS2"].ToString();
                    painelConferencia.Conferente = rdr["CONFERENTE"].ToString();
                    painelConferencia.Conferido = rdr["PERC"].ToString();
                    list.Add(painelConferencia);
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
            return list;

        }

        //AppCelular lista itens finalizados de conferencia 
        [WebMethod]
        public List<Produto> ListaItensFinalizados(long numped)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Produto> list = new List<Produto>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT NUMSEQ, CODPROD, DESCRICAO,  QTUNIDADE, QTSEPARADA FROM (SELECT P.CODPROD, P.DESCRICAO, I.NUMSEQ, NVL(SUM(I.QT / E.QTUNIT), 0) AS QTUNIDADE, NVL(I.QTSEPARADA, 0)AS QTSEPARADA " +
                    " FROM PCPEDI I, PCPRODUT P, PCEMBALAGEM E WHERE I.CODPROD = P.CODPROD AND E.CODPROD = P.CODPROD AND I.CODFILIALRETIRA = E.CODFILIAL AND I.NUMPED =:numped " +
                    " GROUP BY P.CODPROD, P.DESCRICAO, I.NUMSEQ, I.QTSEPARADA ) WHERE QTUNIDADE = QTSEPARADA GROUP BY NUMSEQ, CODPROD, DESCRICAO, QTUNIDADE, QTSEPARADA ORDER BY 1, 3 ASC ", cnn);
                cmd.BindByName = true;
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                OracleDataReader rdr = cmd.ExecuteReader();

                Produto produto = null;

                while (rdr.Read())
                {
                    produto = new Produto();

                    produto.Seq = Convert.ToInt32(rdr["NUMSEQ"]);
                    produto.Codigo = Convert.ToInt32(rdr["CODPROD"]);
                    produto.Descricao = rdr["DESCRICAO"].ToString();
                    produto.Qt = Convert.ToDecimal(rdr["QTUNIDADE"]);
                    produto.QtSep = Convert.ToDecimal(rdr["QTSEPARADA"]);
                    list.Add(produto);
                   
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
            return list;
        }

        //AppCelular Valida usuario e matricula
        [WebMethod]
        public Pessoa ConfirmaAcesso(string usuario, int matricula)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            Pessoa pessoa = new Pessoa();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT MATRICULA, NOME, NOME_GUERRA FROM PCEMPR WHERE MATRICULA =:matricula AND NOME_GUERRA =:usuario", cnn);
                cmd.Parameters.Add(new OracleParameter("MATRICULA", matricula));
                cmd.Parameters.Add(new OracleParameter("NOME_GUERRA", usuario));
                OracleDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    pessoa.Matricula = Convert.ToInt32(rdr["MATRICULA"]);
                    pessoa.Nome = rdr["NOME"].ToString();
                    pessoa.Usuario = rdr["NOME_GUERRA"].ToString();
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
            return pessoa;
        }

        //AppCelular Inicia o processo de conferencia de mercadoria por pedido
        [WebMethod]
        public Pedido IniciaConferencia(long numped)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            Pedido pedido = new Pedido();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT I.NUMPED, NVL(C.CODFUNCSEP,0) AS CODFUNCSEP, COUNT(DISTINCT(P.CODPROD)) AS QT_PRODUTOS, " +
                    " SUM(I.QT / E.QTUNIT)AS QT_ITENS, C.CODFUNCCONF, C.CODCLI, C.POSICAO, C.CONDVENDA, NVL(C.ORDEMCONF, 0)ORDEMCONF, NVL(C.NUMCUPOM, 0)NUMCUPOM " +
                    " FROM PCPEDI I, PCPRODUT P, PCEMBALAGEM E, PCPEDC C WHERE I.CODPROD = P.CODPROD AND E.CODPROD = P.CODPROD AND C.NUMPED = I.NUMPED AND C.CODFILIAL = E.CODFILIAL AND C.POSICAO <> 'C' AND C.ORDEMCONF = 99 " +
                    " AND I.NUMPED =:numped GROUP BY I.NUMPED, C.CODFUNCSEP, C.DTFINALSEP, C.CODFUNCCONF, C.CODCLI, C.POSICAO, C.CONDVENDA, C.ORDEMCONF, C.NUMCUPOM ", cnn);
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                OracleDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    pedido.NumPedido = Convert.ToInt64(rdr["NUMPED"]);
                    pedido.Maticula = Convert.ToInt32(rdr["CODFUNCSEP"]);
                    pedido.QtProduto = Convert.ToDecimal(rdr["QT_PRODUTOS"]);
                    pedido.QtItens = Convert.ToDecimal(rdr["QT_ITENS"]);
                    int codigo = string.IsNullOrEmpty(rdr["CODFUNCCONF"].ToString()) ? 0 : Convert.ToInt32(rdr["CODFUNCCONF"]);
                    pedido.Conferente = codigo;
                    pedido.CodCli = Convert.ToInt32(rdr["CODCLI"]);
                    pedido.Posicao = rdr["POSICAO"].ToString();
                    int condVenda = string.IsNullOrEmpty(rdr["CONDVENDA"].ToString()) ? 0 : Convert.ToInt32(rdr["CONDVENDA"]);
                    pedido.CondVenda = condVenda;
                    pedido.OrdemConf = Convert.ToInt32(rdr["ORDEMCONF"]);
                    int numCupom = string.IsNullOrEmpty(rdr["NUMCUPOM"].ToString()) ? 0 : Convert.ToInt32(rdr["NUMCUPOM"]);
                    pedido.NumCupom = numCupom;
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

        //AppCelular Vincular pedido ao conferente 
        [WebMethod]
        public void AtribuirPedidoParaCaonferente(int matricula, long numped)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");            
            try
            {
                if(matricula != 0 && numped != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDC SET CODFUNCSEP =:matricula, DTINICIALSEP = SYSDATE WHERE NUMPED =:numped ", cnn);
                    cmd.Parameters.Add(new OracleParameter("CODFUNCSEP", matricula));
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
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

        //AppCelular valida quandidade pedida e quantidade conferida
        [WebMethod]
        public Produto SepararProduto(long numped, int codprod)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            Produto produto = new Produto();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT NUMSEQ, CODPROD, DESCRICAO, QTUNIDADE, QTSEPARADA FROM(SELECT NUMSEQ, P.CODPROD, P.DESCRICAO, SUM(I.QT / E.QTUNIT)AS QTUNIDADE, NVL(I.QTSEPARADA, 0) AS QTSEPARADA " +
                    " FROM PCPEDI I, PCPRODUT P, PCEMBALAGEM E WHERE I.CODPROD = P.CODPROD AND E.CODPROD = P.CODPROD AND I.CODFILIALRETIRA = E.CODFILIAL AND I.NUMPED =:numped AND I.CODPROD =:codprod AND NUMSEQ IN(SELECT MIN(NUMSEQ) FROM PCPEDI WHERE NUMPED =:numped AND CODPROD =:codprod AND DTFINALSEP IS NULL) " +
                    " GROUP BY I.NUMSEQ, P.CODPROD, P.DESCRICAO, I.QT, E.QTUNIT, I.QTSEPARADA ORDER BY 2 ASC) WHERE QTUNIDADE <> QTSEPARADA GROUP BY NUMSEQ, CODPROD, DESCRICAO, QTUNIDADE, QTSEPARADA", cnn);  
                cmd.BindByName = true;
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                cmd.Parameters.Add(new OracleParameter("CODPROD", codprod));
                OracleDataReader rdr = cmd.ExecuteReader();                

                if (rdr.Read())
                {
                    produto.Seq = Convert.ToInt32(rdr["NUMSEQ"]);
                    produto.Codigo = Convert.ToInt32(rdr["CODPROD"]);
                    produto.Descricao = rdr["DESCRICAO"].ToString();
                    produto.Qt = Convert.ToDecimal(rdr["QTUNIDADE"]);
                    produto.QtSep = Convert.ToDecimal(rdr["QTSEPARADA"]);
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
            return produto;
        }

        //AppCelular Inclui quantidade conferida no pedido por produto
        [WebMethod]
        public void ConfirmaConferencia(decimal qt_digitada, int matricula, long numped, int codigo, int numSeq)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (matricula != 0 && numped != 0 && codigo != 0 && qt_digitada != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDI I SET QTSEPARADA = NVL(I.QTSEPARADA,0) + :qt_digitada, I.DTINICIALSEP = SYSDATE, I.CODFUNCSEP =:matricula " +
                        " WHERE I.NUMPED =:numped AND I.CODPROD =:codigo AND I.NUMSEQ =:numSeq  ", cnn);
                    cmd.Parameters.Add(new OracleParameter("QTSEPARADA", qt_digitada));
                    cmd.Parameters.Add(new OracleParameter("CODFUNCSEP", matricula));
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                    cmd.Parameters.Add(new OracleParameter("CODPROD", codigo));
                    cmd.Parameters.Add(new OracleParameter("NUMSEQ", numSeq));
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

        //AppCelular Finaliza Conferencia do item no pedido
        [WebMethod]
        public void FinalizaConferenciaItem(long numped, int codigo, int numSeq)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("UPDATE PCPEDI I SET DTFINALSEP = SYSDATE WHERE I.NUMPED =:numped AND I.CODPROD =:codigo AND I.NUMSEQ =:numSeq " +
                    " AND(I.QT / (SELECT QTUNIT FROM PCEMBALAGEM WHERE CODPROD =:codigo AND CODFILIAL IN (SELECT CODFILIAL FROM PCPEDC WHERE NUMPED =:numped))) = NVL(I.QTSEPARADA, 0) AND I.DTFINALSEP IS NULL", cnn);              
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                cmd.Parameters.Add(new OracleParameter("CODPROD", codigo));
                cmd.Parameters.Add(new OracleParameter("NUMSEQ", numSeq));
                cmd.ExecuteNonQuery();            
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

        //AppCelular Finaliza o pedido por conferente, informando o processo ao faturamento por meio de finalização
        [WebMethod]
        public void FinalizaConferencia(long numped, int conferente)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (conferente != 0 && numped != 0 )
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDC C SET C.CODFUNCSEP =:conferente, C.DTFINALSEP = SYSDATE WHERE C.NUMPED =:numped AND ((SELECT COUNT(I.CODFUNCSEP) FROM PCPEDI I WHERE NUMPED =:numped) = (SELECT COUNT(I.CODPROD) FROM PCPEDI I WHERE NUMPED =:numped))", cnn);                    
                    cmd.Parameters.Add(new OracleParameter("CODFUNCSEP", conferente));
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numped));                    
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

        //AppCelular Valida conferencia se já foi finalizada por pedido
        [WebMethod]
        public Conferencia ValidaConferenciaCompleta(long numped)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            Conferencia conferencia = new Conferencia();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT (SELECT COUNT(I.CODFUNCSEP)FROM PCPEDI I WHERE NUMPED =:numped)QT_ITENS_CONF, (SELECT COUNT(I.CODPROD)FROM PCPEDI I WHERE NUMPED =:numped)QT_ITENS_ORG, " +
                    " NVL((SELECT SUM((I.QT / E.QTUNIT)) AS QTUNIDADE FROM PCPEDI I, PCPRODUT P, PCEMBALAGEM E WHERE I.CODPROD = P.CODPROD AND E.CODPROD = P.CODPROD AND I.CODFILIALRETIRA = E.CODFILIAL  AND(I.QT / E.QTUNIT) = NVL(I.QTSEPARADA, 0) AND I.NUMPED =:numped),0)SOMA_PRO_ORG, " +
                    " NVL((SELECT SUM(NVL(I.QTSEPARADA, 0))AS QTSEPARADA FROM PCPEDI I, PCPRODUT P, PCEMBALAGEM E WHERE I.CODPROD = P.CODPROD AND E.CODPROD = P.CODPROD AND I.CODFILIALRETIRA = E.CODFILIAL AND(I.QT / E.QTUNIT) = NVL(I.QTSEPARADA, 0) AND I.NUMPED =:numped),0)SOMA_PRO_SEP FROM DUAL ", cnn);
                cmd.BindByName = true;
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));             
                OracleDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    conferencia.QT_Itens_Conf = Convert.ToInt32(rdr["QT_ITENS_CONF"]);
                    conferencia.QT_Itens_Org = (Convert.ToInt32(rdr["QT_ITENS_ORG"]));
                    conferencia.Soma_Pro_Sep = (Convert.ToInt32(rdr["SOMA_PRO_SEP"]));
                    conferencia.Soma_Pro_Org = (Convert.ToInt32(rdr["SOMA_PRO_ORG"]));
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
            return conferencia;
        }

        //Painel faturamento dispara pedido para processo de separação/conferencia
        [WebMethod]
        public void EnviaPedidoParaPainel(int opcao, long numero, int filial)
        {       
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (opcao == 2 && numero != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDC SET ORDEMCONF = 99, NUMVIASMAPASEP = 1, DTIMPORTACAO = SYSDATE WHERE NUMNOTA =:numero AND CODFILIAL =:filial " +
                        " AND DATA > TRUNC(SYSDATE) - 220 AND CONDVENDA = 8 AND ORDEMCONF IS NULL", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMNOTA", numero));
                    cmd.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    cmd.ExecuteNonQuery();
                }
                else if (opcao == 3 && numero != 0)
                {
                    cnn.Open();
                    OracleCommand cmd1 = new OracleCommand("UPDATE PCCARREG C SET C.NUMVIASMAPA = 1 WHERE NUMCAR IN " +
                        " (SELECT NUMCAR FROM PCPEDC WHERE NUMPED =:numped AND CODFILIAL =:filial AND DATA > TRUNC(SYSDATE) - 220 AND ORDEMCONF IS NULL)", cnn);
                    cmd1.Parameters.Add(new OracleParameter("NUMPED", numero));
                    cmd1.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    cmd1.ExecuteNonQuery();

                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDC SET ORDEMCONF = 99, NUMVIASMAPASEP = 1, DTIMPORTACAO = SYSDATE WHERE NUMPED =:numero AND CODFILIAL =:filial " +
                        " AND DATA > TRUNC(SYSDATE) - 220 AND CONDVENDA NOT IN(7) AND ORDEMCONF IS NULL", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numero));
                    cmd.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    cmd.ExecuteNonQuery();
                }
                else if (opcao == 4 && numero != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCCARREG SET DTEXPORTACAO = SYSDATE WHERE NUMCAR =:numero AND DTEXPORTACAO IS NULL AND NUMVIASMAPA IS NOT NULL AND DTFAT IS NULL " +
                        " AND DESTINO NOT LIKE '%VENDA CHECK OUT 2030%' AND DESTINO NOT LIKE '%VENDA BALCAO RP%' ", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMCAR", numero));
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

        //Painel faturamento excluir pedido para processo de separação/conferencia
        [WebMethod]
        public void ExcluirPedidoDoPainel(int opcao, long numero, int filial)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (opcao == 2 && numero != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDC SET ORDEMCONF = null, CODFUNCSEP = null, DTINICIALSEP = null, DTFINALSEP = null, DTEXPORTACAO = SYSDATE, NUMVIASMAPASEP = null WHERE NUMNOTA =:numero AND CODFILIAL =:filial " +
                        " AND DATA > TRUNC(SYSDATE) - 120 AND CONDVENDA = 8 AND DTFINALSEP IS NULL AND ORDEMCONF = 99", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMNOTA", numero));
                    cmd.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    int numeroLinhas = cmd.ExecuteNonQuery();

                    if (numeroLinhas > 0)
                    {
                        OracleCommand cmd1 = new OracleCommand("UPDATE PCPEDI SET CODFUNCSEP = null, QTSEPARADA = null, DTINICIALSEP = null, DTFINALSEP = null WHERE NUMPED IN " +
                        " (SELECT NUMPED FROM PCPEDC WHERE NUMNOTA =:numero AND CONDVENDA = 8 AND DATA > TRUNC(SYSDATE) - 120 AND CODFILIAL =:filial)", cnn);
                        cmd1.Parameters.Add(new OracleParameter("NUMNOTA", numero));
                        cmd1.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                        cmd1.ExecuteNonQuery();
                    }


                }
                else if (opcao == 3 && numero != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCPEDC SET ORDEMCONF = null, CODFUNCSEP = null, DTINICIALSEP = null, DTFINALSEP = null, DTEXPORTACAO = SYSDATE, NUMVIASMAPASEP = null WHERE NUMPED =:numero AND CODFILIAL =:filial " +
                        " AND DATA > TRUNC(SYSDATE) - 120 AND ORDEMCONF = 99 AND POSICAO NOT IN ('F','C')", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numero));
                    cmd.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    int numeroLinhas = cmd.ExecuteNonQuery();

                    if (numeroLinhas > 0)
                    {
                        OracleCommand cmd1 = new OracleCommand("UPDATE PCPEDI SET CODFUNCSEP = null, QTSEPARADA = null, DTINICIALSEP = null, DTFINALSEP = null WHERE NUMPED =:numped", cnn);
                        cmd1.Parameters.Add(new OracleParameter("NUMPED", numero));
                        cmd1.ExecuteNonQuery();
                    }
                }
                else if (opcao == 4 && numero != 0)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCCARREG SET DTEXPORTACAO = null WHERE NUMCAR =:numero AND DTEXPORTACAO IS NOT NULL AND NUMVIASMAPA IS NOT NULL AND DTFAT IS NULL " +
                        " AND DESTINO NOT LIKE '%VENDA CHECK OUT 2030%' AND DESTINO NOT LIKE '%VENDA BALCAO RP%' AND CODFUNCCONF IS NULL ", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMCAR", numero));
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

        //Painel Conferencia Carregamento
        [WebMethod]
        public List<PainelCarreg> ListarCarregamentoParaConferencia()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<PainelCarreg> list = new List<PainelCarreg>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand(" SELECT C.DTEXPORTACAO DTSEQ, C.NUMCAR, V.PLACA, C.DESTINO, SUBSTR(R.NOME,1,INSTR(R.NOME,' ')-1) AS MOTORISTA, SUBSTR(E.NOME,1,INSTR(E.NOME,' ')-1)CONFERENTE, " +
                    " ROUND(((COUNT(P.CODFUNCSEP) * 100) / COUNT(P.NUMPED)), 2) || '%' AS PERC FROM PCPEDC P, PCCARREG C, PCVEICUL V, (SELECT NOME, MATRICULA FROM PCEMPR) R, (SELECT NOME, MATRICULA FROM PCEMPR) E " +
                    " WHERE P.NUMCAR = C.NUMCAR AND V.CODVEICULO = C.CODVEICULO AND E.MATRICULA(+) = C.CODFUNCCONF AND R.MATRICULA = C.CODMOTORISTA AND C.DTFAT IS NULL AND C.DTEXPORTACAO IS NOT NULL " +
                    " GROUP BY C.DTEXPORTACAO, C.NUMCAR, V.PLACA, C.DESTINO, R.NOME, E.NOME ORDER BY C.DTEXPORTACAO DESC ", cnn);
                cmd.BindByName = true;
                OracleDataReader rdr = cmd.ExecuteReader();

                PainelCarreg painelCarregamento = null;

                while (rdr.Read())
                {
                    painelCarregamento = new PainelCarreg();

                    painelCarregamento.DtSeq = rdr["DTSEQ"].ToString();
                    painelCarregamento.NumCar = Convert.ToInt64(rdr["NUMCAR"]);
                    painelCarregamento.Placa = rdr["PLACA"].ToString();
                    painelCarregamento.Destino = rdr["DESTINO"].ToString();
                    painelCarregamento.Motorista = rdr["MOTORISTA"].ToString();
                    painelCarregamento.Conferente = rdr["CONFERENTE"].ToString();
                    painelCarregamento.Perc = rdr["PERC"].ToString();                    
                    list.Add(painelCarregamento);
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
            return list;
        }

        //Painel de separação antecipada almoxarifado
        [WebMethod]
        public List<Almoxarifado> ListaPedidosdoAlmoxarifado()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Almoxarifado> list = new List<Almoxarifado>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT C.NUMPED, L.CLIENTE, C.OBS, NVL(C.NUMNOTA, 0)NOTA, E.NOME, P.CODPROD, P.DESCRICAO, (I.QT / M.QTUNIT)AS QT, DECODE(NVL(CODFUNCCONF2,0),0,'NAO',1,'SIM')CODFUNCCONF2 " +
                    " FROM PCPEDI I, PCPRODUT P, PCPEDC C, PCEMPR E, PCCLIENT L, PCEMBALAGEM M WHERE P.CODPROD = I.CODPROD AND C.CODCLI = L.CODCLI " +
                    " AND E.MATRICULA = C.CODFUNCSEP AND C.NUMPED = I.NUMPED AND P.CODEPTO = 507 AND I.CODPROD = M.CODPROD AND I.CODFILIALRETIRA = M.CODFILIAL AND C.DATA > TRUNC(SYSDATE) - 90 " +
                    " AND C.CODFILIAL = 1 AND C.ORDEMCONF = 99 AND C.DTINICIALSEP IS NOT NULL AND C.DTFINALSEP IS NULL ORDER BY 1 ", cnn);
                cmd.BindByName = true;                
                OracleDataReader rdr = cmd.ExecuteReader();

                Almoxarifado almoxarifado = null;

                while (rdr.Read())
                {
                    almoxarifado = new Almoxarifado();

                    almoxarifado.Pedido = Convert.ToInt64(rdr["NUMPED"]);
                    almoxarifado.Cliente = rdr["CLIENTE"].ToString();
                    almoxarifado.Obs = rdr["OBS"].ToString();
                    almoxarifado.Nota = Convert.ToInt32(rdr["NOTA"]);
                    almoxarifado.Conferente = rdr["NOME"].ToString();
                    almoxarifado.CodProd = Convert.ToInt32(rdr["CODPROD"]);
                    almoxarifado.Descricao = rdr["DESCRICAO"].ToString();
                    almoxarifado.Qtde = Convert.ToDecimal(rdr["QT"]);
                    almoxarifado.Conferido = rdr["CODFUNCCONF2"].ToString();
                    list.Add(almoxarifado);
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
            return list;
        }

        //AppCelular lista detalhes do pedido campos OBS
        [WebMethod]
        public List<DetalhePed> ListaDetalhesDoPedido(long numped)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<DetalhePed> list = new List<DetalhePed>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT NUMPED, NVL(NUMNOTA,0)NOTA, OBS, OBS1, OBS2 FROM PCPEDC WHERE NUMPED =: numped ", cnn);
                cmd.BindByName = true;
                cmd.Parameters.Add(new OracleParameter("NUMPED", numped));
                OracleDataReader rdr = cmd.ExecuteReader();

                DetalhePed detalhes = null;

                while (rdr.Read())
                {
                    detalhes = new DetalhePed();

                    detalhes.Pedido = Convert.ToInt64(rdr["NUMPED"]);
                    detalhes.Nota = Convert.ToInt32(rdr["NOTA"]);
                    detalhes.Obs = rdr["OBS"].ToString();
                    detalhes.Obs1 = rdr["OBS1"].ToString();
                    detalhes.Obs2 = rdr["OBS2"].ToString();
                    list.Add(detalhes);
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
            return list;
        }

        //Serviço pra carta de correção e Manifesto
        [WebMethod]
        public void CorrigirCartaCorrecao(int conferente, string motorista, long cpf, long numped, string marca, string placa, string veiculoUF, string rua, string bairro, string cidade, string uf)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
                if (conferente == 659)
                {
                    //motorista
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE PCEMPR E SET E.NOME =:motorista, E.CPF =:cpf WHERE E.matricula = 1394 ", cnn);
                    cmd.Parameters.Add(new OracleParameter("NOME", motorista));
                    cmd.Parameters.Add(new OracleParameter("CPF", cpf));
                    cmd.ExecuteNonQuery();
                    //motorista
                    OracleCommand cmd2 = new OracleCommand("UPDATE PCCARREG C SET C.CODMOTORISTA =1394 WHERE NUMCAR IN (SELECT NUMCAR FROM PCPEDC WHERE NUMPED =:numped) ", cnn);
                    cmd2.Parameters.Add(new OracleParameter("NUMPED", numped));
                    cmd2.ExecuteNonQuery();

                    //veiculo
                    OracleCommand cmd3 = new OracleCommand("UPDATE PCVEICUL V SET V.DESCRICAO =:marca, V.MARCA =:marca, V.PLACA =:placa, V.UFPLACAVEICULO =:veiculoUF WHERE V.CODVEICULO = 1286 ", cnn);
                    cmd3.Parameters.Add(new OracleParameter("DESCRICAO", marca));
                    cmd3.Parameters.Add(new OracleParameter("MARCA", marca));
                    cmd3.Parameters.Add(new OracleParameter("PLACA", placa));
                    cmd3.Parameters.Add(new OracleParameter("UFPLACAVEICULO", veiculoUF));
                    cmd3.ExecuteNonQuery();

                    //veiculo
                    OracleCommand cmd4 = new OracleCommand("UPDATE PCCARREG C SET C.CODVEICULO =1286 WHERE NUMCAR IN (SELECT NUMCAR FROM PCPEDC WHERE NUMPED =:numped) ", cnn);
                    cmd4.Parameters.Add(new OracleParameter("NUMPED", numped));
                    cmd4.ExecuteNonQuery();

                    //Fornecedor
                    OracleCommand cmd5 = new OracleCommand("UPDATE PCFORNEC F SET F.ENDER =:rua, F.BAIRRO =:bairro, F.CIDADE =:cidade, F.ESTADO =:uf WHERE F.CODFORNEC = 9276 ", cnn);
                    cmd5.Parameters.Add(new OracleParameter("ENDER", rua));
                    cmd5.Parameters.Add(new OracleParameter("BAIRRO", bairro));
                    cmd5.Parameters.Add(new OracleParameter("CIDADE", cidade));
                    cmd5.Parameters.Add(new OracleParameter("ESTADO", uf));
                    cmd5.ExecuteNonQuery();

                    //Fornecedor
                    OracleCommand cmd6 = new OracleCommand("UPDATE PCPEDC C SET C.CODFORNECFRETE = 9276 WHERE C.NUMPED =:numped ", cnn);
                    cmd6.Parameters.Add(new OracleParameter("NUMPED", numped));
                    cmd6.ExecuteNonQuery();
                }

        }

        //Painel do faturamento 
        [WebMethod]
        public List<Faturamento> ListaPedidosParaFaturar()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Faturamento> list = new List<Faturamento>();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT C.DATA, C.CODFILIAL, C.NUMCAR, C.NUMPED, C.CODCLI, L.CLIENTE, L.UFRG UF, C.VLTOTAL, E.NOME CONFERENTE " +
                    " FROM PCPEDC C, PCCLIENT L, PCEMPR E WHERE L.CODCLI = C.CODCLI AND E.MATRICULA = C.CODFUNCSEP AND C.DTFINALSEP IS NOT NULL " +
                    " AND C.DTFAT IS NULL AND C.ORDEMCONF = 99 AND C.DATA >= TRUNC(SYSDATE) - 120 ORDER BY 1", cnn);
                cmd.BindByName = true;                
                OracleDataReader rdr = cmd.ExecuteReader();

                Faturamento faturamento = null;

                while (rdr.Read())
                {
                    faturamento = new Faturamento();

                    faturamento.Data = Convert.ToDateTime(rdr["DATA"]);
                    faturamento.Filial = Convert.ToInt32(rdr["CODFILIAL"]);
                    faturamento.Carregamento = Convert.ToInt64(rdr["NUMCAR"]);
                    faturamento.Pedido = Convert.ToInt64(rdr["NUMPED"]);
                    faturamento.Codigo = Convert.ToInt32(rdr["CODCLI"]);
                    faturamento.Cliente = rdr["CLIENTE"].ToString();
                    faturamento.UF = rdr["UF"].ToString();
                    faturamento.VlTotal = Convert.ToDecimal(rdr["VLTOTAL"]);
                    faturamento.Conferente = rdr["CONFERENTE"].ToString();
                    list.Add(faturamento);
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
            return list;
        }

        //Painel produtos a solicitar produção
        [WebMethod]
        public List<Industria> ListarProducaoSolicitada()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Industria> list = new List<Industria>();
            
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

                Industria industria = null;

                while (rdr.Read())
                {
                    industria = new Industria();

                    industria.Filial = Convert.ToInt32(rdr["CODFILIAL"]);
                    industria.NumCar = Convert.ToInt64(rdr["NUMCAR"]);
                    industria.Pedido = Convert.ToInt64(rdr["NUMPED"]);                    
                    industria.RCA = rdr["RCA"].ToString();
                    industria.Codigo = Convert.ToInt32(rdr["CODPROD"]);
                    industria.Descricao = rdr["DESCRICAO"].ToString();
                    industria.Qt = Convert.ToDecimal(rdr["QT"]);
                    industria.Obs = rdr["OBS"].ToString();
                    industria.Obs1 = rdr["OBS1"].ToString();
                    industria.Obs2 = rdr["OBS2"].ToString();
                    list.Add(industria);                    
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
            return list;
        }

        //Valida se já foi enviado com finalização concluida
        //Valida se o pedido ou Cupom e tv8
        [WebMethod]
        public Pedido ValidaEnvioPedidoPainel(int opcao, long numero, int filial)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            Pedido pedido = new Pedido();
            try
            {
                if (opcao == 2)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT C.CONDVENDA, C.NUMPED FROM PCPEDC C INNER JOIN PCNFSAID S ON S.NUMPED = C.NUMPEDENTFUT  WHERE S.NUMNOTA =:numero AND S.CODFILIAL =:filial " +
                        " AND C.DTFINALSEP IS NULL AND C.DTFAT = (SELECT MAX(DTFAT) FROM PCPEDC C WHERE C.NUMNOTA =:numero AND CODFILIAL =:filial)", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMNOTA", numero));
                    cmd.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    OracleDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        pedido.CondVenda = Convert.ToInt32(rdr["CONDVENDA"]);
                        pedido.NumPedido = (Convert.ToInt64(rdr["NUMPED"]));
                    }
                    rdr.Close();
                }
                else if (opcao == 3)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT C.CONDVENDA, C.NUMPED FROM PCPEDC C WHERE C.NUMPED =:numero AND C.CODFILIAL =:filial  AND C.DTFINALSEP IS NULL AND C.DTFAT IS NULL AND CONDVENDA <> 7 ", cnn);
                    cmd.Parameters.Add(new OracleParameter("NUMPED", numero));
                    cmd.Parameters.Add(new OracleParameter("CODFILIAL", filial));
                    OracleDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        pedido.CondVenda = Convert.ToInt32(rdr["CONDVENDA"]);
                        pedido.NumPedido = (Convert.ToInt64(rdr["NUMPED"]));
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
            return pedido;
        }

        //Painel Faturar carga
        [WebMethod]
        public List<Carregamento> ListarFaturamentoDeCarregamento()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Carregamento> list = new List<Carregamento>();

            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT C.DATAMON, C.NUMCAR, C.CODFUNCCONF, E.NOME AS CONFERENTE, C.CODVEICULO, V.PLACA, C.DESTINO, C.CODMOTORISTA, TB1.QTPEDIDOS, TB2.QTCONCLUIDOS " +
                    "  FROM PCPEDC P, PCCARREG C, PCEMPR E, PCVEICUL V, (SELECT G.NUMCAR, COUNT(DISTINCT C.NUMPED)AS QTPEDIDOS FROM PCPEDC C, PCCARREG G WHERE G.NUMCAR = C.NUMCAR  GROUP BY G.NUMCAR) TB1, " +
                    " (SELECT G.NUMCAR, COUNT(DISTINCT NUMPED)AS QTCONCLUIDOS FROM PCPEDC C, PCCARREG G WHERE G.NUMCAR = C.NUMCAR AND G.CODFUNCCONF IS NOT NULL AND C.DTCANCEL IS NULL AND G.DTFAT IS NULL AND C.DTFINALSEP IS NOT NULL GROUP BY G.NUMCAR) TB2 " +
                    "  WHERE P.NUMCAR = C.NUMCAR AND TB1.NUMCAR = C.NUMCAR AND TB2.NUMCAR = C.NUMCAR AND V.CODVEICULO = C.CODVEICULO AND E.MATRICULA = C.CODFUNCCONF AND C.DTFAT IS NULL AND C.CODFUNCCONF IS NOT NULL " +
                    "GROUP BY C.DATAMON, C.NUMCAR, C.CODFUNCCONF, E.NOME, C.CODVEICULO, V.PLACA, C.DESTINO, C.CODMOTORISTA, TB1.QTPEDIDOS, TB2.QTCONCLUIDOS ORDER BY 1 ASC ", cnn);
                cmd.BindByName = true;
                OracleDataReader rdr = cmd.ExecuteReader();

                Carregamento carregamento = null;

                while (rdr.Read())
                {
                    carregamento = new Carregamento();                    
                    
                    carregamento.DtMontagem = Convert.ToDateTime(rdr["DATAMON"].ToString());
                    carregamento.NumCar = Convert.ToInt64(rdr["NUMCAR"]);
                    carregamento.CodFunc = Convert.ToInt32(rdr["CODFUNCCONF"]);
                    carregamento.Conferente = rdr["CONFERENTE"].ToString();
                    carregamento.CodVeiculo = Convert.ToInt32(rdr["CODVEICULO"]);
                    carregamento.Placa = rdr["PLACA"].ToString();
                    carregamento.Destino = rdr["DESTINO"].ToString();
                    carregamento.CodMot = Convert.ToInt32(rdr["CODMOTORISTA"]);
                    carregamento.QtPedidos = Convert.ToInt32(rdr["QTPEDIDOS"]);
                    carregamento.QtConcluido = Convert.ToInt32(rdr["QTCONCLUIDOS"]);
                    list.Add(carregamento);
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
            return list;
        }

        //Painel Faturamento de Carregamento itens com corte
        [WebMethod]
        public List<Corte> ListarItensComDivergenciaDeConferencia()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<Corte> list = new List<Corte>();

            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT G.NUMCAR, C.NUMPED, I.CODFUNCSEP, SUBSTR(E.NOME,1,INSTR(E.NOME,' ')-1)CONFERENTE, P.CODPROD, P.DESCRICAO, QTSEPARARUN AS QTCORTE, I.QTLOCALIZADA AS QTINCLUIR " +
                    " FROM PCPEDC C, PCPEDI I, PCCARREG G, PCEMPR E, PCPRODUT P WHERE C.NUMPED = I.NUMPED AND G.NUMCAR = C.NUMCAR AND E.MATRICULA = I.CODFUNCSEP AND P.CODPROD = I.CODPROD " +
                    " AND G.DTFAT IS NULL AND (I.QTSEPARARUN > 0 OR I.QTLOCALIZADA > 0)  AND G.DATAMON > TRUNC(SYSDATE) - 120 " +
                    " GROUP BY G.NUMCAR, C.NUMPED, I.CODFUNCSEP, E.NOME, P.CODPROD, P.DESCRICAO, QTSEPARARUN, QTLOCALIZADA ", cnn);
                cmd.BindByName = true;
                OracleDataReader rdr = cmd.ExecuteReader();

                Corte corte = null;

                while (rdr.Read())
                {
                    corte = new Corte();

                    corte.NumCar = Convert.ToInt64(rdr["NUMCAR"]);
                    corte.Pedido = Convert.ToInt64(rdr["NUMPED"]);
                    corte.CodFunc = Convert.ToInt32(rdr["CODFUNCSEP"]);
                    corte.Conferente = rdr["CONFERENTE"].ToString();
                    corte.Codigo = Convert.ToInt32(rdr["CODPROD"]);
                    corte.Descricao = rdr["DESCRICAO"].ToString();
                    corte.QtCorte = Convert.ToDecimal(rdr["QTCORTE"]);
                    corte.QtIncluir = Convert.ToDecimal(rdr["QTINCLUIR"]);
                    list.Add(corte);
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
            return list;
        }

        //Painel setor de Carga itens com Corte / Inclusao 
        [WebMethod]
        public List<CortePainel> ListarItensPainelComDivergencia()
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO");
            List<CortePainel> list = new List<CortePainel>();

            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT G.NUMCAR, SUBSTR(E.NOME,1,INSTR(E.NOME,' ')-1)CONFERENTE, P.CODPROD, P.DESCRICAO, QTSEPARARUN AS QTCORTE, I.QTLOCALIZADA AS QTINCLUIR " +
                    " FROM PCPEDC C, PCPEDI I, PCCARREG G, PCEMPR E, PCPRODUT P WHERE C.NUMPED = I.NUMPED AND G.NUMCAR = C.NUMCAR AND E.MATRICULA = I.CODFUNCSEP AND P.CODPROD = I.CODPROD " +
                    " AND G.DTFAT IS NULL AND (I.QTSEPARARUN > 0 OR I.QTLOCALIZADA > 0)  AND G.DATAMON > TRUNC(SYSDATE) - 120 " +
                    " GROUP BY G.NUMCAR, E.NOME, P.CODPROD, P.DESCRICAO, QTSEPARARUN, QTLOCALIZADA ", cnn);
                cmd.BindByName = true;
                OracleDataReader rdr = cmd.ExecuteReader();

                CortePainel corte = null;

                while (rdr.Read())
                {
                    corte = new CortePainel();

                    corte.NumCar = Convert.ToInt64(rdr["NUMCAR"]);                    
                    corte.Conferente = rdr["CONFERENTE"].ToString();
                    corte.Codigo = Convert.ToInt32(rdr["CODPROD"]);
                    corte.Descricao = rdr["DESCRICAO"].ToString();
                    corte.QtCorte = Convert.ToDecimal(rdr["QTCORTE"]);
                    corte.QtIncluir = Convert.ToDecimal(rdr["QTINCLUIR"]);
                    list.Add(corte);
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
            return list;
        }

        //Envia novo carregamento para sequencia de entregas
        [WebMethod]
        public void InserirNovaCargaSeqEntrega( long novo_car, long ref_numcar, int seq_entrega, string obs)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (novo_car != 0 && ref_numcar != 0 && seq_entrega != 0)
                {
                    cnn.Open();                    
                    OracleCommand cmd = new OracleCommand("INSERT INTO ABZ_INCLUIR_CARGA (ID, NOVO_CAR, REF_NUMCAR, SEQ_ENTREGA, OBS, ENVIADO, EXCLUIDO) VALUES " +
                        " (seq_abz_incluir_carga.NEXTVAL, :novo_car, :ref_numcar, :seq_entrega, :obs, trunc(sysdate),'')", cnn);
                    cmd.Parameters.Add(new OracleParameter("NOVO_CAR", novo_car));
                    cmd.Parameters.Add(new OracleParameter("REF_NUMCAR", ref_numcar));
                    cmd.Parameters.Add(new OracleParameter("SEQ_ENTREGA", seq_entrega));
                    cmd.Parameters.Add(new OracleParameter("OBS", obs));
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


        //Painel Confirma matricula
        //Expecifico da filial 1
        [WebMethod]
        public void ConfirmaConferenciaAutomatica(long numped, int matricula, int opcao)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (matricula != 0 && numped != 0 && opcao == 2)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand($"UPDATE PCPEDC C SET C.CODFUNCSEP ='{matricula}', C.ORDEMCONF = 98, C.DTINICIALSEP = SYSDATE, DTIMPORTACAO = SYSDATE " +
                        $"WHERE NUMNOTA ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120   ", cnn);
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd1 = new OracleCommand($"UPDATE PCPEDI C SET C.CODFUNCSEP ='{matricula}', C.DTINICIALSEP = SYSDATE WHERE C.NUMPED IN " +
                        $"(SELECT NUMPED FROM PCPEDC WHERE NUMNOTA ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120)  ", cnn);
                    cmd1.ExecuteNonQuery();
                }
                else if (matricula != 0 && numped != 0 && opcao == 3)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand($"UPDATE PCPEDC C SET C.CODFUNCSEP ='{matricula}', C.ORDEMCONF = 98, C.DTINICIALSEP = SYSDATE, DTIMPORTACAO = SYSDATE " +
                        $"WHERE NUMPED ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') ", cnn);
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd1 = new OracleCommand($"UPDATE PCPEDI C SET C.CODFUNCSEP ='{matricula}', C.DTINICIALSEP = SYSDATE WHERE C.NUMPED IN " +
                        $"(SELECT NUMPED FROM PCPEDC WHERE NUMPED ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C'))   ", cnn);
                    cmd1.ExecuteNonQuery();
                }
                else if (matricula != 0 && numped != 0 && opcao == 4)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand($"UPDATE PCPEDC C SET C.CODFUNCSEP ='{matricula}', C.ORDEMCONF = 98, C.DTINICIALSEP = SYSDATE, DTIMPORTACAO = SYSDATE " +
                        $" WHERE C.NUMCAR IN (SELECT NUMCAR FROM PCPEDC WHERE NUMCAR ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C')) ", cnn);
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd1 = new OracleCommand($"UPDATE PCPEDI C SET C.CODFUNCSEP ='{matricula}', C.DTINICIALSEP = SYSDATE WHERE C.NUMPED IN " +
                        $" (SELECT NUMPED FROM PCPEDC WHERE NUMCAR ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C'))   ", cnn);
                    cmd1.ExecuteNonQuery();
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

        [WebMethod]
        public void FinalizarConferenciaAutomatica(long numped, int opcao)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            try
            {
                if (numped != 0 && opcao == 2)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand($"UPDATE PCPEDC C SET C.DTFINALSEP = SYSDATE " +
                        $"WHERE NUMNOTA ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120 AND ORDEMCONF = 98 AND CODFUNCSEP IS NOT NULL   ", cnn);
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd1 = new OracleCommand($"UPDATE PCPEDI C SET C.DTFINALSEP = SYSDATE WHERE C.NUMPED IN " +
                        $"(SELECT NUMPED FROM PCPEDC WHERE NUMNOTA ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND DATA > TRUNC(SYSDATE) - 120 AND CODFUNCSEP IS NOT NULL)  ", cnn);
                    cmd1.ExecuteNonQuery();
                }
                else if (numped != 0 && opcao == 3)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand($"UPDATE PCPEDC C SET C.DTFINALSEP = SYSDATE " +
                        $"WHERE NUMPED ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND ORDEMCONF = 98 AND CODFUNCSEP IS NOT NULL", cnn);
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd1 = new OracleCommand($"UPDATE PCPEDI C SET C.DTFINALSEP = SYSDATE WHERE C.NUMPED IN " +
                        $"(SELECT NUMPED FROM PCPEDC WHERE NUMPED ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND ORDEMCONF = 98 AND CODFUNCSEP IS NOT NULL)   ", cnn);
                    cmd1.ExecuteNonQuery();
                }
                else if (numped != 0 && opcao == 4)
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand($"UPDATE PCPEDC C SET C.DTFINALSEP = SYSDATE " +
                        $" WHERE C.NUMCAR IN (SELECT NUMCAR FROM PCPEDC WHERE NUMCAR ='{numped}'  AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND ORDEMCONF = 98 AND CODFUNCSEP IS NOT NULL) ", cnn);
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd1 = new OracleCommand($"UPDATE PCPEDI C SET C.DTFINALSEP = SYSDATE WHERE C.NUMPED IN " +
                        $" (SELECT NUMPED FROM PCPEDC WHERE NUMCAR ='{numped}' AND CONDVENDA = 8 AND POSICAO NOT IN('C') AND ORDEMCONF = 98 AND CODFUNCSEP IS NOT NULL)   ", cnn);
                    cmd1.ExecuteNonQuery();
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

        //Painel Confirma matricula        
        [WebMethod]
        public Pessoa ConfirmaMatricula(int matricula)
        {
            OracleConnection cnn = new OracleConnection("DATA SOURCE=192.168.251.3:1521/WINT;PERSIST SECURITY INFO=True;USER ID=ACOBRAZIL; Password=SGAGRANADO;");
            Pessoa pessoa = new Pessoa();
            try
            {
                cnn.Open();
                OracleCommand cmd = new OracleCommand("SELECT MATRICULA, NOME, NOME_GUERRA FROM PCEMPR WHERE MATRICULA =:matricula ", cnn);
                cmd.Parameters.Add(new OracleParameter("MATRICULA", matricula));
                OracleDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    pessoa.Matricula = Convert.ToInt32(rdr["MATRICULA"]);
                    pessoa.Nome = rdr["NOME"].ToString();
                    pessoa.Usuario = rdr["NOME_GUERRA"].ToString();
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
            return pessoa;
        }

    }
}
