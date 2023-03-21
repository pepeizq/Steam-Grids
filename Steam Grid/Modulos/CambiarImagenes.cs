using Herramientas;
using Interfaz;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.ApplicationModel.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using static Steam_Grid.MainWindow;

namespace Modulos
{
    public static class CambiarImagenes
    {
        public static async void Cargar(string id, string nombre)
        {
            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            if (datos.Values["OpcionesSteamGridDBUsuario"] == null)
            {
                ResourceLoader recursos = new ResourceLoader();

                Objetos.ttipJuegos.Title = recursos.GetString("MessageAddSteamGridDBAPI1");
                Objetos.ttipJuegos.Subtitle = recursos.GetString("MessageAddSteamGridDBAPI2");
                Objetos.ttipJuegos.IsOpen = true;
            }
            else
            {
                Pestañas.Visibilidad(Objetos.gridCambiarImagenes, true, null, false);

                Objetos.gridCambiarImagenesCargando.Visibility = Visibility.Visible;
                Objetos.gridCambiarImagenesCargado.Visibility = Visibility.Collapsed;

                int i = 0;
                foreach (object boton in Objetos.spCambiarImagenesBotones.Children)
                {
                    if (boton.GetType() == typeof(Button2))
                    {
                        Button2 boton2 = boton as Button2;

                        boton2.Tag = i;
                        boton2.Click += CambiarPestaña;
                        boton2.PointerEntered += Animaciones.EntraRatonBoton2;
                        boton2.PointerExited += Animaciones.SaleRatonBoton2;

                        i += 1;
                    }
                }

                CambiarPestaña(0);

                //------------------------------------------------------

                Objetos.tbCambiarImagenesTitulo.Text = nombre;

                string json = await Decompiladores.CogerHtml("https://www.steamgriddb.com/api/v2/games/steam/" + id, datos.Values["OpcionesSteamGridDBUsuario"].ToString());

                if (json != null)
                {
                    SteamGridJuego juego = JsonConvert.DeserializeObject<SteamGridJuego>(json);

                    string jsonLibrary = await Decompiladores.CogerHtml("https://www.steamgriddb.com/api/v2/grids/game/" + juego.Datos.ID + "?dimensions=600x900&types=static,animated", datos.Values["OpcionesSteamGridDBUsuario"].ToString());

                    if (jsonLibrary != null)
                    {
                        SteamGridImagenes juegos = JsonConvert.DeserializeObject<SteamGridImagenes>(jsonLibrary);

                        Objetos.gvImagenesLibrary.Items.Clear();
                        Objetos.tbImagenesLibraryCantidad.Text = juegos.Grids.Count.ToString();

                        foreach (SteamGridImagen imagen in juegos.Grids)
                        {
                            Objetos.gvImagenesLibrary.Items.Add(BotonEstilo(id, imagen.Enlace, imagen.Previo, "p"));
                        }
                    }

                    string jsonHeroes = await Decompiladores.CogerHtml("https://www.steamgriddb.com/api/v2/heroes/game/" + juego.Datos.ID + "?dimensions=1920x620,3840x1240&types=static,animated", datos.Values["OpcionesSteamGridDBUsuario"].ToString());

                    if (jsonHeroes != null)
                    {
                        SteamGridImagenes juegos = JsonConvert.DeserializeObject<SteamGridImagenes>(jsonHeroes);

                        Objetos.gvImagenesHeroes.Items.Clear();
                        Objetos.tbImagenesHeroesCantidad.Text = juegos.Grids.Count.ToString();

                        foreach (SteamGridImagen imagen in juegos.Grids)
                        {
                            Objetos.gvImagenesHeroes.Items.Add(BotonEstilo(id, imagen.Enlace, imagen.Previo, "_hero"));
                        }
                    }

                    string jsonLogos = await Decompiladores.CogerHtml("https://www.steamgriddb.com/api/v2/logos/game/" + juego.Datos.ID + "?types=static,animated", datos.Values["OpcionesSteamGridDBUsuario"].ToString());

                    if (jsonLogos != null)
                    {
                        SteamGridImagenes juegos = JsonConvert.DeserializeObject<SteamGridImagenes>(jsonLogos);

                        Objetos.gvImagenesLogos.Items.Clear();
                        Objetos.tbImagenesLogosCantidad.Text = juegos.Grids.Count.ToString();

                        foreach (SteamGridImagen imagen in juegos.Grids)
                        {
                            Objetos.gvImagenesLogos.Items.Add(BotonEstilo(id, imagen.Enlace, imagen.Previo, "_logo"));
                        }
                    }
                }

                //------------------------------------------------------

                string carpetaSteam = Steam.GenerarRutaImagenes();
                StorageFolder carpetaSteam2 = await StorageFolder.GetFolderFromPathAsync(carpetaSteam);

                IReadOnlyList<StorageFile> listaFicheros = await carpetaSteam2.GetFilesAsync();
                bool existe = false;

                foreach (StorageFile fichero in listaFicheros)
                {
                    if (fichero.Name.Contains(id) == true)
                    {
                        existe = true;
                    }
                }

                if (existe == true)
                {
                    Objetos.botonCambiarImagenesLimpiar.Visibility = Visibility.Visible;
                }
                else
                {
                    Objetos.botonCambiarImagenesLimpiar.Visibility = Visibility.Collapsed;
                }

                Objetos.botonCambiarImagenesLimpiar.Tag = id;
                Objetos.botonCambiarImagenesLimpiar.Click += LimpiarImagenesClick;
                Objetos.botonCambiarImagenesLimpiar.PointerEntered += Animaciones.EntraRatonBoton2;
                Objetos.botonCambiarImagenesLimpiar.PointerExited += Animaciones.SaleRatonBoton2;

                //------------------------------------------------------

                Objetos.gridCambiarImagenesCargando.Visibility = Visibility.Collapsed;
                Objetos.gridCambiarImagenesCargado.Visibility = Visibility.Visible;
            }           
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
            foreach (object boton in Objetos.spCambiarImagenesBotones.Children)
            {
                if (boton.GetType() == typeof(Button2))
                {
                    Button2 boton2 = boton as Button2;

                    if (i == botonPulsado)
                    {
                        boton2.Background = colorPulsado;
                    }
                    else
                    {
                        boton2.Background = new SolidColorBrush(Colors.Transparent);
                    }

                    i += 1;
                }
            }

            foreach (object sp in Objetos.spCambiarImagenesPestañas.Children)
            {
                if (sp.GetType() == typeof(StackPanel))
                {
                    StackPanel sp2 = sp as StackPanel;
                    sp2.Visibility = Visibility.Collapsed;
                }                   
            }

            StackPanel spMostrar = Objetos.spCambiarImagenesPestañas.Children[botonPulsado] as StackPanel;
            spMostrar.Visibility = Visibility.Visible;
        }

