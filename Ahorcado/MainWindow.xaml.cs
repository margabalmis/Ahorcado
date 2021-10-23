using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Ahorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly char[] abc = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K','L', 'M', 'N',
                                   'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U','V', 'W','X', 'Y', 'Z' };
        readonly List<string> palabrasAdivinar = new List<string>() { "INFIERNO", "ÑU"};
        string palabraSeleccionada;
        char letraSeleccionada;
        TextBlock letra;
        int numGuiones;
        List<TextBlock> letraTextBlock = new List<TextBlock>();



        public MainWindow()
        {
            InitializeComponent();
            CrearBotonesLetras();
            ObtenerPalabra();
            GerenarGuiones();
        }

        private void GerenarGuiones()
        {
            char[] letras = palabraSeleccionada.ToCharArray();
            numGuiones = palabraSeleccionada.Length;
            WrapPanel guiones = new WrapPanel();

            for ( int i = 0; i< numGuiones; i++) {

                Viewbox viewGuiones = new Viewbox();
                Border border = new Border();
                Label guion = new Label();
                letra = new TextBlock()
                {
                    MinWidth = 25,
                    Text = letras[i].ToString(),
                    Visibility = Visibility.Hidden,
                };
                letraTextBlock.Add(letra);

                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(0, 0, 0, 2);

                guion.Content = letra;
                border.Child = guion;
                viewGuiones.Child = border;
                guiones.Children.Add(viewGuiones);
                
            }
            palabraYGuiones.Content = guiones;
        }

        private void ObtenerPalabra()
        {
            Random rnd = new Random();
            int numPalabra = rnd.Next(0, palabrasAdivinar.Count);
            palabraSeleccionada = palabrasAdivinar[numPalabra];
        }

        public void CrearBotonesLetras() {


            for (int i = 0; i < abc.Length; i++)
            {
                Label letra = new Label() { Content = abc[i] };
                Viewbox view = new Viewbox();

                Button b = new Button()
                {
                    Tag = abc[i],
                    Style = (Style)this.Resources["abcButtons"],
                    Content = view
                };
               
                b.Click += ClickLetraBtt;

                view.Child = letra;

                letrasUniformGrid.Children.Add(b);
            }

        }

        private void ClickLetraBtt(object sender, RoutedEventArgs e)
        {
            letraSeleccionada = char.Parse((sender as Button).Tag.ToString());
            ComprobarLetra();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NuevaPartida();
            }
            else if (e.Key == Key.Escape)
            {
                Rendirse();
            }
            else {
                string letra = e.Key.ToString();

                ComprobarTeclaPulsada(char.Parse(letra));
            }
             
        }
   
        private void ComprobarTeclaPulsada( char tecla )
        {
            if (palabraSeleccionada.Contains(tecla))
            {
                letraSeleccionada = tecla;
            }
            ComprobarLetra();
        }
       
        private void Rendirse()
        {
            throw new NotImplementedException();
        }

        private void NuevaPartida()
        {
            throw new NotImplementedException();
        }

        private void ComprobarLetra()
        {
            Char[] palabraArrayCaracteres = palabraSeleccionada.ToCharArray();

            if (palabraArrayCaracteres.Contains(letraSeleccionada))
            {
                foreach( TextBlock textBlock in letraTextBlock )
                {
                    if (textBlock.Text == letraSeleccionada.ToString())
                    {
                        textBlock.Visibility = Visibility.Visible;
                    }
                }

            }
            else 
            {
                SumarFallo();
            }

        }

        private void SumarFallo()
        {
            throw new NotImplementedException();
        }
    }
}
