using FontAwesome6.Fonts;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using Modulos;
using Windows.Storage;

namespace Steam_Grid
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            CargarObjetos();

            BarraTitulo.Generar(this);
            BarraTitulo.CambiarTitulo(null);
            Cerrar.Cargar();
            Pestañas.Cargar();
            ScrollViewers.Cargar();
            Menu.Cargar();
            Opciones.Cargar();
            Steam.CargarRuta();
            Steam.CargarUltimoUsuario();
            Steam.CargarJuegosInstalados();
        }

        public void CargarObjetos()
        {
            Objetos.ventana = ventana;
            Objetos.gridTitulo = gridTitulo;
            Objetos.tbTitulo = tbTitulo;
            Objetos.nvPrincipal = nvPrincipal;
            Objetos.nvItemMenu = nvItemMenu;
            Objetos.menuItemMenu = menuItemMenu;
            Objetos.nvItemOpciones = nvItemOpciones;
            Objetos.nvItemSubirArriba = nvItemSubirArriba;
            Objetos.gridCierre = gridCierre;

            //-------------------------------------------------------------------

            Objetos.botonCerrarAppSi = botonCerrarAppSi;
            Objetos.botonCerrarAppNo = botonCerrarAppNo;
            Objetos.iconoCerrarAppAzar = iconoCerrarAppAzar;
            Objetos.tbCerrarAppAzar = tbCerrarAppAzar;
            Objetos.botonCerrarAppAzar = botonCerrarAppAzar;

            //-------------------------------------------------------------------

            Objetos.gridJuegos = gridJuegos;
            Objetos.gridCambiarImagenes = gridCambiarImagenes;
            Objetos.gridOpciones = gridOpciones;

            //-------------------------------------------------------------------

            Objetos.svJuegos = svJuegos;
            Objetos.prJuegos = prJuegos;
            Objetos.gvJuegos = gvJuegos;
            Objetos.ttipJuegos = ttipJuegos;

            //-------------------------------------------------------------------

            Objetos.gridCambiarImagenesCargando = gridCambiarImagenesCargando;
            Objetos.gridCambiarImagenesCargado = gridCambiarImagenesCargado;
            Objetos.spCambiarImagenesBotones = spCambiarImagenesBotones;
            Objetos.svCambiarImagenes = svCambiarImagenes;
            Objetos.spCambiarImagenesPestañas = spCambiarImagenesPestanas;
            Objetos.tbCambiarImagenesTitulo = tbCambiarImagenesTitulo;
            Objetos.tbImagenesLibraryCantidad = tbImagenesLibraryCantidad;
            Objetos.tbImagenesHeroesCantidad = tbImagenesHeroesCantidad;
            Objetos.tbImagenesLogosCantidad = tbImagenesLogosCantidad;
            Objetos.gvImagenesLibrary = gvImagenesLibrary;
            Objetos.gvImagenesHeroes = gvImagenesHeroes;
            Objetos.gvImagenesLogos = gvImagenesLogos;
            Objetos.botonCambiarImagenesLimpiar = botonCambiarImagenesLimpiar;
            Objetos.ttipCambiarImagenes = ttipCambiarImagenes;

            //-------------------------------------------------------------------

            Objetos.spOpcionesBotones = spOpcionesBotones;
            Objetos.svOpciones = svOpciones;
            Objetos.spOpcionesPestañas = spOpcionesPestanas;
            Objetos.tbOpcionesSteamGridDBAPI = tbOpcionesSteamGridDBAPI;
            Objetos.botonOpcionesSteamGridDBAPIMostrarAyuda = botonOpcionesSteamGridDBAPIMostrarAyuda;
            Objetos.botonOpcionesSteamCambiarCarpeta = botonOpcionesSteamCambiarCarpeta;
            Objetos.tbOpcionesSteamRutaCarpeta = tbOpcionesSteamRutaCarpeta;
            Objetos.tbOpcionesSteamUsuario = tbOpcionesSteamUsuario;
            Objetos.tsOpcionesSteamCarpeta = tsOpcionesSteamCarpeta;
            Objetos.cbOpcionesIdioma = cbOpcionesIdioma;
            Objetos.cbOpcionesPantalla = cbOpcionesPantalla;
            Objetos.botonOpcionesLimpiar = botonOpcionesLimpiar;
        }

        public static class Objetos
        {
            public static Window ventana { get; set; }
            public static Grid gridTitulo { get; set; }
            public static TextBlock tbTitulo { get; set; }
            public static NavigationView nvPrincipal { get; set; }
            public static NavigationViewItem nvItemMenu { get; set; }
            public static MenuFlyout menuItemMenu { get; set; }
            public static NavigationViewItem nvItemOpciones { get; set; }
            public static NavigationViewItem nvItemSubirArriba { get; set; }
            public static Grid gridCierre { get; set; }

            //-------------------------------------------------------------------

            public static Button botonCerrarAppSi { get; set; }
            public static Button botonCerrarAppNo { get; set; }
            public static FontAwesome iconoCerrarAppAzar { get; set; }
            public static TextBlock tbCerrarAppAzar { get; set; }
            public static Button botonCerrarAppAzar { get; set; }

            //-------------------------------------------------------------------

            public static Grid gridJuegos { get; set; }
            public static Grid gridCambiarImagenes { get; set; }
            public static Grid gridOpciones { get; set; }

            //-------------------------------------------------------------------

            public static ScrollViewer svJuegos { get; set; }
            public static ProgressRing prJuegos { get; set; }
            public static GridView gvJuegos { get; set; }
            public static TeachingTip ttipJuegos { get; set; }

            //-------------------------------------------------------------------

            public static Grid gridCambiarImagenesCargando { get; set; }
            public static Grid gridCambiarImagenesCargado { get; set; }
            public static StackPanel spCambiarImagenesBotones { get; set; }
            public static ScrollViewer svCambiarImagenes { get; set; }
            public static StackPanel spCambiarImagenesPestañas { get; set; }
            public static TextBlock tbCambiarImagenesTitulo { get; set; }
            public static TextBlock tbImagenesLibraryCantidad { get; set; }
            public static TextBlock tbImagenesHeroesCantidad { get; set; }
            public static TextBlock tbImagenesLogosCantidad { get; set; }
            public static GridView gvImagenesLibrary { get; set; }
            public static GridView gvImagenesHeroes { get; set; }
            public static GridView gvImagenesLogos { get; set; }
            public static Button botonCambiarImagenesLimpiar { get; set; }
            public static TeachingTip ttipCambiarImagenes { get; set; }

            //-------------------------------------------------------------------

            public static StackPanel spOpcionesBotones { get; set; }
            public static ScrollViewer svOpciones { get; set; }
            public static StackPanel spOpcionesPestañas { get; set; }
            public static TextBox tbOpcionesSteamGridDBAPI { get; set; }
            public static Button botonOpcionesSteamGridDBAPIMostrarAyuda { get; set; }
            public static Button botonOpcionesSteamCambiarCarpeta { get; set; }
            public static TextBlock tbOpcionesSteamRutaCarpeta { get; set; }
            public static TextBox tbOpcionesSteamUsuario { get; set; }
            public static ToggleSwitch tsOpcionesSteamCarpeta { get; set; }
            public static ComboBox cbOpcionesIdioma { get; set; }
            public static ComboBox cbOpcionesPantalla { get; set; }
            public static Button botonOpcionesLimpiar { get; set; }
        }

        private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            Pestañas.CreadorItems(FontAwesome6.EFontAwesomeIcon.Brands_Steam, recursos.GetString("Games"));

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            if (datos.Values["OpcionesSteamGridDBUsuario"] != null)
            {
                StackPanel sp = (StackPanel)Objetos.nvPrincipal.MenuItems[1];
                Pestañas.Visibilidad(gridJuegos, true, sp, true);
            }
        }

        private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ResourceLoader recursos = new ResourceLoader();

            if (args.InvokedItemContainer != null)
            {
                if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem2))
                {
                    NavigationViewItem2 item = args.InvokedItemContainer as NavigationViewItem2;

                    if (item.Name == "nvItemMenu")
                    {

                    }
                    else if (item.Name == "nvItemOpciones")
                    {
                        Pestañas.Visibilidad(gridOpciones, true, null, false);
                        BarraTitulo.CambiarTitulo(recursos.GetString("Options"));
                        ScrollViewers.EnseñarSubir(svOpciones);
                    }
                }
            }

            if (args.InvokedItem != null)
            {
                if (args.InvokedItem.GetType() == typeof(StackPanel2))
                {
                    StackPanel2 sp = (StackPanel2)args.InvokedItem;

                    if (sp.Children[1] != null)
                    {
                        if (sp.Children[1].GetType() == typeof(TextBlock))
                        {
                            TextBlock tb = sp.Children[1] as TextBlock;

                            if (tb.Text == recursos.GetString("Games"))
                            {
                                Pestañas.Visibilidad(gridJuegos, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svJuegos);
                            }
                        }
                    }
                }
            }
        }
    }
}
