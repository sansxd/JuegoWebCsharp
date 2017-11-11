using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void kill(object sender, EventArgs e)
    {
        Session["baraja"] = null;
        Session["memorySave"] = null;
        Session["NombreJugador"] = null;
    }
}
