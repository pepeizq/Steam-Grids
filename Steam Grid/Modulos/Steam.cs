using CommunityToolkit.WinUI.UI.Controls;
using Interfaz;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Collections.Generic;
using System;
using Windows.Storage;
using Windows.UI;
using static Steam_Grid.MainWindow;

namespace Modulos
{
    public static class Steam
    {
        public static string dominioImagenes = "https://cdn.cloudflare.steamstatic.com";

        public static void CargarRuta()
        {
            RegistryKey registro = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");

            if (registro.GetValue("SteamPath") != null)
            {
                string carpetaSteam = registro.GetValue("SteamPath").ToString();
                carpetaSteam = carpetaSteam.Replace("/", "\\");

                ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
                datos.Values["OpcionesSteamInstalacion"] = carpetaSteam;

                Objetos.tbOpcionesSteamRutaCarpeta.Text = carpetaSteam;
            }
        }

        public static void CargarUltimoUsuario()
        {
            RegistryKey registroUsuario = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam\\ActiveProcess");
            string usuario = registroUsuario.GetValue("ActiveUser", RegistryValueKind.DWord).ToString();

            if (usuario == "0")
            {
                RegistryKey registroUsuario2 = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam\\Users");

                foreach (string id in registroUsuario2.GetSubKeyNames())
                {
                    if (id.Length > 2)
                    {
                        usuario = id;
                        break;
                    }
                }
            }

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesSteamUsuario"] = usuario;

            Objetos.tbOpcionesSteamUsuario.Text = usuario;
        }

        public static string GenerarRutaImagenes()
        {
            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
           
            if (datos.Values["OpcionesSteamInstalacion"] != null) 
            {
                string carpetaSteam = datos.Values["OpcionesSteamInstalacion"].ToString();

                if (datos.Values["OpcionesSteamUsuario"] != null)
                {
                    return carpetaSteam + "\\userdata\\" + datos.Values["OpcionesSteamUsuario"].ToString() + "\\config\\grid";
                }
            }
            
            return null;
        }

