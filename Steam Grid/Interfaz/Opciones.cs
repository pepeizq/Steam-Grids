using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Collections.Generic;
using Windows.Globalization;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.System.UserProfile;
using Windows.UI;
using WinRT.Interop;
using static Steam_Grid.MainWindow;

namespace Interfaz
{
    public static class Opciones
    {
        public static void Cargar()
        {
            Objetos.nvItemOpciones.PointerEntered += Animaciones.EntraRatonNvItem2;
            Objetos.nvItemOpciones.PointerExited += Animaciones.SaleRatonNvItem2;

            //---------------------------------

            int i = 0;
            foreach (Button2 boton in Objetos.spOpcionesBotones.Children)
            {
                boton.Tag = i;
                boton.Click += CambiarPestaña;
                boton.PointerEntered += Animaciones.EntraRatonBoton2;
                boton.PointerExited += Animaciones.SaleRatonBoton2;

                i += 1;
            }

            CambiarPestaña(i - 1);

            //---------------------------------

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            if (datos.Values["OpcionesSteamGridDBUsuario"] == null)
            {
                Pestañas.Visibilidad(Objetos.gridOpciones, true, null, false);
                CambiarPestaña(0);
            }
            else
            {
                Objetos.tbOpcionesSteamGridDBAPI.Text = datos.Values["OpcionesSteamGridDBUsuario"].ToString();
            }

            Objetos.tbOpcionesSteamGridDBAPI.TextChanged += CambiarUsuario;

            Objetos.botonOpcionesSteamGridDBAPIMostrarAyuda.Click += MostrarAyuda;
            Objetos.botonOpcionesSteamGridDBAPIMostrarAyuda.PointerEntered += Animaciones.EntraRatonBoton2;
            Objetos.botonOpcionesSteamGridDBAPIMostrarAyuda.PointerExited += Animaciones.SaleRatonBoton2;

            Objetos.tbOpcionesSteamUsuario.TextChanged += CambiarUsuarioSteam;

            //---------------------------------

            Objetos.botonOpcionesSteamCambiarCarpeta.Click += CambiarSteamCarpeta;
            Objetos.botonOpcionesSteamCambiarCarpeta.PointerEntered += Animaciones.EntraRatonBoton2;
            Objetos.botonOpcionesSteamCambiarCarpeta.PointerExited += Animaciones.SaleRatonBoton2;

            //---------------------------------

            if (datos.Values["OpcionesSteamCarpeta"] == null)
            {
                datos.Values["OpcionesSteamCarpeta"] = false;
            }

            Objetos.tsOpcionesSteamCarpeta.IsOn = (bool)datos.Values["OpcionesSteamCarpeta"];
            Objetos.tsOpcionesSteamCarpeta.Toggled += TsSteamCarpeta;

            //---------------------------------

            IReadOnlyList<string> idiomasApp = ApplicationLanguages.ManifestLanguages;

            foreach (string idioma in idiomasApp)
            {
                Objetos.cbOpcionesIdioma.Items.Add(idioma);
            }

            if (datos.Values["OpcionesIdioma"] == null)
            {
                IReadOnlyList<string> idiomasUsuario = GlobalizationPreferences.Languages;
                bool seleccionado = false;

                foreach (string idioma in idiomasUsuario)
                {
                    foreach (string idioma2 in idiomasApp)
                    {
                        if (idioma2 == idioma)
                        {
                            Objetos.cbOpcionesIdioma.SelectedItem = idioma2;
                            seleccionado = true;
                        }
                    }
                }

                if (seleccionado == false)
                {
                    Objetos.cbOpcionesIdioma.SelectedIndex = 0;
                }

                datos.Values["OpcionesIdioma"] = Objetos.cbOpcionesIdioma.SelectedItem;
            }
            else
            {
                Objetos.cbOpcionesIdioma.SelectedItem = datos.Values["OpcionesIdioma"];
            }

            ApplicationLanguages.PrimaryLanguageOverride = Objetos.cbOpcionesIdioma.SelectedItem.ToString();
          
            Objetos.cbOpcionesIdioma.SelectionChanged += CbOpcionIdioma;
            Objetos.cbOpcionesIdioma.PointerEntered += Animaciones.EntraRatonComboCaja2;
            Objetos.cbOpcionesIdioma.PointerExited += Animaciones.SaleRatonComboCaja2;

            //---------------------------------

            if (datos.Values["OpcionesPantalla"] == null)
            {
                datos.Values["OpcionesPantalla"] = 0;
            }

            Objetos.cbOpcionesPantalla.SelectionChanged += CbOpcionPantalla;
            Objetos.cbOpcionesPantalla.PointerEntered += Animaciones.EntraRatonComboCaja2;
            Objetos.cbOpcionesPantalla.PointerExited += Animaciones.SaleRatonComboCaja2;
            Objetos.cbOpcionesPantalla.SelectedIndex = (int)datos.Values["OpcionesPantalla"];

            //---------------------------------

            Objetos.botonOpcionesLimpiar.Click += BotonOpcionLimpiar;
            Objetos.botonOpcionesLimpiar.PointerEntered += Animaciones.EntraRatonBoton2;
            Objetos.botonOpcionesLimpiar.PointerExited += Animaciones.SaleRatonBoton2;
        }

