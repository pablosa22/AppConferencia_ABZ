<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manifesto.aspx.cs" Inherits="AppConferenciaABP.Manifesto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="estilo.css" />
    <title>Manifesto</title>  
</head>
<body>
    <form id="form1" runat="server">
       <audio id="myAudio">
        <source src="Sons/Policia.mp3" type="audio/mpeg" />
        <source src="horse.mp3" type="audio/mpeg" />
  Your browser does not support the audio element.
</audio>

<p>Click the buttons to play or pause the audio.</p>

<button onclick="playAudio()" type="button">Play Audio</button>
<button onclick="pauseAudio()" type="button">Pause Audio</button> 

<script>
    var x = document.getElementById("myAudio");

    function playAudio() {
        x.play();
    }

    function pauseAudio() {
        x.pause();
    } 
</script>
    </form>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</body>
</html>