        private static GridViewItem BotonEstilo(string id, string enlace, string previo, string tipo)
        {
            ImagenJuego imagenyenlace = new ImagenJuego(id, enlace, tipo);

            Button2 boton = new Button2
            {
                Margin = new Thickness(0),
                Padding = new Thickness(2),
                Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Tag = imagenyenlace,
                MaxWidth = 600,
                MaxHeight = 900
            };

            if (enlace.Contains(".png") == true || enlace.Contains(".jpg") == true)
            {
                Image imagen = new Image
                {
                    Source = new BitmapImage(new Uri(previo))
                };

                boton.Content = imagen;
            }
            else if (enlace.Contains(".webp") == true)
            {
                WebView2 imagen = new WebView2
                {
                    Source = new Uri(enlace),
                    IsHitTestVisible = false                    
                };

                if (tipo == "p")
                {
                    imagen.MinHeight = 262;
                }
                else if (tipo == "_hero")
                {
                    imagen.MinHeight = 120;
                }
                else
                {
                    imagen.MinHeight = 150;
                }

                boton.Content = imagen;
            }

            boton.Click += ImagenJuegoClick;
            boton.PointerEntered += Animaciones.EntraRatonBoton2;
            boton.PointerExited += Animaciones.SaleRatonBoton2;

            GridViewItem item = new GridViewItem
            {
                Content = boton,
                Margin = new Thickness(5, 0, 5, 10),
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };

            return item;
        }

        private static async void ImagenJuegoClick(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            List<GridViewItem> imagenes = new List<GridViewItem>();
            
            foreach (GridViewItem item in Objetos.gvImagenesLibrary.Items)
            {
                imagenes.Add(item);
            }

            foreach (GridViewItem item in Objetos.gvImagenesHeroes.Items)
            {
                imagenes.Add(item);
            }

            foreach (GridViewItem item in Objetos.gvImagenesLogos.Items)
            {
                imagenes.Add(item);
            }

            foreach (GridViewItem item in imagenes)
            {
                item.IsEnabled = false;
            }

            Button2 boton = (Button2)sender;
            ImagenJuego imagenyenlace = (ImagenJuego)boton.Tag;
            string enlace = imagenyenlace.enlace;

            string carpetaSteam = Steam.GenerarRutaImagenes();
            carpetaSteam = carpetaSteam + "\\" + imagenyenlace.id + imagenyenlace.tipo + ".png";

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:15.0) Gecko/20100101 Firefox/15.0.1");

            try
            {
                byte[] ficheroBytes = await cliente.GetByteArrayAsync(enlace);
                File.WriteAllBytes(carpetaSteam, ficheroBytes);
            }
            catch { }

            foreach (GridViewItem item in imagenes)
            {
                item.IsEnabled = true;
            }

            Objetos.botonCambiarImagenesLimpiar.Visibility = Visibility.Visible;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            if ((bool)datos.Values["OpcionesSteamCarpeta"] == true)
            {
                await Launcher.LaunchFolderPathAsync(Steam.GenerarRutaImagenes());
            }

            Objetos.ttipCambiarImagenes.Title = recursos.GetString("ChangingImage1");
            Objetos.ttipCambiarImagenes.Subtitle = recursos.GetString("ChangingImage2");
            Objetos.ttipCambiarImagenes.IsOpen = true;
        }

        private static async void LimpiarImagenesClick(object sender, RoutedEventArgs e)
        {
            Button2 boton = sender as Button2;
            boton.IsEnabled = false;
            string id = boton.Tag.ToString();

            string carpetaSteam = Steam.GenerarRutaImagenes();
            StorageFolder carpetaSteam2 = await StorageFolder.GetFolderFromPathAsync(carpetaSteam);

            IReadOnlyList<StorageFile> listaFicheros = await carpetaSteam2.GetFilesAsync();

            foreach (StorageFile fichero in listaFicheros)
            {
                if (fichero.Name.Contains(id) == true)
                {
                    try
                    {
                        await fichero.DeleteAsync();
                    }
                    catch { }                  
                }
            }

            boton.IsEnabled = true;
            boton.Visibility = Visibility.Collapsed;
        }
    }

    public class ImagenJuego
    {
        public string id { get; set; }
        public string enlace { get; set; }
        public string tipo { get; set; }

        public ImagenJuego(string Id, string Enlace, string Tipo)
        {
            id = Id;
            enlace = Enlace;
            tipo = Tipo;
        }
    }
}