        private static void CambiarPestaña(object sender, RoutedEventArgs e)
        {
            Button2 botonPulsado = sender as Button2;
            int pestañaMostrar = (int)botonPulsado.Tag;
            CambiarPestaña(pestañaMostrar);
        }

        private static void CambiarPestaña(int botonPulsado)
        {
            SolidColorBrush colorPulsado = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]);
            colorPulsado.Opacity = 0.6;

            int i = 0;
            foreach (Button2 boton in Objetos.spOpcionesBotones.Children)
            {
                if (i == botonPulsado)
                {
                    boton.Background = colorPulsado;
                }
                else
                {
                    boton.Background = new SolidColorBrush(Colors.Transparent);
                }

                i += 1;
            }

            foreach (StackPanel sp in Objetos.spOpcionesPestañas.Children)
            {
                sp.Visibility = Visibility.Collapsed;
            }

            StackPanel spMostrar = Objetos.spOpcionesPestañas.Children[botonPulsado] as StackPanel;
            spMostrar.Visibility = Visibility.Visible;
        }

        public static void CambiarUsuario(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            datos.Values["OpcionesSteamGridDBUsuario"] = tb.Text;
        }

        public static void CambiarUsuarioSteam(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            datos.Values["OpcionesSteamUsuario"] = tb.Text;
        }

        public static async void MostrarAyuda(object sender, RoutedEventArgs e)
        {
            Image imagen = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Ayuda/steamgriddb.png")),
                Width = 995,
                Height = 352
            };

            ResourceLoader recursos = new ResourceLoader();

            ContentDialog ventana = new ContentDialog
            {
                RequestedTheme = ElementTheme.Dark,
                PrimaryButtonText = recursos.GetString("Open2"),
                CloseButtonText = recursos.GetString("Close"),
                Content = imagen,
                XamlRoot = Objetos.ventana.Content.XamlRoot,
                Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"])
            };

            ventana.PrimaryButtonClick += AbrirEnlace;

            await ventana.ShowAsync();
        }

        private static async void AbrirEnlace(ContentDialog ventana, ContentDialogButtonClickEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://www.steamgriddb.com/profile/preferences/api"));
        }

        public static async void CambiarSteamCarpeta(object sender, RoutedEventArgs e)
        {
            FolderPicker carpetaPicker = new FolderPicker();
            carpetaPicker.FileTypeFilter.Add("*");

            IntPtr hwnd = WindowNative.GetWindowHandle(Objetos.ventana);
            InitializeWithWindow.Initialize(carpetaPicker, hwnd);

            StorageFolder carpetaElegida = null;

            try
            {
                carpetaElegida = await carpetaPicker.PickSingleFolderAsync();
            }
            catch { }

            if (carpetaElegida != null)
            {
                Objetos.tbOpcionesSteamRutaCarpeta.Text = carpetaElegida.Path;

                ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
                datos.Values["OpcionesSteamInstalacion"] = carpetaElegida.Path;
            }
        }

        public static void TsSteamCarpeta(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesSteamCarpeta"] = ts.IsOn;
        }

        public static void CbOpcionIdioma(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesIdioma"] = cb.SelectedItem;

            ApplicationLanguages.PrimaryLanguageOverride = datos.Values["OpcionesIdioma"].ToString();
        }
       
        public static void CbOpcionPantalla(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesPantalla"] = cb.SelectedIndex;

            IntPtr ventanaInt = WindowNative.GetWindowHandle(Objetos.ventana);
            WindowId ventanaID = Win32Interop.GetWindowIdFromWindow(ventanaInt);
            AppWindow ventana2 = AppWindow.GetFromWindowId(ventanaID);

            if (cb.SelectedIndex == 0)
            {
                ventana2.SetPresenter(AppWindowPresenterKind.Default);
            }
            else if (cb.SelectedIndex == 1)
            {
                ventana2.SetPresenter(AppWindowPresenterKind.FullScreen);
            }
            else if (cb.SelectedIndex == 2)
            {
                ventana2.SetPresenter(AppWindowPresenterKind.Overlapped);
            }
        }

        public static async void BotonOpcionLimpiar(object sender, RoutedEventArgs e)
        {
            await ApplicationData.Current.ClearAsync();
            AppInstance.Restart(null);
        }
    }
}
