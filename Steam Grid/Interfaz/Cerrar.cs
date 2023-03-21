using FontAwesome6;
using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using Windows.System;
using static Steam_Grid.MainWindow;

namespace Interfaz
{
    public static class Cerrar
    {
        private static bool cerrar2 = false;

        public static void Cargar()
        {
            Objetos.ventana.Closed += VentanaCerrar;

            Objetos.botonCerrarAppSi.Click += SiCerrar;
            Objetos.botonCerrarAppSi.PointerEntered += Animaciones.EntraRatonBoton2;
            Objetos.botonCerrarAppSi.PointerExited += Animaciones.SaleRatonBoton2;

            Objetos.botonCerrarAppNo.Click += NoCerrar;
            Objetos.botonCerrarAppNo.PointerEntered += Animaciones.EntraRatonBoton2;
            Objetos.botonCerrarAppNo.PointerExited += Animaciones.SaleRatonBoton2;

            Objetos.botonCerrarAppAzar.Click += AbrirEnlaceAzar;
            Objetos.botonCerrarAppAzar.PointerEntered += Animaciones.EntraRatonBoton2;
            Objetos.botonCerrarAppAzar.PointerExited += Animaciones.SaleRatonBoton2;
        }

        private static void VentanaCerrar(object sender, WindowEventArgs e)
        {
            if (cerrar2 == false)
            {
                e.Handled = true;
                Objetos.gridCierre.Visibility = Visibility.Visible;

                ResourceLoader recursos = new ResourceLoader();

                List<MensajeCerrar> mensajes = new List<MensajeCerrar>
                {
                    new MensajeCerrar(EFontAwesomeIcon.Brands_Github, recursos.GetString("AppClosingMessage1"), AppDatos.Github),
                    //new MensajeCerrar(EFontAwesomeIcon.Solid_ThumbsUp, recursos.GetString("AppClosingMessage2"), AppDatos.Votar),
                    new MensajeCerrar(EFontAwesomeIcon.Solid_Desktop, recursos.GetString("AppClosingMessage3"), AppDatos.NotasParches)
                };

                Random azar = new Random();
                int numeroElegido = azar.Next(0, mensajes.Count);

                Objetos.iconoCerrarAppAzar.Icon = mensajes[numeroElegido].icono;
                Objetos.tbCerrarAppAzar.Text = mensajes[numeroElegido].mensaje;
                Objetos.botonCerrarAppAzar.Tag = mensajes[numeroElegido].enlace;
            }
        }

        private static void SiCerrar(object sender, RoutedEventArgs e)
        {
            cerrar2 = true;

            Application app = Application.Current;
            app.Exit();
        }

        private static void NoCerrar(object sender, RoutedEventArgs e)
        {
            Objetos.gridCierre.Visibility = Visibility.Collapsed;
        }

        private static async void AbrirEnlaceAzar(object sender, RoutedEventArgs e)
        {
            Button2 boton = sender as Button2;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));
        }
    }

    public class MensajeCerrar
    {
        public EFontAwesomeIcon icono { get; set; }
        public string mensaje { get; set; }
        public string enlace { get; set; }

        public MensajeCerrar(EFontAwesomeIcon Icono, string Mensaje, string Enlace)
        {
            icono = Icono;
            mensaje = Mensaje;
            enlace = Enlace;
        }
    }
}
