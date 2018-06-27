﻿using Microsoft.Win32;
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
        MainWindow _main;
        public Massaohjelma(Window main)
        {
            InitializeComponent();
            _main = (MainWindow)main; //Otetaan aloitusikkuna talteen
            SeulaArvotOhjeArvoihin(); //Laitetaan valitut seula-arvot Ohjealue-ruudun seulakenttiin
            SeulaJsonLataus(); //Ladataan Seulat.json tiedostosta kaikki seula-arvot talteen seulalistaan
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
            //Suoritetaan seulalaskut tässä
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

        private void PDFCreation(string fileName)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Seulonnantulos";

            PdfPage page = document.AddPage();

            XGraphics graphics = XGraphics.FromPdfPage(page);

            //Fonttien määritykset
            XFont font = new XFont("Verdanna", 15, XFontStyle.Regular);
            XFont textFont = new XFont("Verdanna", 10, XFontStyle.Regular);
            XFont otsikkoFont = new XFont("Verdanna", 15, XFontStyle.Bold);
            XFont alaOtsikkoFont = new XFont("Verdanna", 8, XFontStyle.Regular);
            XFont osoiteFont = new XFont("Verdanna", 8, XFontStyle.Regular);

            GetValuesFromTextBoxes();

            //Piirtää tarvittavat otsikot ja niiden arvot PDF-tiedostoon
            graphics.DrawImage(XImage.FromFile(@".\Asetukset\logo.jpg"), 10, 0, 120, 30);
            graphics.DrawString("Yhdyskuntatekniikan laboratorio", alaOtsikkoFont, XBrushes.Black, new XRect(50, 27, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Opistotie 2 puh. (017) 255 6000", osoiteFont, XBrushes.Black, new XRect(50, 35, page.Width, page.Height), XStringFormats.TopLeft);


            graphics.DrawString("PANK 4102", textFont, XBrushes.Black, new XRect(150, 30, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString("P Ä Ä L L Y S T E T U T K I M U S", otsikkoFont, XBrushes.Black, new XRect(50, 50, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Urakka", textFont, XBrushes.Black, new XRect(50, 80, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(urakkaHolder, textFont, XBrushes.Black, new XRect(130, 80, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sek.asema", textFont, XBrushes.Black, new XRect(50, 95, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sekAsemaHolder, textFont, XBrushes.Black, new XRect(130, 95, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näytteen ottaja", textFont, XBrushes.Black, new XRect(60, 80, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString(nayteHolder, textFont, XBrushes.Black, new XRect(140, 80, page.Width, page.Height), XStringFormats.TopCenter);

            graphics.DrawString("Työkohde", textFont, XBrushes.Black, new XRect(49, 95, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString(tyoKohdeHolder, textFont, XBrushes.Black, new XRect(139, 95, page.Width, page.Height), XStringFormats.TopCenter);

            //Piirtää seulojen arvot PDF-dokumenttiin
            graphics.DrawString("#mm", textFont, XBrushes.Black, new XRect(55, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula1.Text, textFont, XBrushes.Black, new XRect(55, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula2.Text, textFont, XBrushes.Black, new XRect(55, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula3.Text, textFont, XBrushes.Black, new XRect(55, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula4.Text, textFont, XBrushes.Black, new XRect(55, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula5.Text, textFont, XBrushes.Black, new XRect(55, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula6.Text, textFont, XBrushes.Black, new XRect(55, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula7.Text, textFont, XBrushes.Black, new XRect(55, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula8.Text, textFont, XBrushes.Black, new XRect(55, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula9.Text, textFont, XBrushes.Black, new XRect(55, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula10.Text, textFont, XBrushes.Black, new XRect(55, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula11.Text, textFont, XBrushes.Black, new XRect(55, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula12.Text, textFont, XBrushes.Black, new XRect(55, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula13.Text, textFont, XBrushes.Black, new XRect(55, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula14.Text, textFont, XBrushes.Black, new XRect(55, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula15.Text, textFont, XBrushes.Black, new XRect(55, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(Seula16.Text, textFont, XBrushes.Black, new XRect(55, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Pohja", textFont, XBrushes.Black, new XRect(55, 400, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Seulalle jäi g", textFont, XBrushes.Black, new XRect(100, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG0.Text, textFont, XBrushes.Black, new XRect(100, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG1.Text, textFont, XBrushes.Black, new XRect(100, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG2.Text, textFont, XBrushes.Black, new XRect(100, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG3.Text, textFont, XBrushes.Black, new XRect(100, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG4.Text, textFont, XBrushes.Black, new XRect(100, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG5.Text, textFont, XBrushes.Black, new XRect(100, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG6.Text, textFont, XBrushes.Black, new XRect(100, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG7.Text, textFont, XBrushes.Black, new XRect(100, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG8.Text, textFont, XBrushes.Black, new XRect(100, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG9.Text, textFont, XBrushes.Black, new XRect(100, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG10.Text, textFont, XBrushes.Black, new XRect(100, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG11.Text, textFont, XBrushes.Black, new XRect(100, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG12.Text, textFont, XBrushes.Black, new XRect(100, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG13.Text, textFont, XBrushes.Black, new XRect(100, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG14.Text, textFont, XBrushes.Black, new XRect(100, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG15.Text, textFont, XBrushes.Black, new XRect(100, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulaG16.Text, textFont, XBrushes.Black, new XRect(100, 400, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Seulalle jäi %", textFont, XBrushes.Black, new XRect(180, 150, page.Width, page.Height), XStringFormats.TopLeft);
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

            graphics.DrawString("Läpäisy %", textFont, XBrushes.Black, new XRect(260, 150, page.Width, page.Height), XStringFormats.TopLeft);
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

            //Piirtää otsikkotietoja PDF-dokumenttiin
            graphics.DrawString("Päällyste", textFont, XBrushes.Black, new XRect(340, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(paallysteHolder, textFont, XBrushes.Black, new XRect(426, 150, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näyte no", textFont, XBrushes.Black, new XRect(340, 165, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(nayteNroHolder, textFont, XBrushes.Black, new XRect(426, 165, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Päiväys", textFont, XBrushes.Black, new XRect(340, 180, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(paivaysHolder, textFont, XBrushes.Black, new XRect(426, 180, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Klo", textFont, XBrushes.Black, new XRect(340, 195, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kloHolder, textFont, XBrushes.Black, new XRect(426, 195, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Paalu/kaista", textFont, XBrushes.Black, new XRect(340, 210, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(paaluHolder, textFont, XBrushes.Black, new XRect(426, 210, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää sideainepitoisuuksien määritykset PDF-dokumenttiin
            graphics.DrawString("SIDEAINEMÄÄRITYS", textFont, XBrushes.Black, new XRect(340, 230, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näytteen paino", textFont, XBrushes.Black, new XRect(340, 245, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("g", textFont, XBrushes.Black, new XRect(450, 245, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(naytePainoHolder, textFont, XBrushes.Black, new XRect(475, 245, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Kiviain. yht. paino", textFont, XBrushes.Black, new XRect(340, 260, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("g", textFont, XBrushes.Black, new XRect(450, 260, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviAineHolder, textFont, XBrushes.Black, new XRect(475, 260, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sideainemäärä", textFont, XBrushes.Black, new XRect(340, 275, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("g", textFont, XBrushes.Black, new XRect(450, 275, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sideAineMHolder, textFont, XBrushes.Black, new XRect(475, 275, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sideainepitoisuus", textFont, XBrushes.Black, new XRect(340, 290, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("%", textFont, XBrushes.Black, new XRect(450, 290, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sideainePHolder, textFont, XBrushes.Black, new XRect(475, 290, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää ohjearvojen määritykset PDF-dokumenttiin
            graphics.DrawString("OHJEARVOT", textFont, XBrushes.Black, new XRect(340, 310, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sideainepitoisuus", textFont, XBrushes.Black, new XRect(340, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("%", textFont, XBrushes.Black, new XRect(450, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sideAinePitHolder, textFont, XBrushes.Black, new XRect(475, 325, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Täytejauheen määrä", textFont, XBrushes.Black, new XRect(340, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("%", textFont, XBrushes.Black, new XRect(450, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tayteJauheHolder, textFont, XBrushes.Black, new XRect(475, 340, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("12", textFont, XBrushes.Black, new XRect(415, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak12Holder, textFont, XBrushes.Black, new XRect(475, 355, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("8", textFont, XBrushes.Black, new XRect(415, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak8Holder, textFont, XBrushes.Black, new XRect(475, 370, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("4", textFont, XBrushes.Black, new XRect(415, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak4Holder, textFont, XBrushes.Black, new XRect(475, 385, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("2", textFont, XBrushes.Black, new XRect(415, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak2Holder, textFont, XBrushes.Black, new XRect(475, 400, page.Width, page.Height), XStringFormats.TopLeft);


            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("0.5", textFont, XBrushes.Black, new XRect(415, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak05Holder, textFont, XBrushes.Black, new XRect(475, 415, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("0.063", textFont, XBrushes.Black, new XRect(415, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak0063Holder, textFont, XBrushes.Black, new XRect(475, 430, page.Width, page.Height), XStringFormats.TopLeft);


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

        string urakkaHolder;
        string sekAsemaHolder;
        string lisatietoHolder;
        string nayteHolder;
        string tyoKohdeHolder;

        string paallysteHolder;
        string nayteNroHolder;
        string paivaysHolder;
        string kloHolder;
        string paaluHolder;

        string naytePainoHolder;
        string kiviAineHolder;
        string sideAineMHolder;
        string sideainePHolder;

        string sideAinePitHolder;
        string tayteJauheHolder;
        string rak12Holder;
        string rak8Holder;
        string rak4Holder;
        string rak2Holder;
        string rak05Holder;
        string rak0063Holder;


        public void GetValuesFromTextBoxes()
        {
            urakkaHolder = urakka.Text.Trim();
            sekAsemaHolder = sekoitusAsema.Text.Trim();
            lisatietoHolder = lisatietoja.Text.Trim();
            nayteHolder = naytteenOttaja.Text.Trim();
            tyoKohdeHolder = tyokohde.Text.Trim();

            paallysteHolder = paallyste.Text.Trim();
            nayteNroHolder = nayteNro.Text.Trim();
            paivaysHolder = paivays.Text.Trim();
            kloHolder = klo.Text.Trim();
            paaluHolder = paaluKaista.Text.Trim();

            naytePainoHolder = naytteenPaino.Text.Trim();
            kiviAineHolder = kiviaineksenYhteispaino.Text.Trim();
            sideAineMHolder = sideainemaara.Text.Trim();
            sideainePHolder = sideainepitoisuus.Text.Trim();

            sideAinePitHolder = sideainepitoisuusOhjeArvo.Text.Trim();
            tayteJauheHolder = tayteJauheenMaara.Text.Trim();
            rak12Holder = rakeisuus12mm.Text.Trim();
            rak8Holder = rakeisuus8mm.Text.Trim();
            rak4Holder = rakeisuus4mm.Text.Trim();
            rak2Holder = rakeisuus2mm.Text.Trim();
            rak05Holder = rakeisuus05mm.Text.Trim();
            rak0063Holder = rakeisuus0065mm.Text.Trim();
        }

        
    }
}
