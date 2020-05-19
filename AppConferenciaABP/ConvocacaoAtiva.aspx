<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvocacaoAtiva.aspx.cs" Inherits="AppConferenciaABP.ConvocacaoAtiva" %>

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
    <script type="text/javascript">
        function piscando() {

            //Autor: Bruno Passos - Clandevelop Sistemas e Sites
            //http://www.clandevelop-sistemas-e-sites.blogspot.com

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


    <title>Conv.Ativa</title>  
</head>
<body class="text-center" onload="piscando();">
    <form id="form1" runat="server">        
        <audio autoplay loop>
          <source src="Sons/AlertaConvocaoAtiva.wav" type="audio/ogg"/>          
        </audio>
         <br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/aco2.png"/>                     
        <br />
        <br />
        <div class="container">
          <blink>  
            <h4>Convocação de Produção Cliente em Loja</h4>            
          </blink>  
            <br />
                <asp:Label ID="lbPedido" class="container" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>                
            <br />
            <br />
                <asp:Button ID="btEntrar" class="btn btn-outline-success btn-block" runat="server" Text="Acessar Pedido" OnClick="btEntrar_Click"  />                                     
            <br />
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
