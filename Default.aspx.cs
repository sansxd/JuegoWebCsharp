using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{

    SqlCommand cmd = new SqlCommand();
    SqlConnection con = new SqlConnection();

    //contador ronda

    //carta seleccionada
    public string cartaseleccionada;
    //posicion de la carta seleccionada
    public int PosicionCarta;
    //lista para guardar las rondas

    //arreglo para guardar la baraja

    public class cartas
    {
        public string valorCarta;
        public string imagenCarta;
        public cartas(string b1, string b2)
        {
            this.valorCarta = b1;
            this.imagenCarta = b2;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = @"Data Source=LOD\SQLEXPRESS;Initial Catalog=Datamain;Integrated Security=True";
        con.Open();

        if (!Page.IsPostBack)
        {
            //al encerrar este metodo y ejecucuion dentro de un !Ispostback
            //aseguro que estas variables importantes no se vuelvan a crear en cada refresh de la clase
            void baraja()
            {
                //Creacion de  4 objetos cartas con sus respectivos valores bases
                cartas carta1 = new cartas("corazon", @"image\1.jpg");
                cartas carta2 = new cartas("pica", @"image\2.jpg");
                cartas carta3 = new cartas("trebol", @"image\3.PNG");
                cartas carta4 = new cartas("diamante", @"image\4.jpg");


                //Creacion arreglo contenedor de los objetos cartas
                cartas[] cartasparabarajar = new cartas[] { carta1, carta2, carta3, carta4 };
                Session["baraja"] = cartasparabarajar;
                //creacion de la memoria contenedora de las rondas
                List<Tuple<int, string, int>> lista = new List<Tuple<int, string, int>>();
                Session["savetest"] = lista;
                //contador de las rondas instanciado aqui para evitar problemas de reseteado del valor
                int numronda = 1;
                Session["savecontador"] = numronda;
                //puntaje obtenido
                int puntaje = 0;
                Session["savepuntaje"] = puntaje;

            }
            baraja();
        }

    }


    protected void siguiente_ronda(object sender, EventArgs e)
    {
        void cambiarposicion(cartas[] cartasparabarajar)
        {
            //instancia de RNG
            Random random = new Random();
            //index no empieza en el valor uno por lo tanto index-- recorrera el algoritmo hasta quedar en 0
            for (int index = cartasparabarajar.Length - 1; index >= 0; index--)
            {
                //instancia y ejecucion del metodo void cambio
                //int p1 se le asigna el valor de random
                //int p2 se le asigna el valor del index actual
                //finalmente se usa el arreglo previamente creado 
                cambio(random.Next(index + 1), index, cartasparabarajar);
                if (index == 0)
                {

                    //ojo este proceso esta explicado en el metodo confirmarnombre
                    //Imagenes con nuevas posiciones
                    ImageButton1.AlternateText = cartasparabarajar[0].valorCarta;
                    ImageButton2.AlternateText = cartasparabarajar[1].valorCarta;
                    ImageButton3.AlternateText = cartasparabarajar[2].valorCarta;
                    ImageButton4.AlternateText = cartasparabarajar[3].valorCarta;
                    ImageButton1.TabIndex = 1;
                    ImageButton2.TabIndex = 2;
                    ImageButton3.TabIndex = 3;
                    ImageButton4.TabIndex = 4;

                    ImageButton1.ImageUrl = cartasparabarajar[0].imagenCarta;
                    ImageButton2.ImageUrl = cartasparabarajar[1].imagenCarta;
                    ImageButton3.ImageUrl = cartasparabarajar[2].imagenCarta;
                    ImageButton4.ImageUrl = cartasparabarajar[3].imagenCarta;
                }
            }

        }
        //metodo complementario de cambiar posicion
        void cambio(int p1, int p2, cartas[] cartasparabarajar)
        {
            //objeto de cartas temporal el cual toma el valor de la carta seleccionada por index
            cartas temp = cartasparabarajar[p1];

            //arreglo con la posicion dictada por random toma el valor del arreglo con posicion dictada por index
            //a la vista no explica mucho pero su funcionalidad de esta linea es
            //asignar la carta diferente a nuestra posicion p1 ya guardada
            //bajo esta logica tanto p1 como p2 toman el mismo valor
            cartasparabarajar[p1] = cartasparabarajar[p2];

            //para evitar duplicado de valor en p1 y p2, se le asigna el valor guardado de p1 a p2
            //arreglo con posicion dictada en index toma el valor de nuestro objeto temporal
            cartasparabarajar[p2] = temp;

        }





        Button1.Visible = false;
        cartas[] BarajaSave = (cartas[])Session["baraja"];
        cambiarposicion(BarajaSave);
        ImageButton1.Visible = true;
        ImageButton2.Visible = true;
        ImageButton3.Visible = true;
        ImageButton4.Visible = true;

    }

    protected void choose(object sender, ImageClickEventArgs e)
    {
        //previamente en !ispostback declare variables bases a nuestras session que no se volveran a repetir
        //de ese modo no tengo que instanciar una  variable session en este metodo que obviamente se repetira
        //en cada accion y asi reseteando el valor de session
        int numrondas = Convert.ToInt16(Session["savecontador"]);





        //creacion de un objeto  button momentaneo que es replica del cual se ejecuto el evento onlick
        ImageButton momentanea = (sender as ImageButton);
        //captura de valores
        string cartaToc = Convert.ToString(momentanea.AlternateText);
        int posicion = Convert.ToInt16(momentanea.TabIndex);
        //mismo proceso del principio ahora con la variable list
        List<Tuple<int, string, int>> listo = new List<Tuple<int, string, int>>();
        listo = (List<Tuple<int, string, int>>)Session["savetest"];
        //al saber si contemos las 5 rondas en nuestro objeto lista procedemos a continuar con la parte 
        //final del juego si este if cumple la condicion, de lo contrario sigue con la ronda
        if (listo.Count == 5)
        {


            //ocultado de las cartas 
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            //ocultado  del boton de rondas
            Button1.Visible = false;

            //inicia la parte final del juego la cual es  calcular  el puntaje
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            //textboxs
            tfinal1.Visible = true;
            tfinal2.Visible = true;
            tfinal3.Visible = true;
            tfinal4.Visible = true;
            tfinal5.Visible = true;
            tfinal6.Visible = true;
            tfinal7.Visible = true;
            tfinal8.Visible = true;
            tfinal9.Visible = true;
            tfinal10.Visible = true;
            //btn para calculo de puntaje
            btncalculo.Visible = true;

        }
        else
        {
            listo.Add(new Tuple<int, string, int>(numrondas, cartaToc, posicion));
            numrondas++;
            //al finalizar la operacion vuelvo  guardo  el valor dentro de Session
            Session["savecontador"] = numrondas;

            //mismo proceso anterior
            Session["memorySave"] = listo;


            //ocultado de las cartas 
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            //habilitacion del boton  que nos llevara a  la siguiente  ronda
            Button1.Visible = true;
        }
    }

    protected void confirmarnombre(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("insert into usuario" + "(nombre) values(@name)", con);
        cmd.Parameters.AddWithValue("@name", TextBox1.Text);
        cmd.ExecuteNonQuery();

        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        //practicamente el inicio del juego
        string nombrePlayer = TextBox1.Text;
        //salvado del nombre de player
        //en este caso mataremos la variable nombrejugador al finalizar el juego para evitar errores de mismo nombre
        //esto es evitable creando una variable dictionary y usando un try/catch para notificar al usuario el ingreso de otro nombre
        Session["NombreJugador"] = nombrePlayer;
        Label1.Visible = false;
        TextBox1.Visible = false;
        btnnombre.Visible = false;
        Label9.Visible = false;
        edad.Visible = false;


        //ocultado del principio

        //mismo proceso explicado en metodo choose
        cartas[] cartasparabarajar = (cartas[])Session["baraja"];

        //asignacion de nombres a los objetos para posterior guardado al seleccionarlas
        ImageButton1.AlternateText = cartasparabarajar[0].valorCarta;
        ImageButton2.AlternateText = cartasparabarajar[1].valorCarta;
        ImageButton3.AlternateText = cartasparabarajar[2].valorCarta;
        ImageButton4.AlternateText = cartasparabarajar[3].valorCarta;
        //si bien tabindex cumple una funcion diferente a como la usaremos ahora
        //si quiero saber la posicion de la carta elegida solamente tengo que consultar .tabindex
        ImageButton1.TabIndex = 1;
        ImageButton2.TabIndex = 2;
        ImageButton3.TabIndex = 3;
        ImageButton4.TabIndex = 4;
        //usando la propiedad Imageurl  y el atributo imagencarta que guarda la ruta de enlace de esta
        //puedo darle la imagen correspondiente a la carta
        ImageButton1.ImageUrl = cartasparabarajar[0].imagenCarta;
        ImageButton2.ImageUrl = cartasparabarajar[1].imagenCarta;
        ImageButton3.ImageUrl = cartasparabarajar[2].imagenCarta;
        ImageButton4.ImageUrl = cartasparabarajar[3].imagenCarta;
        //habilitacion de  las cartas y inicio del juego
        ImageButton1.Visible = true;
        ImageButton2.Visible = true;
        ImageButton3.Visible = true;
        ImageButton4.Visible = true;
    }

    protected void calculo_puntaje(object sender, EventArgs e)
    {

        List<Tuple<int, string, int>> listo = new List<Tuple<int, string, int>>();
        listo = (List<Tuple<int, string, int>>)Session["savetest"];
        for (int i = 0; i <= listo.Count; i++)
        {
            //para acceder a cada uno de los textbox donde ingresamos datos
            //lo mejor es colocar variables if a nuestro int i el cual recorre y a la vez representa el numero de rondas

            if (i == 0)
            {
                if (Convert.ToString(tfinal1.Text) == listo[i].Item2 && Convert.ToInt16(tfinal2.Text) == listo[i].Item3)
                {
                    int puntaje = Convert.ToInt16(Session["savepuntaje"]);
                    puntaje++;
                    Session["savepuntaje"] = puntaje;
                }
            }
            if (i == 1)
            {
                if (Convert.ToString(tfinal3.Text) == listo[i].Item2 && Convert.ToInt16(tfinal4.Text) == listo[i].Item3)
                {
                    int puntaje = Convert.ToInt16(Session["savepuntaje"]);
                    puntaje++;
                    Session["savepuntaje"] = puntaje;
                }
            }
            if (i == 2)
            {
                if (Convert.ToString(tfinal5.Text) == listo[i].Item2 && Convert.ToInt16(tfinal6.Text) == listo[i].Item3)
                {
                    int puntaje = Convert.ToInt16(Session["savepuntaje"]);
                    puntaje++;
                    Session["savepuntaje"] = puntaje;
                }
            }
            if (i == 3)
            {
                if (Convert.ToString(tfinal7.Text) == listo[i].Item2 && Convert.ToInt16(tfinal8.Text) == listo[i].Item3)
                {
                    int puntaje = Convert.ToInt16(Session["savepuntaje"]);
                    puntaje++;
                    Session["savepuntaje"] = puntaje;
                }
            }
            //if final donde asignaremos la variable nombrejugador y puntaje a nuestra pagina web
            if (i == 4)
            {
                if (Convert.ToString(tfinal9.Text) == listo[i].Item2 && Convert.ToInt16(tfinal10.Text) == listo[i].Item3)
                {
                    int puntaje = Convert.ToInt16(Session["savepuntaje"]);
                    puntaje++;
                    Session["savepuntaje"] = puntaje;
                    //ocultado de los textbox y buttons

                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    //textboxs
                    tfinal1.Visible = false;
                    tfinal2.Visible = false;
                    tfinal3.Visible = false;
                    tfinal4.Visible = false;
                    tfinal5.Visible = false;
                    tfinal6.Visible = false;
                    tfinal7.Visible = false;
                    tfinal8.Visible = false;
                    tfinal9.Visible = false;
                    tfinal10.Visible = false;
                    //btn para calculo de puntaje
                    btncalculo.Visible = false;

                    //momento final donde asignamos valores
                    Nombrelabel.Text = (string)Session["NombreJugador"];
                    Puntajelabel.Text = (string)Convert.ToString(Session["savepuntaje"]);
                }
            }
        }
    }

    
}
