using Microsoft.Win32;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Massaohjelma.xaml
    /// </summary>
    public partial class Massaohjelma : Window
    {
        List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        List<Osoitteet> _osoitteet = new List<Osoitteet>();
        MainWindow _main;
        public Massaohjelma(Window main)
        {
            InitializeComponent();
            _main = (MainWindow)main; //Otetaan aloitusikkuna talteen
            SeulaArvotOhjeArvoihin(); //Laitetaan valitut seula-arvot Ohjealue-ruudun seulakenttiin
            SeulaJsonLataus(); //Ladataan Seulat.json tiedostosta kaikki seula-arvot talteen seulalistaan
            OsoiteJsonLataus(); //Ladataan Osoitetiedot.json tiedostosta kaikki osoitetiedot listaan talteen
            GetOsoitteetToTextBoxes();
            foreach (Seulakirjasto s in seulalista)
            {
                Console.WriteLine(s.seula);
            }
            
            foreach (Seulakirjasto s in seulalista)
            {
                Seula1.Items.Add(s.seula);
                Seula2.Items.Add(s.seula);
                Seula3.Items.Add(s.seula);
                Seula4.Items.Add(s.seula);
                Seula5.Items.Add(s.seula);
                Seula6.Items.Add(s.seula);
                Seula7.Items.Add(s.seula);
                Seula8.Items.Add(s.seula);
                Seula9.Items.Add(s.seula);
                Seula10.Items.Add(s.seula);
                Seula11.Items.Add(s.seula);
                Seula12.Items.Add(s.seula);
                Seula13.Items.Add(s.seula);
                Seula14.Items.Add(s.seula);
                Seula15.Items.Add(s.seula);
                Seula16.Items.Add(s.seula);
            }
            Seula1.SelectedIndex = 0;
            Seula2.SelectedIndex = 1;
            Seula3.SelectedIndex = 4;
            Seula4.SelectedIndex = 6;
            Seula5.SelectedIndex = 7;
            Seula6.SelectedIndex = 9;
            Seula7.SelectedIndex = 11;
            Seula8.SelectedIndex = 12;
            Seula9.SelectedIndex = 14;
            Seula10.SelectedIndex = 16;
            Seula11.SelectedIndex = 19;
            Seula12.SelectedIndex = 21;
            Seula13.SelectedIndex = 25;
            Seula14.SelectedIndex = 28;
            Seula15.SelectedIndex = 29;
            Seula16.SelectedIndex = 30;
        }
        private void EmptyFields_Click(object sender, RoutedEventArgs e)
        {
            EmptyFields();
            //EmptyResultFields();
        }
        private void EmptyFields() //Funktio mikä tyhjentää kaikki tekstikentät seulalaskenta-ruudusta
        {
            foreach (Control c in seulaArvot.Children)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
        }
        private void Massaohjelma_Closed(object sender, EventArgs e)
        {
            _main.SuljeIkkuna("massa");
        }

        private void LaskeSeula_Click(object sender, RoutedEventArgs e)
        {
            EmptyResultFields();//tyhjentää tuloskentät
            int pyoristys = Convert.ToInt32(dbDesimaali.Text);//Otetaan valittu pyöristysarvo ohjelmasta
            List<SyotetytArvot> syotetytarvot = new List<SyotetytArvot>(); //Luo listan johon syötetyt arvot tallennetaan
            syotetytarvot = LuetaanSyotetytArvot(); //Luetaan syotetyt arvot luotuun listaan
            double kokomassa = LaskeKokonaisMassa(syotetytarvot, pyoristys); //lasketaan arvojen kokonaismäärä
            TulostenLasku_SeulalleJai(syotetytarvot, kokomassa, pyoristys); //Lasketaan laskutoimituksia ja syötetään tulokset tuloskenttiin
            TulostenLasku_LapaisyProsentti(syotetytarvot, kokomassa, pyoristys);//--------||----------
            Laskut l = new Laskut();
        }

        private void TulostenLasku_LapaisyProsentti(List<SyotetytArvot> sa, double kokomassa, int pyoristys)
        {
            //Luodaan lista jonne tulokset laitetaan
            List<SyotetytArvot> tulos = new List<SyotetytArvot>();
            Laskut laskut = new Laskut();
            //Lasketaan ja syötetään tulokset tulos-listaan
            tulos = laskut.LapaisyProsentti(sa, kokomassa, 0, 1);
            //Käydään lista läpi ja tulostetaan tulokset oikeille kentille
            foreach (SyotetytArvot s in tulos)
            {
                switch (s.index)
                {
                    case 1:
                        lapaisypros1.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 2:
                        lapaisypros2.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 3:
                        lapaisypros3.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 4:
                        lapaisypros4.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 5:
                        lapaisypros5.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 6:
                        lapaisypros6.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 7:
                        lapaisypros7.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 8:
                        lapaisypros8.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 9:
                        lapaisypros9.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 10:
                        lapaisypros10.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 11:
                        lapaisypros11.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 12:
                        lapaisypros12.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 13:
                        lapaisypros13.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 14:
                        lapaisypros14.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 15:
                        lapaisypros15.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 16:
                        lapaisypros16.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    default:
                        {
                            Console.WriteLine("VIRHE Kiviohjelma.xaml.cs tiedostossa: lapaisypros-kenttiä täydentäessä tuli vastaan outo index luku SyotetytArvot-listasta 'tulos'");
                            break;
                        }
                }
            }

        }
        private void TulostenLasku_SeulalleJai(List<SyotetytArvot> sa, double kokomassa, int pyoristys)
        {
            for (int l = 0; l < sa.Count; l++)
            {
                //otetaan ja lasketaan yksi tulos
                Laskut laskut = new Laskut();
                double tulos = Math.Round(laskut.seulalleJai(Convert.ToDouble(sa[l].syote), kokomassa), pyoristys);

                //Valitaan oikea paikka tulokselle sen indeksin perusteella
                switch (sa[l].index)
                {
                    case 1:
                        seulapros1.Text = tulos.ToString();
                        break;
                    case 2:
                        seulapros2.Text = tulos.ToString();
                        break;
                    case 3:
                        seulapros3.Text = tulos.ToString();
                        break;
                    case 4:
                        seulapros4.Text = tulos.ToString();
                        break;
                    case 5:
                        seulapros5.Text = tulos.ToString();
                        break;
                    case 6:
                        seulapros6.Text = tulos.ToString();
                        break;
                    case 7:
                        seulapros7.Text = tulos.ToString();
                        break;
                    case 8:
                        seulapros8.Text = tulos.ToString();
                        break;
                    case 9:
                        seulapros9.Text = tulos.ToString();
                        break;
                    case 10:
                        seulapros10.Text = tulos.ToString();
                        break;
                    case 11:
                        seulapros11.Text = tulos.ToString();
                        break;
                    case 12:
                        seulapros12.Text = tulos.ToString();
                        break;
                    case 13:
                        seulapros13.Text = tulos.ToString();
                        break;
                    case 14:
                        seulapros14.Text = tulos.ToString();
                        break;
                    case 15:
                        seulapros15.Text = tulos.ToString();
                        break;
                    case 16:
                        seulapros16.Text = tulos.ToString();
                        break;
                    case 17:
                        seulapros17.Text = tulos.ToString();
                        break;
                    default:
                        {
                            Console.WriteLine("VIRHE Kiviohjelma.xaml.cs tiedostossa: seulapros-kenttiä täydentäessä tuli vastaan outo index luku SyotetytArvot-listasta");
                            break;
                        }
                }

            }
        }

        private double LaskeKokonaisMassa(List<SyotetytArvot> sa, int pyoristys)
        {
            //laskee syotettyjen arvojen kokonaismassan ja palauttaa sen
            //Listassa on arvot jotka lasketaan yhteen, ja pyoristys-parametri kertoo kuinka tarkasti arvo pyöristetään
            double kokomassa = 0;
            foreach (SyotetytArvot se in sa)
            {
                kokomassa += Convert.ToDouble(se.syote);
            }
            punnittuYhteensa.Text = Math.Round(kokomassa, pyoristys).ToString();
            return Math.Round(kokomassa, pyoristys);
        }


        private List<SyotetytArvot> LuetaanSyotetytArvot()
        {
            //Lukee käyttäjän syöttämät arvot, lisää ne listaan ja palauttaa listan
            //Syötetyissä arvoissa saattaa olla välejä (kaikkeja rivejä ei täytetty) joten koodi tarkistaa sen myös
            List<SyotetytArvot> sa = new List<SyotetytArvot>();
            double g;
            try
            {
                if (double.TryParse(seulaG0.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG0.Text);
                    s.syote = g;
                    s.index = 1;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG1.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG1.Text);
                    s.syote = g;
                    s.index = 2;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG2.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG2.Text);
                    s.syote = g;
                    s.index = 3;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG3.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG3.Text);
                    s.syote = g;
                    s.index = 4;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG4.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG4.Text);
                    s.syote = g;
                    s.index = 5;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG5.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG5.Text);
                    s.syote = g;
                    s.index = 6;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG6.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG6.Text);
                    s.syote = g;
                    s.index = 7;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG7.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG7.Text);
                    s.syote = g;
                    s.index = 8;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG8.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG8.Text);
                    s.syote = g;
                    s.index = 9;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG9.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG9.Text);
                    s.syote = g;
                    s.index = 10;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG10.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG10.Text);
                    s.syote = g;
                    s.index = 11;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG11.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG11.Text);
                    s.syote = g;
                    s.index = 12;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG12.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG12.Text);
                    s.syote = g;
                    s.index = 13;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG13.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG13.Text);
                    s.syote = g;
                    s.index = 14;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG14.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG14.Text);
                    s.syote = g;
                    s.index = 15;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG15.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG15.Text);
                    s.syote = g;
                    s.index = 16;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG16.Text, out g))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g = Convert.ToDouble(seulaG16.Text);
                    s.syote = g;
                    s.index = 17;
                    sa.Add(s);
                }
            }
            catch
            {

            }
            return sa;
        }



        private void SeulaArvotOhjeArvoihin()
        {
            //Kopioi valitut seula-arvot Ohjealue-ruudun seulakenttiin
            seulaValue1.Text = Seula1.Text.Trim();
            seulaValue2.Text = Seula2.Text.Trim();
            seulaValue3.Text = Seula3.Text.Trim();
            seulaValue4.Text = Seula4.Text.Trim();
            seulaValue5.Text = Seula5.Text.Trim();
            seulaValue6.Text = Seula6.Text.Trim();
            seulaValue7.Text = Seula7.Text.Trim();
            seulaValue8.Text = Seula8.Text.Trim();
            seulaValue9.Text = Seula9.Text.Trim();
            seulaValue10.Text = Seula10.Text.Trim();
            seulaValue11.Text = Seula11.Text.Trim();
            seulaValue12.Text = Seula12.Text.Trim();
            seulaValue13.Text = Seula13.Text.Trim();
            seulaValue14.Text = Seula14.Text.Trim();
            seulaValue15.Text = Seula15.Text.Trim();
            seulaValue16.Text = Seula16.Text.Trim();
        }

        private void EmptyResultFields() //Tyhjentää tulosruuduissa olevat arvot
        {

            seulapros1.Text = String.Empty;
            seulapros2.Text = String.Empty;
            seulapros3.Text = String.Empty;
            seulapros4.Text = String.Empty;
            seulapros5.Text = String.Empty;
            seulapros6.Text = String.Empty;
            seulapros7.Text = String.Empty;
            seulapros8.Text = String.Empty;
            seulapros9.Text = String.Empty;
            seulapros10.Text = String.Empty;
            seulapros11.Text = String.Empty;
            seulapros12.Text = String.Empty;
            seulapros13.Text = String.Empty;
            seulapros14.Text = String.Empty;
            seulapros15.Text = String.Empty;
            seulapros16.Text = String.Empty;
            seulapros17.Text = String.Empty;

            lapaisypros1.Text = String.Empty;
            lapaisypros2.Text = String.Empty;
            lapaisypros3.Text = String.Empty;
            lapaisypros4.Text = String.Empty;
            lapaisypros5.Text = String.Empty;
            lapaisypros6.Text = String.Empty;
            lapaisypros7.Text = String.Empty;
            lapaisypros8.Text = String.Empty;
            lapaisypros9.Text = String.Empty;
            lapaisypros10.Text = String.Empty;
            lapaisypros11.Text = String.Empty;
            lapaisypros12.Text = String.Empty;
            lapaisypros13.Text = String.Empty;
            lapaisypros14.Text = String.Empty;
            lapaisypros15.Text = String.Empty;
            lapaisypros16.Text = String.Empty;

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Kun syötetään tietoja seulakenttiin, tämä funktio varmistaa että vain numeroita ja pilkkuja voi laittaa kenttiin
            Regex regex = new Regex("^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void SeulaJsonLataus()
        {

            //Tarkistetaan onko Seulat.json tiedostoa ja sen isäntäkansiota olemassa
            //Jos on, luetaan seulatiedot tiedostosta listaan
            //Jos ei, luodaan tiedosto ja kansio tarpeen mukaan ja luetaan seulat sitten.
            string seulajson;
            if (File.Exists(@".\Asetukset\Seulat.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\Seulat.json");
                    seulajson = s.ReadToEnd();
                    s.Close();
                    seulalista = JsonConvert.DeserializeObject<List<Seulakirjasto>>(seulajson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhe Kiviohjelma.xaml.cs tiedostossa: Seulat.json tiedoston haussa virhe.  " + e.Message + ",   " + e.StackTrace);
                }
            }
            else if ((!Directory.Exists(@".\Asetukset")) || (Directory.Exists(@".\Asetukset") && !File.Exists(@".\Asetukset\Seulat.json")))
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                }
                SeulaArvoJSONLuonti.SeulaJSONLuonti();
                SeulaJsonLataus();
            }
        }

        private void OsoiteJsonLataus()
        {
            //Tarkistetaan onko Osoitetiedot.json tiedostoa ja sen isäntäkansiota olemassa
            //Jos on, luetaan seulatiedot tiedostosta listaan
            //Jos ei, luodaan tiedosto ja kansio tarpeen mukaan ja luetaan seulat sitten.

            string osoitejson;
            if (File.Exists(@".\Asetukset\Osoitetiedot.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\Osoitetiedot.json");
                    osoitejson = s.ReadToEnd();
                    s.Close();
                    _osoitteet = JsonConvert.DeserializeObject<List<Osoitteet>>(osoitejson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhe Kiviohjelma.xaml.cs tiedostossa: Seulat.json tiedoston haussa virhe.  " + e.Message + ",  " + e.StackTrace);
                }
            }
            else if ((!Directory.Exists(@".\Asetukset")) || (Directory.Exists(@".\Asetukset") && !File.Exists(@".\Asetukset\Osoitetiedot.json")))
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                }
                OsoiteTiedotJSON.OsoiteJSONLuonti();
                OsoiteJsonLataus();
            }


        }

        private void GetOsoitteetToTextBoxes()
        {
            for (int i = 0; i < _osoitteet.Count; i++)
            {
                alempiOtsikko.Text = _osoitteet[0].osoiteTieto;
                lahiOsoite.Text = _osoitteet[1].osoiteTieto;
                osoite.Text = _osoitteet[2].osoiteTieto;
                puh.Text = _osoitteet[3].osoiteTieto;
            }
        }

        private void tallennaOsoitetiedot_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _osoitteet.Count; i++)
            {
                _osoitteet[0].osoiteTieto = alempiOtsikko.Text;
                _osoitteet[1].osoiteTieto = lahiOsoite.Text;
                _osoitteet[2].osoiteTieto = osoite.Text;
                _osoitteet[3].osoiteTieto = puh.Text;
            }

            string json = JsonConvert.SerializeObject(_osoitteet);
            try
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                    File.WriteAllText(@".\Asetukset\Osoitetiedot.json", json);
                }
                else
                {
                    File.WriteAllText(@".\Asetukset\Osoitetiedot.json", json);
                }
                MessageBox.Show("Tiedot tallennettu");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tietojen tallennus epäonnistui" + ex.Message);
            }
        }


        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {
            Open();
        }

        private void Open() //Avaa File Explorerin jonka avulla voidaan ladata tiedostoja
        {
            Stream stream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        using (stream)
                        {

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tiedoston avaaminen epäonnistui" + ex.Message);
                }
            }
        }


        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            Save();
        }

        private void SaveFiles_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            //Avataan tallennusdialogi ja tallennetaan tiedosto haluamaan sijaintiin
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fs = (FileStream)saveFileDialog.OpenFile();

                fs.Close();
            }
        }
        private void CommandBinding_Print(object sender, ExecutedRoutedEventArgs e)
        {
            PrintFile();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintFile();
        }

        private void PrintFile()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
        }
        private void btnLaskeSideainepitoisuus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTyhjennaOhjeAlue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            //Sulkee ikkunan
            this.Close();
        }
        private void CreatePDF_Click(object sender, RoutedEventArgs e)
        {
            MassaohjelmaPDF pdf = new MassaohjelmaPDF();
            pdf.SavePDF(this);
        }

        //Funktiot mitkä mahdollistavat liikkumaan TextBoxeissa Enter-napin painalluksella
        //-------------------------------------------------------------------
        private void seulaArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void ohjeAlue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void sideaineArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void tietoArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }
        //--------------------------------------------------------------------------------

    }
}
