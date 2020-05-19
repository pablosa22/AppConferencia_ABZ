<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PainelOSIndustria.aspx.cs" Inherits="AppConferenciaABP.PainelOSIndustria" EnableEventValidation="false" %>

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

    <title>Painel Os Industria</title>
</head>
<body>
    <form id="form1" runat="server">        
        <div class="row">            
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
			   <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="4" ForeColor="Black" Font-Size="11pt" CssClass="table table-bordered table-sm" GridLines="None" AllowPaging="False" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Bold="False">           
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
