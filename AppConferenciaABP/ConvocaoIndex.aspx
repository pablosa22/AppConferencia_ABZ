<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvocaoIndex.aspx.cs" Inherits="AppConferenciaABP.ConvocaoIndex" %>

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
    <meta http-equiv="refresh" content="10" />
    <script type="text/javascript">
        function piscando() {
           
            var tempo = 500; //1000 = 1s      

            blinks = document.getElementsByTagName("blink");
            for (var i = 0; i < blinks.length; i++) {
                if (blinks[i].getAttribute("style") == "VISIBILITY: hidden") {
                    blinks[i].setAttribute("style", "VISIBILITY: visible");
                } else {
                    blinks[i].setAttribute("style", "VISIBILITY: hidden");
                }
            }
            setTimeout('piscando()', tempo);
        }
</script>
    <title>Convocacao</title>
</head>
<body class="text-center" onload="piscando();">
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/aco2.png"/>                     
            <br />
            <br />
            <br />
                <h3>Não Existe Pedido Para Conferência no Momento</h3> 
             <br />
            <br />
            <blink> 
                <h4>Aguarde...</h4> 
            </blink>
            <p class="mt-5 mb-3 text-muted">&copy;Desenvolvido Pela TI AçoBrazil Versão 1.0</p>             
            <asp:Label ID="lbDescricao" class="container" runat="server">Filial: 1 - Aço Brazil</asp:Label>
        </div>
    </form>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</body>
</html>
