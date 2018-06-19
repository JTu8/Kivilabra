using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SeulaMuokkausIkkuna : Window
    {
        List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        MainWindow _main;
        public SeulaMuokkausIkkuna(Window main)
        {
            InitializeComponent();
            _main = (MainWindow)main;
            seulalista = SeulaJsonLataus();
            if (seulalista != null)
            {
                foreach (Seulakirjasto s in seulalista)
                {
                    Console.WriteLine(s.seula);
                    lbSeulaLista.Items.Add(s.seula);
                }
            }
        }

        private void btnUusiSeula_Click(object sender, RoutedEventArgs e)
        {
            if(tbUusiSeula.Text != String.Empty)
            {
                string syote = tbUusiSeula.Text;
                double? uusi;
                try
                {
                    uusi = Convert.ToDouble(syote);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    uusi = null;
                }
                if (uusi != null)
                {
                    seulalista.Add(new Seulakirjasto {index = seulalista.Count, seula = Convert.ToDouble(uusi) });
                }

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _main.SuljeIkkuna("seula");
        }

        private List<Seulakirjasto> SeulaJsonLataus()
        {
            //Tarkistetaan onko Seulat.json tiedostoa ja sen isäntäkansiota olemassa
            //Jos on, luetaan seulatiedot tiedostosta
            //Jos ei, luodaan tiedosto ja kansio tarpeen mukaan ja luetaan seulat sitten.
            string seulajson;
            if (File.Exists(@".\Asetukset\Seulat.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\Seulat.json");
                    seulajson = s.ReadToEnd();
                    s.Close();
                    return JsonConvert.DeserializeObject<List<Seulakirjasto>>(seulajson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhe Kiviohjelma.xaml.cs tiedostossa: Seulat.json tiedoston haussa virhe.  " + e.Message + ",   " + e.StackTrace);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        
    }
}
