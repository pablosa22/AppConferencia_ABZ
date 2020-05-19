<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncluirCarga.aspx.cs" Inherits="AppConferenciaABP.IncluirCarga" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="estilo.css" />
    <title>Incluír Carregamento</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pb-2 mt-4 mb-2 border-bottom container">
		    <h4>Incluir Novo Carregamento na Sequência de Entregas</h4> 
        </div>
        <br />
        <div class="row"> 
            <div class="col-1">
            </div>
           <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <label for="sel">Ref Carregamento de Nº</label> 
                <asp:TextBox ID="TextBoxRefCar" class="form-control" runat="server"></asp:TextBox>     
           </div> 
            <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <br />                
                <asp:Button ID="btPesquisar" class="btn btn-outline-dark btn-block" runat="server" Text="Pesquisar" />                                 
            </div> 
            <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <br />                
                <asp:Button ID="btIncluirNova" class="btn btn-outline-warning btn-block" runat="server" Text="Incluir Carga" />                                 
            </div>
        </div>    
        <br />
        <div class="row">
            <div class="col-1">
            </div>
            <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <label ID ="lbNovoCarregamento" for="sel">Novo Carregamento</label> 
                <asp:TextBox ID="TextBoxNovoCar" class="form-control" runat="server"></asp:TextBox>     
           </div> 
           <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <label ID="lbSequencia" for="sel">Sequência Entrega</label> 
                <asp:TextBox ID="TextBoxSeq" class="form-control" runat="server"></asp:TextBox>     
           </div>  
           <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <label for="sel">Observação</label> 
                <asp:TextBox ID="TextBoxObs" class="form-control" runat="server"></asp:TextBox>     
           </div> 
           <div class="col-1">
            </div> 
        </div>
        <br />
        <div class ="row">
            <div class="col-1">
            </div>
            <div class="col-5">
                <asp:Button ID="btConfirmar" class="btn btn-outline-success btn-block" runat="server" Text="Enviar Para o Painel" OnClick="btConfirmar_Click" />                                 
            </div>
            <div class="col-5">
                <asp:Button ID="btExcluir" class="btn btn-outline-danger btn-block" runat="server" Text="Excluir do Painel" />
            </div>
            <div class="col-1">
            </div>
        </div>
    </form>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>  
</body>
</html>
