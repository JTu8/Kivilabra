using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.Diagnostics;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for Kiviohjelma.xaml
    /// </summary>
    public partial class Kiviohjelma : Window
    {
        

        MainWindow _main;
        List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        public Kiviohjelma(Window main)
        {
            InitializeComponent();
            _main = (MainWindow)main; //Otetaan aloitusikkuna talteen
            SeulaArvotOhjeArvoihin(); //Laitetaan valitut seula-arvot Ohjealue-ruudun seulakenttiin
            SeulaJsonLataus(); //Ladataan Seulat.json tiedostosta kaikki seula-arvot talteen seulalistaan
            rbKuivaseulonta.IsChecked = true;
        }
        private void Kiviohjelma_Closed(object sender, EventArgs e)
        {
            //kertoo aloitusikkunalle että ohjelma on suljettu
            //käytetään siihen että ei voi olla useampi kiviohjelma käynnissä yhtä aikaa
            _main.SuljeIkkuna("kivi");
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

        private void btnTyhjennaOhjealueet_Click(object sender, RoutedEventArgs e)
        {
            //Tyhjentää Ohjealue-ruudusta kaikki tekstikentät
            foreach (Control c in ohjeArvot.Children)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
            //Laittaa seulat takaisin Ohjealue-ruutuun
            SeulaArvotOhjeArvoihin();
        }

        KayraInitializer kayrainitializer = new KayraInitializer();
        private void btnNaytaKaavio_Click(object sender, RoutedEventArgs e) // Avataan käyräikkuna
        {
            kayrainitializer.KayraPiirto(this);
        }

        public void SuljeKayraIkkuna()
        {
            kayrainitializer.SuljeKayraIkkuna(); ;
        }



        private void btnLaske_Click(object sender, RoutedEventArgs e)
        {
            //Ottaa syötetyt tiedot talteen ja suorittaa laskutoimitukset niille
            
            EmptyResultFields();//tyhjentää tuloskentät
            int pyoristys = Convert.ToInt32(dbDesimaali.Text);//Otetaan valittu pyöristysarvo ohjelmasta
            List<SyotetytArvot> syotetytarvot = new List<SyotetytArvot>(); //Luo listan johon syötetyt arvot tallennetaan
            syotetytarvot = LuetaanSyotetytArvot(); //Luetaan syotetyt arvot luotuun listaan
            double kokomassa = LaskeKokonaisMassa(syotetytarvot, pyoristys); //lasketaan arvojen kokonaismäärä
            TulostenLasku_SeulalleJai(syotetytarvot, kokomassa, pyoristys); //Lasketaan laskutoimituksia ja syötetään tulokset tuloskenttiin
            TulostenLasku_LapaisyProsentti(syotetytarvot,kokomassa, pyoristys);//--------||----------
            Laskut l = new Laskut();
            if(markapaino.Text != String.Empty)
            {
                //Lasketaan kosteusprosentti jos märkäpaino-kentään on syötetty arvo
                tbKosteuspros.Text = Math.Round(l.KosteusProsentti(kokomassa, Convert.ToDouble(markapaino.Text)), pyoristys).ToString();
                //Lasketaan pesutappio
                if(rbPesuseulonta.IsChecked == true)
                {
                    tbPesutappio.Text = Math.Round((Math.Round(Convert.ToDouble(markapaino.Text), pyoristys)) - kokomassa, pyoristys).ToString();
                }
               
            }
            
        }

        

        private void TulostenLasku_LapaisyProsentti(List<SyotetytArvot> sa, double kokomassa, int pyoristys)
        {
            //Luodaan lista jonne tulokset laitetaan
            List<SyotetytArvot> tulos = new List<SyotetytArvot>();
            Laskut laskut = new Laskut();
            //Lasketaan ja syötetään tulokset tulos-listaan
            tulos = laskut.LapaisyProsentti(sa, kokomassa);
            //Käydään lista läpi ja tulostetaan tulokset oikeille kentille
            foreach(SyotetytArvot s in tulos)
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
                    case 17:
                        lapaisypros17.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                        break;
                    case 18:
                        lapaisypros18.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
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
                    case 18:
                        seulapros18.Text = tulos.ToString();
                        break;
                    case 19:
                        seulapros19.Text = tulos.ToString();
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
            return Math.Round(kokomassa,pyoristys);
        }


        private List<SyotetytArvot> LuetaanSyotetytArvot()
        {
            //Lukee käyttäjän syöttämät arvot, lisää ne listaan ja palauttaa listan
            //Syötetyissä arvoissa saattaa olla välejä (kaikkeja rivejä ei täytetty) joten koodi tarkistaa sen myös
            List<SyotetytArvot> sa = new List<SyotetytArvot>();
            double g0, g1, g2, g3, g4, g5, g6, g7, g8, g9, g10, g11, g12, g13, g14, g15, g16, g17,g18;
            try
            {
                if (double.TryParse(seulaG0.Text, out g0))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g0 = Convert.ToDouble(seulaG0.Text);
                    s.syote = g0;
                    s.index = 1;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG1.Text, out g1))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g1 = Convert.ToDouble(seulaG1.Text);
                    s.syote = g1;
                    s.index = 2;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG2.Text, out g2))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g2 = Convert.ToDouble(seulaG2.Text);
                    s.syote = g2;
                    s.index = 3;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG3.Text, out g3))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g3 = Convert.ToDouble(seulaG3.Text);
                    s.syote = g3;
                    s.index = 4;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG4.Text, out g4))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g4 = Convert.ToDouble(seulaG4.Text);
                    s.syote = g4;
                    s.index = 5;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG5.Text, out g5))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g5 = Convert.ToDouble(seulaG5.Text);
                    s.syote = g5;
                    s.index = 6;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG6.Text, out g6))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g6 = Convert.ToDouble(seulaG6.Text);
                    s.syote = g6;
                    s.index = 7;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG7.Text, out g7))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g7 = Convert.ToDouble(seulaG7.Text);
                    s.syote = g7;
                    s.index = 8;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG8.Text, out g8))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g8 = Convert.ToDouble(seulaG8.Text);
                    s.syote = g8;
                    s.index = 9;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG9.Text, out g9))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g9 = Convert.ToDouble(seulaG9.Text);
                    s.syote = g9;
                    s.index = 10;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG10.Text, out g10))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g10 = Convert.ToDouble(seulaG10.Text);
                    s.syote = g10;
                    s.index = 11;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG11.Text, out g11))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g11 = Convert.ToDouble(seulaG11.Text);
                    s.syote = g11;
                    s.index = 12;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG12.Text, out g12))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g12 = Convert.ToDouble(seulaG12.Text);
                    s.syote = g12;
                    s.index = 13;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG13.Text, out g13))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g13 = Convert.ToDouble(seulaG13.Text);
                    s.syote = g13;
                    s.index = 14;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG14.Text, out g14))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g14 = Convert.ToDouble(seulaG14.Text);
                    s.syote = g14;
                    s.index = 15;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG15.Text, out g15))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g15 = Convert.ToDouble(seulaG15.Text);
                    s.syote = g15;
                    s.index = 16;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG16.Text, out g16))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g16 = Convert.ToDouble(seulaG16.Text);
                    s.syote = g16;
                    s.index = 17;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG17.Text, out g17))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g17 = Convert.ToDouble(seulaG17.Text);
                    s.syote = g17;
                    s.index = 18;
                    sa.Add(s);
                }
                if (double.TryParse(seulaG18.Text, out g18))
                {
                    SyotetytArvot s = new SyotetytArvot();
                    g18 = Convert.ToDouble(seulaG18.Text);
                    s.syote = g18;
                    s.index = 19;
                    sa.Add(s);
                }
            }
            catch
            {

            }
            return sa;
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Kun syötetään tietoja seulakenttiin, tämä funktio varmistaa että vain numeroita ja pilkkuja voi laittaa kenttiin
            Regex regex = new Regex("^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e) 
        {
            //Näppäinkomento joka avaa File Explorerin (Ctrl + O)
            Open();
        }

        private void Open() //Avaa File Explorerin, jonka avulla ladataan tiedostoja
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

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //Jos painaa tallennusnappia, kutsutaan tallennusfunktiota joka on alla
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

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            //kun painaa pikanäppäinyhdistelmää (Ctrl + S), avataan tallennusdialogi
            Save();
        }
        private void CommandBinding_Print(object sender, ExecutedRoutedEventArgs e)
        {
            //Kun painaa pikanäppäinyhdistelmää (Ctrl + P), avataan tulostusdialogi
            PrintFile();
        }
        private void PrintFile()
        {
            //Avaa tulostusdialogin
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
        }
        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            //Sulkee ikkunan
            this.Close();
        }

        private void CreatePDF_Click(object sender, RoutedEventArgs e) //Luo PDF menupainikkeen Click funktio
        {


            //Avaa tallennus dialogin, joka tallentaa tiedoston oletuksena PDF-muodossa 
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PDF Document (*.pdf)|*.pdf";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    FileStream fs = (FileStream)fileDialog.OpenFile();

                    fs.Close();

                }
                catch (IOException ex)
                {
                    MessageBox.Show("Tiedosto avattu toisessa ohjelmassa", ex.Message);
                }


            }

            //Ottaa syötetyn tiedoston nimen sekä polun ja antaa sen parametrina PDFCreation funktiolle
            string text = fileDialog.FileName;
            Console.WriteLine(text);

            PDFCreation(text);

        }

        private void PDFCreation(string fileName) //Funktio mikä luo PDF-tiedostoon labelit, otsikot sekä ottaa tarvittavat arvot
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Seulonnantulos";

            PdfPage page = document.AddPage();

            XGraphics graphics = XGraphics.FromPdfPage(page);

            //Fonttien määritykset
            XFont font = new XFont("Verdanna", 15, XFontStyle.Regular);
            XFont textFont = new XFont("Verdanna", 10, XFontStyle.Regular);
            XFont otsikkoFont = new XFont("Verdanna", 15, XFontStyle.Bold);
            XFont alaOtsikkoFont = new XFont("Verdanna", 7, XFontStyle.Regular);
            XFont osoiteFont = new XFont("Verdanna", 7, XFontStyle.Regular);

            GetValueFromTextBoxes(); //Kutsuu funktiota mikä määrittää tekstilaatikoiden arvot

            //Piirtää tarvittavat otsikot ja niiden arvot PDF-tiedostoon
            graphics.DrawImage(XImage.FromFile(@".\Asetukset\kuvat\savonialogo1.jpg"), 10, 0, 120, 30);
            graphics.DrawString(alaOtsikkoHolder, alaOtsikkoFont, XBrushes.Black, new XRect(50, 25, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(osoiteHolder, osoiteFont, XBrushes.Black, new XRect(50, 33, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(postiHolder, osoiteFont, XBrushes.Black, new XRect(50, 40, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(puhHolder, osoiteFont, XBrushes.Black, new XRect(50, 48, page.Width, page.Height), XStringFormats.TopLeft);


            graphics.DrawString("KIVIAINESTUTKIMUS", otsikkoFont, XBrushes.Black, new XRect(280, 50, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulontaHolder, textFont, XBrushes.Black, new XRect(450, 55, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("SFS-EN 933-1", textFont, XBrushes.Black, new XRect(450, 40, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Työmaa", textFont, XBrushes.Black, new XRect(50, 80, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tyomaaHolder, textFont, XBrushes.Black, new XRect(100, 80, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Lajite", textFont, XBrushes.Black, new XRect(50, 95, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lajiteHolder, textFont, XBrushes.Black, new XRect(100, 95, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString(lisatietoHolder, textFont, XBrushes.Black, new XRect(50, 120, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näyte no", textFont, XBrushes.Black, new XRect(80, 80, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString(nayteHolder, textFont, XBrushes.Black, new XRect(150, 80, page.Width, page.Height), XStringFormats.TopCenter);

            graphics.DrawString("Päiväys", textFont, XBrushes.Black, new XRect(80, 95, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString(dateHolder, textFont, XBrushes.Black, new XRect(170, 95, page.Width, page.Height), XStringFormats.TopCenter);

            graphics.DrawString("Näytteen ottaja", textFont, XBrushes.Black, new XRect(95, 120, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString(nayteOttajaHolder, textFont, XBrushes.Black, new XRect(185, 120, page.Width, page.Height), XStringFormats.TopCenter);

            //Piirtää seulojen arvot PDF-dokumenttiin
            graphics.DrawString("#mm", textFont, XBrushes.Black, new XRect(50, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula1.Text, textFont, XBrushes.Black, new XRect(50, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula2.Text, textFont, XBrushes.Black, new XRect(50, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula3.Text, textFont, XBrushes.Black, new XRect(50, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula4.Text, textFont, XBrushes.Black, new XRect(50, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula5.Text, textFont, XBrushes.Black, new XRect(50, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula6.Text, textFont, XBrushes.Black, new XRect(50, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula7.Text, textFont, XBrushes.Black, new XRect(50, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula8.Text, textFont, XBrushes.Black, new XRect(50, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula9.Text, textFont, XBrushes.Black, new XRect(50, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula10.Text, textFont, XBrushes.Black, new XRect(50, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula11.Text, textFont, XBrushes.Black, new XRect(50, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula12.Text, textFont, XBrushes.Black, new XRect(50, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula13.Text, textFont, XBrushes.Black, new XRect(50, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula14.Text, textFont, XBrushes.Black, new XRect(50, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula15.Text, textFont, XBrushes.Black, new XRect(50, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula16.Text, textFont, XBrushes.Black, new XRect(50, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula17.Text, textFont, XBrushes.Black, new XRect(50, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula18.Text, textFont, XBrushes.Black, new XRect(50, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Pohja", textFont, XBrushes.Black, new XRect(50, 430, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää seuloille jäävien grammojen arvot PDF-dokumenttiin
            graphics.DrawString("Seulalle jäi g", textFont, XBrushes.Black, new XRect(150, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG0.Text, textFont, XBrushes.Black, new XRect(150, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG1.Text, textFont, XBrushes.Black, new XRect(150, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG2.Text, textFont, XBrushes.Black, new XRect(150, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG3.Text, textFont, XBrushes.Black, new XRect(150, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG4.Text, textFont, XBrushes.Black, new XRect(150, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG5.Text, textFont, XBrushes.Black, new XRect(150, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG6.Text, textFont, XBrushes.Black, new XRect(150, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG7.Text, textFont, XBrushes.Black, new XRect(150, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG8.Text, textFont, XBrushes.Black, new XRect(150, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG9.Text, textFont, XBrushes.Black, new XRect(150, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG10.Text, textFont, XBrushes.Black, new XRect(150, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG11.Text, textFont, XBrushes.Black, new XRect(150, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG12.Text, textFont, XBrushes.Black, new XRect(150, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG13.Text, textFont, XBrushes.Black, new XRect(150, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG14.Text, textFont, XBrushes.Black, new XRect(150, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG15.Text, textFont, XBrushes.Black, new XRect(150, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG16.Text, textFont, XBrushes.Black, new XRect(150, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG17.Text, textFont, XBrushes.Black, new XRect(150, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG18.Text, textFont, XBrushes.Black, new XRect(150, 430, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää seuloilla jäävien prosenttien arvot PDF-dokumenttiin
            graphics.DrawString("Seulalle jäi %", textFont, XBrushes.Black, new XRect(250, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros1.Text, textFont, XBrushes.Black, new XRect(250, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros2.Text, textFont, XBrushes.Black, new XRect(250, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros3.Text, textFont, XBrushes.Black, new XRect(250, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros4.Text, textFont, XBrushes.Black, new XRect(250, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros5.Text, textFont, XBrushes.Black, new XRect(250, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros6.Text, textFont, XBrushes.Black, new XRect(250, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros7.Text, textFont, XBrushes.Black, new XRect(250, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros8.Text, textFont, XBrushes.Black, new XRect(250, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros9.Text, textFont, XBrushes.Black, new XRect(250, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros10.Text, textFont, XBrushes.Black, new XRect(250, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros11.Text, textFont, XBrushes.Black, new XRect(250, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros12.Text, textFont, XBrushes.Black, new XRect(250, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros13.Text, textFont, XBrushes.Black, new XRect(250, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros14.Text, textFont, XBrushes.Black, new XRect(250, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros15.Text, textFont, XBrushes.Black, new XRect(250, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros16.Text, textFont, XBrushes.Black, new XRect(250, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros17.Text, textFont, XBrushes.Black, new XRect(250, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros18.Text, textFont, XBrushes.Black, new XRect(250, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulapros19.Text, textFont, XBrushes.Black, new XRect(250, 430, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää läpäisy % arvot PDF-dokumenttiin
            graphics.DrawString("Läpäisy %", textFont, XBrushes.Black, new XRect(350, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros1.Text, textFont, XBrushes.Black, new XRect(350, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros2.Text, textFont, XBrushes.Black, new XRect(350, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros3.Text, textFont, XBrushes.Black, new XRect(350, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros4.Text, textFont, XBrushes.Black, new XRect(350, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros5.Text, textFont, XBrushes.Black, new XRect(350, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros6.Text, textFont, XBrushes.Black, new XRect(350, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros7.Text, textFont, XBrushes.Black, new XRect(350, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros8.Text, textFont, XBrushes.Black, new XRect(350, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros9.Text, textFont, XBrushes.Black, new XRect(350, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros10.Text, textFont, XBrushes.Black, new XRect(350, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros11.Text, textFont, XBrushes.Black, new XRect(350, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros12.Text, textFont, XBrushes.Black, new XRect(350, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros13.Text, textFont, XBrushes.Black, new XRect(350, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros14.Text, textFont, XBrushes.Black, new XRect(350, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros15.Text, textFont, XBrushes.Black, new XRect(350, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros16.Text, textFont, XBrushes.Black, new XRect(350, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros17.Text, textFont, XBrushes.Black, new XRect(350, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lapaisypros18.Text, textFont, XBrushes.Black, new XRect(350, 415, page.Width, page.Height), XStringFormats.TopLeft);

            try
            {
                document.Save(fileName);

                Process.Start(fileName);
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show("Tyhjä tiedoston nimi", ex.Message);
            }

        }

        //Holderit tekstilaatikoille ja alasvetovalikoille
        string tyomaaHolder;
        string lajiteHolder;
        string seulontaHolder;
        string nayteHolder;
        string dateHolder;
        string nayteOttajaHolder;
        string lisatietoHolder;
        string alaOtsikkoHolder;
        string osoiteHolder;
        string postiHolder;
        string puhHolder;


        public void GetValueFromTextBoxes() //Funktio ottaa tekstilaatikoista arvot ja asettaa ne holder-muuttujille 
        {
            tyomaaHolder = tyomaa.Text.Trim();
            lajiteHolder = lajite.Text.Trim();
            nayteHolder = nayteNro.Text.Trim();
            dateHolder = date.Text.Trim();
            nayteOttajaHolder = naytteenOttaja.Text.Trim();
            lisatietoHolder = lisatieto.Text.Trim();
            alaOtsikkoHolder = alempiOtsikko.Text.Trim();
            osoiteHolder = lahiosoite.Text.Trim();
            postiHolder = osoite.Text.Trim();
            puhHolder = puh.Text.Trim();


            if (rbKuivaseulonta.IsChecked == true) //Tarkastaa kumpi radiobuttoneista on valittuna
            {
                seulontaHolder = rbKuivaseulonta.Content.ToString().Trim();
            }
            else
            {
                seulontaHolder = rbPesuseulonta.Content.ToString().Trim();
            }
        }








        private void Seula_DropDownClosed(object sender, EventArgs e)
        {
            //Kun valitsee uuden seulan seuladropdown valikoista, päivitetään seula-arvot ohjearvojen seuloihin
            SeulaArvotOhjeArvoihin();
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
            seulaValue17.Text = Seula17.Text.Trim();
            seulaValue18.Text = Seula18.Text.Trim();
        }


        private void EmptyFields_Click(object sender, RoutedEventArgs e) //Tyhjennä napin funktio
        {

            EmptyFields();
            //Kutsuu funktiota mikä tyhjentää kaikki tekstikentät seulalaskenta-ruudusta

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
            seulapros18.Text = String.Empty;
            seulapros19.Text = String.Empty;

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
            lapaisypros17.Text = String.Empty;
            lapaisypros18.Text = String.Empty;

            punnittuYhteensa.Text = String.Empty;
            tbKosteuspros.Text = String.Empty;

        }


    }
}
