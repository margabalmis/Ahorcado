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
        string[] abc = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K","L", "M", "N",
                                        "Ñ", "O", "P", "Q", "R", "S", "T", "U","V", "W","X", "Y", "Z" };
        List<string> palabrasAdivinar = new List<string>() { "INFIERNO", "ÑU"};
        
        public MainWindow()
        {
            InitializeComponent();
            CrearBotonesLetras();
            ObtenerPalabra();
        }

        private void ObtenerPalabra()
        {
            List<int> numPalabraUtilizada = new List<int>();

            Random rnd = new Random();
            int numPalabra = rnd.Next(0, palabrasAdivinar.Count);
            numPalabraUtilizada.Add(numPalabra);

            if () { 
            
            
            }
            

            
        }

        public void CrearBotonesLetras() {


            for (int i = 0; i < abc.Length; i++)
            {
                Button b = new Button()
                {
                    Tag = abc[i],
                    Style = (Style)this.Resources["abcButtons"]
                };
                
                Viewbox view = new Viewbox();
                Label letra = new Label();
                letra.Content = abc[i];
                view.Child = letra;
                b.Content = view;

                letrasUniformGrid.Children.Add(b);
            }

            
        
        }

    }
}
