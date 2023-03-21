using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using static Steam_Grid.MainWindow;

namespace Interfaz
{
    public static class ScrollViewers
    {
        public static void Cargar()
        {
            Objetos.nvItemSubirArriba.PointerPressed += SubirArriba;
            Objetos.nvItemSubirArriba.PointerEntered += Animaciones.EntraRatonNvItem2;
            Objetos.nvItemSubirArriba.PointerExited += Animaciones.SaleRatonNvItem2;

            Objetos.svJuegos.ViewChanging += svScroll;
            Objetos.svCambiarImagenes.ViewChanging += svScroll;
            Objetos.svOpciones.ViewChanging += svScroll;
        }

        private static void svScroll(object sender, ScrollViewerViewChangingEventArgs args)
        {
            ScrollViewer sv = sender as ScrollViewer;

            if (sv.VerticalOffset > 150)
            {
                Objetos.nvItemSubirArriba.Visibility = Visibility.Visible;
            }
            else
            {
                Objetos.nvItemSubirArriba.Visibility = Visibility.Collapsed;
            }
        }

        public static void SubirArriba(object sender, RoutedEventArgs e)
        {
            NavigationViewItem nvItem = sender as NavigationViewItem;
            nvItem.Visibility = Visibility.Collapsed;

            Grid grid = nvItem.Content as Grid;
            grid.Background = new SolidColorBrush(Colors.Transparent);

            if (Objetos.gridJuegos.Visibility == Visibility.Visible)
            {
                Objetos.svJuegos.ChangeView(null, 0, null);
            }
            else if (Objetos.gridCambiarImagenes.Visibility == Visibility.Visible)
            {
                Objetos.svCambiarImagenes.ChangeView(null, 0, null);
            }
            else if (Objetos.gridOpciones.Visibility == Visibility.Visible)
            {
                Objetos.svOpciones.ChangeView(null, 0, null);
            }
        }

        public static void EnseñarSubir(ScrollViewer sv)
        {
            if (sv.VerticalOffset > 50)
            {
                Objetos.nvItemSubirArriba.Visibility = Visibility.Visible;
            }
            else
            {
                Objetos.nvItemSubirArriba.Visibility = Visibility.Collapsed;
            }
        }
    }
}
