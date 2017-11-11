<%@ Page Title="" Language="C#" MasterPageFile="~/Base.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="content1" runat="Server">

    <script type="text/javascript">
        function algunMetodo() {
            // este javascript es mio >:3c
            var edad = document.getElementById("<%=edad.ClientID%>").value;
            //submitOK = "true";

            if (isNaN(edad) || age > 1 || age < 18) {
                alert("debes ser mayor de 18 para ingresar");
                submitOK = "false";
            }
            //if (submitOK == "false") {
            //    //return false;
            //}

            return true;
        }
    </script>

    <asp:Label ID="Label1" runat="server" Text="ingresa tu nombre de jugador"></asp:Label>
        <asp:TextBox ID="TextBox1" ForeColor="Black" runat="server"></asp:TextBox>

    <br />
    <asp:Label ID="Label9" runat="server"  Text="ingresa tu edad"></asp:Label>
    <asp:TextBox ID="edad" type="date" ForeColor="Black" runat="server"></asp:TextBox>

    <br />

    <asp:Button ID="btnnombre" runat="server" Text="confirmar nombre" OnClick="confirmarnombre" OnClientClick="return algunMetodo();" />
    <br />
    <asp:Label ID="label2" runat="server" Visible="false" Text="ingrese las cartas elegidas en las rondas"></asp:Label>
    <asp:Label ID="label3" runat="server" Visible="false" Text="ronda 1"></asp:Label>
    <br />
    <asp:TextBox ID="tfinal1" ForeColor="Black" runat="server" Visible="false" Text="nombre de la Carta"></asp:TextBox>
    <asp:TextBox ID="tfinal2" ForeColor="Black" runat="server" Visible="false" Text="posicion" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label ID="label4" runat="server" Visible="false" Text="ronda 2"></asp:Label>
    <br />
    <asp:TextBox ID="tfinal3" ForeColor="Black" runat="server" Visible="false" Text="nombre de la Carta"></asp:TextBox>
    <asp:TextBox ID="tfinal4" ForeColor="Black" runat="server" Visible="false" Text="posicion" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label ID="label5" runat="server" Visible="false" Text="ronda 3"></asp:Label>
    <br />
    <asp:TextBox ID="tfinal5" ForeColor="Black" runat="server" Visible="false" Text="nombre de la Carta"></asp:TextBox>
    <asp:TextBox ID="tfinal6" ForeColor="Black" runat="server" Visible="false" Text="posicion" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label ID="label6" runat="server" Visible="false" Text="ronda 4"></asp:Label>
    <br />
    <asp:TextBox ID="tfinal7" ForeColor="Black" runat="server" Visible="false" Text="nombre de la Carta"></asp:TextBox>
    <asp:TextBox ID="tfinal8" ForeColor="Black" runat="server" Visible="false" Text="posicion" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label ID="label7" runat="server" Visible="false" Text="ronda 5"></asp:Label>
    <br />
    <asp:TextBox ID="tfinal9" ForeColor="Black" runat="server" Visible="false" Text="carta"></asp:TextBox>
    <asp:TextBox ID="tfinal10" ForeColor="Black" runat="server" Visible="false" Text="posicion" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Button ID="btncalculo" runat="server" Visible="false" Text="Calcular puntaje" OnClick="calculo_puntaje" />


    <asp:ImageButton ID="ImageButton1" Visible="false" CssClass="rounded img-fluid" heigh="200px" Width="200px" runat="server" OnClick="choose" />
    &nbsp;<asp:ImageButton ID="ImageButton2" Visible="false" CssClass="rounded  img-fluid" heigh="200px" Width="200px" runat="server" OnClick="choose" />
    <asp:ImageButton ID="ImageButton3" Visible="false" CssClass="rounded img-fluid" heigh="200px" Width="200px" runat="server" OnClick="choose" />
    <asp:ImageButton ID="ImageButton4" Visible="false" CssClass="rounded img-fluid" heigh="200px" Width="200px" runat="server" OnClick="choose" />
    <br />
    <asp:Button ID="Button1" Visible="false" runat="server" OnClick="siguiente_ronda" Text="Siguiente Ronda" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content2" runat="server">
    <asp:Label ID="Nombrelabel" runat="server" Text="Label"></asp:Label>
    <br />

    <asp:Label ID="Puntajelabel" runat="server" Text="Label"></asp:Label>
</asp:Content>
