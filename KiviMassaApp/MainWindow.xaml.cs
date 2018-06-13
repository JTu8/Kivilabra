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

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenKivi(object sender, RoutedEventArgs e) //Avaa kiviohjelman
        {
            Kiviohjelma k = new Kiviohjelma();
            k.Show();
        }

        private void OpenMassa(object sender, RoutedEventArgs e) //Avaa massaohjelman
        {
            Massaohjelma m = new Massaohjelma();
            m.Show();
        }
    }
}
