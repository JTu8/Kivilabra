using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for Massaohjelma.xaml
    /// </summary>
    public partial class Massaohjelma : Window
    {
        public Massaohjelma()
        {
            InitializeComponent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow m = new MainWindow();
            m.SuljeIkkuna("massa");
           
        }

        private void LaskeSeula_Click(object sender, RoutedEventArgs e)
        {
            //Suoritetaan seulalaskut tässä
        }


        private void paallyste_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Seula1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {
            Open();
        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                //this.Cursor = new Cursor(openFileDialog.OpenFile());
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();

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
    }
}
