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
    /// Interaction logic for Kiviohjelma.xaml
    /// </summary>
    public partial class Kiviohjelma : Window
    {
        public Kiviohjelma()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EmptyFields_Click(object sender, RoutedEventArgs e) //Tyhjennä napin funktio
        {

            EmptyGramFields(); //Kutsuu funktiota mikä tyhjentää seulalle jääneet gramma arvot
        }

        private void EmptyGramFields() //Funktio mikä tyhjentää seulalle jääneet gramma arvot
        {
            seulaG0.Text = String.Empty;
            seulaG1.Text = String.Empty;
            seulaG2.Text = String.Empty;
            seulaG3.Text = String.Empty;
            seulaG4.Text = String.Empty;
            seulaG5.Text = String.Empty;
            seulaG6.Text = String.Empty;
            seulaG7.Text = String.Empty;
            seulaG7.Text = String.Empty;
            seulaG8.Text = String.Empty;
            seulaG9.Text = String.Empty;
            seulaG10.Text = String.Empty;
            seulaG11.Text = String.Empty;
            seulaG12.Text = String.Empty;
            seulaG13.Text = String.Empty;
            seulaG14.Text = String.Empty;
            seulaG15.Text = String.Empty;
            seulaG16.Text = String.Empty;
            seulaG17.Text = String.Empty;
        }
    }
}
