<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalizarConferencia.aspx.cs" Inherits="AppConferenciaABP.FinalizarConferencia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="estilo.css" />
    <title>Finaliza Conferência</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pb-2 mt-4 mb-2 border-bottom container">
		    <h4>Finalizar Conferência</h4> 
        </div>
        <br />
        <div class="row">  
            <div class="col-1"></div>
            <div class="col-3 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                <label for="sel1">Opções</label> 
                    <asp:DropDownList ID="DropDownList1" class="form-control" runat="server">
                         <asp:ListItem Value="1">Selecione...</asp:ListItem>
                         <asp:ListItem Value="2">NFC-e</asp:ListItem>
                         <asp:ListItem Value="3">Pedido</asp:ListItem>                     
                         <asp:ListItem Value="4">Carga</asp:ListItem>
                     </asp:DropDownList>
           </div>

           <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <label for="sel">Informe o Número</label> 
                <asp:TextBox ID="TextBoxNumero" class="form-control" runat="server"></asp:TextBox>     
           </div> 
           
           <div class="col-2 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                <label for="sel">Mat.Conferente</label> 
                <asp:TextBox ID="TextBoxCodConferente" class="form-control" runat="server"></asp:TextBox>     
           </div>           
           
           <div class="col-1 col-sm-1 col-md-1 col-lg-1 col-xl-1">
               <br />
               <asp:Button ID="btPesquisar" class="btn btn-outline-primary" runat="server" Text="Pesquisar" OnClick="btPesquisar_Click"    />                                 
           </div>  

           <div class="col-1 col-sm-1 col-md-1 col-lg-1 col-xl-1">
               <br />
               <asp:Button ID="btConfirmar" class="btn btn-outline-warning" runat="server" Text="Inicia Conf" OnClick="btConfirmar_Click"   />                                 
           </div> 
           
                
           <div class="col-1 col-sm-1 col-md-1 col-lg-1 col-xl-1">
               <br />
               <asp:Button ID="btFinalizar" class="btn btn-outline-success" runat="server" Text="Finaliza Conf" OnClick="btFinalizar_Click"  />                                 
           </div>  
        </div>

        <div class="row">
            <br />
            <div class="col-4"></div>
            <div class="col-2">
                <asp:Label ID="lbPedido" runat="server"/>
            </div>
            <div class="col-4">
                <asp:Label ID="lbConferente" runat="server"/>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-1">
            </div>
            <div class="col-10 col-sm-10 col-md-10 col-lg-10 col-xl-10">
			   <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="4" ForeColor="White" Font-Size="12" CssClass="table table-bordered table-dark" GridLines="None" AllowPaging="False">           
               </asp:GridView>                                     
            </div>
            <div class="col-1">
            </div>
        </div>

        <div class="pb-2 mt-4 mb-2 border-bottom container">
            <h6>Pedidos em Conferência</h6>                
               <asp:GridView ID="GridView2" runat="server" Width="100%" CellPadding="4" ForeColor="Black" Font-Size="8" CssClass="table table-bordered" GridLines="None" AllowPaging="False">           
               </asp:GridView>
        </div>      
    </form>
</body>
</html>
