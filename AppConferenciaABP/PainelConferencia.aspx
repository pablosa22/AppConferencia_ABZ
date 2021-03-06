﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PainelConferencia.aspx.cs" Inherits="AppConferenciaABP.PainelConferencia1" %>

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
    <title>Painel Conferencia</title>
</head>
<body>
    <form id="form1" runat="server">        
         <div class="row">            
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
			   <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="4" ForeColor="White" Font-Size="12" CssClass="table table-bordered table-dark table-sm" GridLines="None" AllowPaging="False">           
               </asp:GridView>                                     
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