        public async static void CargarJuegosInstalados()
        {
            Objetos.prJuegos.Visibility = Visibility.Visible;
            Objetos.gvJuegos.Visibility = Visibility.Collapsed;

            string contenidoLibreria = string.Empty;

            try
            {
                ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
                string carpetaSteam = datos.Values["OpcionesSteamInstalacion"].ToString();

                string ficheroLibreria = carpetaSteam + "\\steamapps\\libraryfolders.vdf";
                StorageFile ficheroLibreria2 = await StorageFile.GetFileFromPathAsync(ficheroLibreria);

                contenidoLibreria = await FileIO.ReadTextAsync(ficheroLibreria2);
            }
            catch { }

            if (contenidoLibreria != string.Empty)
            {
                if (contenidoLibreria.Trim().Length > 0)
                {
                    List<string> listaCarpetas = new List<string>();

                    int i = 0;
                    while (i < 100)
                    {
                        if (contenidoLibreria.Contains(Strings.ChrW(34) + "path" + Strings.ChrW(34)) == true)
                        {
                            int int1 = contenidoLibreria.IndexOf(Strings.ChrW(34) + "path" + Strings.ChrW(34));
                            contenidoLibreria = contenidoLibreria.Remove(0, int1 + 6);

                            int int2 = contenidoLibreria.IndexOf(Strings.ChrW(34));
                            contenidoLibreria = contenidoLibreria.Remove(0, int2 + 1);

                            int int3 = contenidoLibreria.IndexOf(Strings.ChrW(34));
                            string temp1 = contenidoLibreria.Remove(int3, contenidoLibreria.Length - int3);

                            listaCarpetas.Add(temp1);
                        }
                        else
                        {
                            break;
                        }

                        i += 1;
                    }

                    if (listaCarpetas.Count > 0)
                    {
                        List<SteamJuego> listaJuegos = new List<SteamJuego>();

                        foreach (string carpetaRuta in listaCarpetas)
                        {
                            string temp1 = carpetaRuta.Replace("\\\\", "\\");

                            StorageFolder carpeta = await StorageFolder.GetFolderFromPathAsync(temp1 + "\\steamapps");

                            IReadOnlyList<StorageFile> ficheros = await carpeta.GetFilesAsync();

                            foreach (StorageFile fichero in ficheros)
                            {
                                if (fichero.FileType.Contains(".acf") == true)
                                {
                                    string contenidoFichero = await FileIO.ReadTextAsync(fichero);

                                    int int1 = contenidoFichero.IndexOf(Strings.ChrW(34) + "name" + Strings.ChrW(34));
                                    contenidoFichero = contenidoFichero.Remove(0, int1 + 6);

                                    int int2 = contenidoFichero.IndexOf(Strings.ChrW(34));
                                    contenidoFichero = contenidoFichero.Remove(0, int2 + 1);

                                    int int3 = contenidoFichero.IndexOf(Strings.ChrW(34));
                                    string nombre = contenidoFichero.Remove(int3, contenidoFichero.Length - int3);

                                    string temp2 = fichero.Name;
                                    temp2 = temp2.Replace("appmanifest_", null);
                                    temp2 = temp2.Replace(".acf", null);
                                    string id = temp2.Trim();

                                    bool añadir = true;

                                    if (id == "228980")
                                    {
                                        añadir = false;
                                    }

                                    if (añadir == true)
                                    {
                                        listaJuegos.Add(new SteamJuego(id, nombre));
                                    }
                                }
                            }
                        }

                        if (listaJuegos.Count > 0)
                        {
                            Objetos.gvJuegos.Items.Clear();

                            listaJuegos.Sort(delegate (SteamJuego c1, SteamJuego c2) { return c1.nombre.CompareTo(c2.nombre); });

                            foreach (SteamJuego juego in listaJuegos)
                            {
                                ImageEx imagen = new ImageEx
                                {
                                    IsCacheEnabled = true,
                                    EnableLazyLoading = true,
                                    Stretch = Stretch.UniformToFill,
                                    Source = dominioImagenes + "/steam/apps/" + juego.id + "/library_600x900.jpg",
                                    CornerRadius = new CornerRadius(2)
                                };

                                imagen.ImageExFailed += ImagenJuegoFalla;

                                Button2 botonJuego = new Button2
                                {
                                    Content = imagen,
                                    Margin = new Thickness(0),
                                    Padding = new Thickness(2),
                                    Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                                    BorderThickness = new Thickness(0),
                                    Tag = juego,
                                    MaxWidth = 300,
                                    CornerRadius = new CornerRadius(5)
                                };

                                botonJuego.Click += ImagenJuegoClick;
                                botonJuego.PointerEntered += Animaciones.EntraRatonBoton2;
                                botonJuego.PointerExited += Animaciones.SaleRatonBoton2;

                                GridViewItem item = new GridViewItem
                                {
                                    Content = botonJuego,
                                    Margin = new Thickness(5, 0, 5, 10)
                                };

                                Objetos.gvJuegos.Items.Add(item);
                            }
                        }
                    }
                }
            }

            Objetos.prJuegos.Visibility = Visibility.Collapsed;
            Objetos.gvJuegos.Visibility = Visibility.Visible;
        }

        private static void ImagenJuegoFalla(object sender, ImageExFailedEventArgs e)
        {
            ImageEx imagen = sender as ImageEx;
            string enlace = imagen.Source.ToString();

            if (enlace.Contains("/library_600x900.jpg") == true)
            {
                enlace = enlace.Replace("/library_600x900.jpg", "/header.jpg");
                imagen.Source = enlace;
                imagen.Stretch = Stretch.Uniform;
            }
        }

        private static void ImagenJuegoClick(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            SteamJuego juego = boton.Tag as SteamJuego;

            CambiarImagenes.Cargar(juego.id, juego.nombre);
        }
    }

    public class SteamJuego
    {
        public string id { get; set; }
        public string nombre { get; set; }

        public SteamJuego(string Id, string Nombre)
        {
            id = Id;
            nombre = Nombre;
        }
    }
}
