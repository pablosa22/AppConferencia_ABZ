using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppConferenciaABP
{
    public partial class ConvocacaoAtiva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            
            // player.SoundLocation = "C:/inetpub/wwwroot/AppConferenciaABZ/Sons/Policia.wav";                                    
            // player.Play();
        }

        protected void btEntrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PainelOSIndustria.aspx?acesso="+1);
            //Response.Redirect("AppConferencia.aspx?mat=" + mat + "&name=" + name);
        }
    }
}