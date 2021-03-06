﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PainelCarregamento.aspx.cs" Inherits="AppConferenciaABP.PainelCarregamento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta http-equiv="refresh" content="15" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="estilo.css" />
    <title>Painel Carregamento</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-7 col-sm-7 col-md-7 col-lg-7 col-xl-7">
                <div class="pb-2 mt-4 mb-2 border-bottom container">
		            <h4>Carregamentos para conferência</h4>  
                        <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="4" ForeColor="White" Font-Size="12" CssClass="table table-bordered table-dark" GridLines="None" AllowPaging="False">           
                        </asp:GridView>                                     
                </div>
	        </div>
            <div class="col-5 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                <div class="pb-2 mt-4 mb-2 border-bottom container">
		            <h4>Itens com divergência</h4>                
                        <asp:GridView ID="GridView2" runat="server" Width="100%" CellPadding="4" ForeColor="White" Font-Size="10" CssClass="table table-bordered table-dark" GridLines="None" AllowPaging="False">           
                        </asp:GridView>                                     
                </div>
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
