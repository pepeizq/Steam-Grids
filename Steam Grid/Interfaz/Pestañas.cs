using FontAwesome6.Fonts;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.UI;
using static Steam_Grid.MainWindow;

namespace Interfaz
{
    public static class Pestañas
    {
        public static void Cargar()
        {
            ResourceLoader recursos = new ResourceLoader();

            Objetos.nvPrincipal.MenuItems.RemoveAt(0);
            Objetos.nvPrincipal.MenuItems.Insert(0, Objetos.nvItemMenu);

            Objetos.nvItemMenu.PointerEntered += EntraRatonNvItemMenu;
            Objetos.nvItemMenu.PointerEntered += Animaciones.EntraRatonNvItem2;
            Objetos.nvItemMenu.PointerExited += Animaciones.SaleRatonNvItem2;

            TextBlock tbOpcionesTt = new TextBlock
            {
                Text = recursos.GetString("Options")
            };

            ToolTipService.SetToolTip(Objetos.nvItemOpciones, tbOpcionesTt);
            ToolTipService.SetPlacement(Objetos.nvItemOpciones, PlacementMode.Bottom);

            Objetos.nvItemOpciones.PointerEntered += Animaciones.EntraRatonNvItem2;
            Objetos.nvItemOpciones.PointerExited += Animaciones.SaleRatonNvItem2;
        }

        public static void Visibilidad(Grid grid, bool nv, StackPanel sp, bool mostrarNombre)
        {
            foreach (var item in Objetos.nvPrincipal.MenuItems)
            {
                if (item.GetType() == typeof(StackPanel2))
                {
                    StackPanel2 spItem = item as StackPanel2;

                    if (spItem.Children.Count > 0)
                    {
                        if (spItem.Children[1] != null)
                        {
                            if (spItem.Children[1].GetType() == typeof(TextBlock))
                            {
                                TextBlock tb = spItem.Children[1] as TextBlock;
                                tb.Visibility = Visibility.Collapsed;
                            }
                        }
                    }                   
                }
            }

            Objetos.nvItemSubirArriba.Visibility = Visibility.Collapsed;

            Objetos.nvPrincipal.Visibility = Visibility.Collapsed;
            Objetos.gridJuegos.Visibility = Visibility.Collapsed;
            Objetos.gridCambiarImagenes.Visibility = Visibility.Collapsed;
            Objetos.gridOpciones.Visibility = Visibility.Collapsed;

            grid.Visibility = Visibility.Visible;

            if (nv == true)
            {
                Objetos.nvPrincipal.Visibility = Visibility.Visible;
            }
            else
            {
                Objetos.nvPrincipal.Visibility = Visibility.Collapsed;
            }

            if (mostrarNombre == true) 
            {
                if (sp.Children[1] != null)
                {
                    if (sp.Children[1].GetType() == typeof(TextBlock))
                    {
                        TextBlock tb = sp.Children[1] as TextBlock;
                        tb.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public static void CreadorItems(FontAwesome6.EFontAwesomeIcon icono, string nombre)
        {
            StackPanel2 sp = new StackPanel2
            {
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(5),
                Orientation = Orientation.Horizontal,
                Height = 30
            };

            sp.PointerEntered += Animaciones.EntraRatonStackPanel2;
            sp.PointerExited += Animaciones.SaleRatonStackPanel2;

            FontAwesome imagen = new FontAwesome
            {
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                Icon = icono
            };

            sp.Children.Add(imagen);

            TextBlock tb = new TextBlock
            {
                Text = nombre,
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                Margin = new Thickness(15, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Visibility = Visibility.Collapsed
            };

            sp.Children.Add(tb);

            if (nombre != null)
            {
                TextBlock tbTt = new TextBlock
                {
                    Text = nombre
                };

                ToolTipService.SetToolTip(sp, tbTt);
                ToolTipService.SetPlacement(sp, PlacementMode.Bottom);
            }
            
            Objetos.nvPrincipal.MenuItems.Insert(1, sp);
        }

        public static void EntraRatonNvItemMenu(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as NavigationViewItem);
        }
    }
}
