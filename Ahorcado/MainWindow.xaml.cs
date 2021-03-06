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
        readonly List<string> palabrasAdivinar = new List<string>() { "ACUMULACION", "PRIMITIVA", "BRUMA","CAPITALISMO", 
                                                                            "INFIERNO", "ÑU" , "CRISTALINO"};
        readonly int totalImages = 10;
        string palabraSeleccionada;
        char letraSeleccionada;
        TextBlock letra;
        int numGuiones;
        List<TextBlock> letraTextBlock;
        int imgInicial = 3;
        public MainWindow()
        {
            InitializeComponent();
            CrearBotonesLetras();
            ObtenerPalabra();
            GerenarGuiones();
        }

        private void GerenarGuiones()
        {
            letraTextBlock = new List<TextBlock>();
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
                    Style = (Style)this.Resources["letrasAdivinadas"]
            };
                letraTextBlock.Add(letra);

                border.Style = (Style)this.Resources["bordeGuiones"];

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
                Perder();
            }
            else {
                string letra = e.Key.ToString();
                if (letra.Equals("Oem3"))
                {
                    ComprobarTeclaPulsada('Ñ');
                }
                else
                {
                    try
                    {
                        ComprobarTeclaPulsada(char.Parse(letra));
                    }
                    catch
                    { 
                       
                    }
                }
                
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
       
        private void Perder()
        {
            string[] msg = new string[]
            {
                "Valla!! Esperaba algo más de ti",
                "Pasapalabra no es lo tuyo",
                "Loooser!!!",
                "Hay algien ahí"

            };

            int numMensage;
            Random r = new Random();
            numMensage = r.Next(msg.Length);

            MessageBox.Show(msg[numMensage], "Game Over", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.No);
            NuevaPartida();
        
        }

        private void NuevaPartida()
        {
            ObtenerPalabra();
            GerenarGuiones();
            imgInicial = 3;
            string uri = @"assets\hangman\3.jpg";
            imagenAhorcado.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
        }

        private void ComprobarLetra()
        {
            Char[] palabraArrayCaracteres = palabraSeleccionada.ToCharArray();
            int letrasAcertadas = 0;

            if (palabraArrayCaracteres.Contains(letraSeleccionada))
            {
                
                foreach (TextBlock textBlock in letraTextBlock)
                {

                    if (textBlock.Text == letraSeleccionada.ToString())
                    {
                        textBlock.Visibility = Visibility.Visible;
                    }
                    if (textBlock.Visibility == Visibility.Visible)
                    {
                        letrasAcertadas++;
                    }
                }
                if (letrasAcertadas == palabraSeleccionada.Length)
                {
                    PartidaGanada();
                }
            }
            else
            {
                SumarFallo();
            }
        }

        private void PartidaGanada()
        {
            string[] msg = new string[]
                {
                    "!!!Excelente!!!",
                    "You are THE BEST!!!",
                    "No has aprobado PSP pero...\n!!ENHORABUENA!! por hoy esta bien.",
                    "FELICIDADES!!!!\nPodrás reclamar tu premio a final de curso."

                };

            int numMensage;
            Random r = new Random();
            numMensage = r.Next(msg.Length);

            MessageBox.Show(msg[numMensage], "VICTORY", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.No);
        }

        private void SumarFallo()
        {

            imgInicial++;
            string uri;

            if (imgInicial < totalImages)
            {
                uri = @"assets\hangman\" + imgInicial + ".jpg";
                imagenAhorcado.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            }
            else
            {
                uri = @"assets\hangman\" + totalImages + ".jpg";
                imagenAhorcado.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
                string[] msg = new string[]
                {
                    "Valla!! Esperaba algo más de ti.",
                    "No lo intentes en Pasapalabra",
                    "Aaaarrrrrg",
                    "Hola, hay alguien ahí???"

                };

                int numMensage;
                Random r = new Random();
                numMensage = r.Next(msg.Length);

                MessageBox.Show(msg[numMensage], "Game Over", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.No);

            }
        }

        private void BotonesPartida(object sender, RoutedEventArgs e)
        {
            String botonPaartida = (sender as Button).Tag.ToString();

            if (botonPaartida.Equals("reiniciar"))
            {
                NuevaPartida();
            }
            else if (botonPaartida.Equals("rendirse"))
            {
                Perder();
            }
        }
    }
}
