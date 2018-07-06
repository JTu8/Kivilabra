using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KiviMassaApp
{
    class SaveLoadFunc
    {
        List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        public string Save(Window w, List<Seulakirjasto> seulist)
        {
            seulalista = seulist;
            string json = null;
            //--------------------------Tarkoituksena ottaa ikkunasta tiedot talteen ja muuttaa ne JSON-stringiksi ja palauttaa se------------------------------------
            if (w.Name == "kiviohjelma")//Jos ikkuna mistä funktiota kutsuttiin on Kiviohjelma, mennään tästä sisälle
            {

                Kiviohjelma kiv = (Kiviohjelma)w; //Otetaan kiviohjelma tähän. Tämän avulla otetaan tallennettavat tiedot
                List<SyotetytArvot> syotteet = new List<SyotetytArvot>(); //Kaikki käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)" 
                List<SeulaTallennus> seulacombot = new List<SeulaTallennus>();//Seulacomboboksin arvot laitetaan tänne (mikä seula valittu, mones seula listasta se on ja mikä on comboboksin sijainti)
                bool pesuseulValinta = false;
                JakoAsetukset jako = new JakoAsetukset();//Laitetaan jaetun näytteen asetukset tänne talteen
                List<TekstiTiedot> tekstitiedot = new List<TekstiTiedot>();//Tähän laitetaan talteen muut tiedot ohjelman yläosasta (nimi, työmaa, lajite jne...)
                List<SeulaLapPros> sisOhjeAla = new List<SeulaLapPros>();//Sisempi ohjealue, alempi ohje%
                List<SeulaLapPros> sisOhjeYla = new List<SeulaLapPros>();//Sisempi ohjealue, ylempi ohje%
                List<SeulaLapPros> uloOhjeAla = new List<SeulaLapPros>();//Ulompi ohjealue, alempi ohje%
                List<SeulaLapPros> uloOhjeYla = new List<SeulaLapPros>();//Ulompi ohjealue, ylempi ohje%


                //Otetaan talteen käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)"
                foreach (Control c in kiv.seulaArvot.Children) //Katsoo läpi kaikki objektit seulaArvot-canvasista
                {
                    if (c.GetType() == typeof(TextBox)) //jos objektin tyyppi on TextBox
                    {
                        if (((TextBox)c).Tag.ToString() != null) //tarkistetaan ettei tag ole null. vaatii että kaikilla textbokseilla on jokin tagi
                        {
                            if (((TextBox)c).Tag.ToString() == "arvo") //tarkistaa onko tagi "arvo"
                            {
                                if (((TextBox)c).Text != String.Empty)
                                {
                                    SyotetytArvot s = new SyotetytArvot();
                                    double g = Convert.ToDouble(((TextBox)c).Text);
                                    string n = ((TextBox)c).Name; //otetaan objektin nimi talteen
                                    int index = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                    s.syote = g;
                                    s.index = index;
                                    syotteet.Add(s);
                                }

                            }
                        }

                    }
                }

                //Otetaan talteen käyttäjän valitsemat seulat "Seula"-combobokseista
                foreach (Control c in kiv.seulaArvot.Children)
                {
                    if (c.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox)c).Tag.ToString() != null)
                        {
                            if (((ComboBox)c).Tag.ToString() == "seula")
                            {
                                if (((ComboBox)c).Text != String.Empty)
                                {
                                    SeulaTallennus s = new SeulaTallennus();
                                    double se = Convert.ToDouble(((ComboBox)c).Text);
                                    string n = ((ComboBox)c).Name; //otetaan objektin nimi talteen
                                    int si = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                    s.seularvo = se;//Tallennetaan mikä seula on valittuna
                                    s.sijainti = si;//tallennetaan valitun comboboksin sijainti/järjestysluku
                                    s.seulansijainti = ((ComboBox)c).SelectedIndex;//Tallennetaan mones seula valittu seula on seulalistasta
                                    seulacombot.Add(s);
                                }

                            }
                        }

                    }
                }

                //Onko valittuna kuiva- vai pesuseulonta
                if (kiv.rbPesuseulonta.IsChecked == true)
                {
                    pesuseulValinta = true;
                }
                else
                {
                    pesuseulValinta = false;
                }

                double? jakokerroin = null;
                int jakoseula = kiv.JakoSeula.SelectedIndex;
                string jakoseularvo = kiv.JakoSeula.Text;
                if (kiv.tbJakoKerroin.Text != String.Empty && Double.TryParse(kiv.tbJakoKerroin.Text, out double r) == true)
                {
                    jakokerroin = Convert.ToDouble(kiv.tbJakoKerroin.Text);
                }
                jako.jakokerroin = jakokerroin;
                jako.jakoseula = jakoseula;
                jako.jakoseularvo = jakoseularvo;

                //Otetaan talteen tekstitiedot ohjelman yläosasta
                foreach (Control c in kiv.tietoarvot.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TekstiTiedot t = new TekstiTiedot();
                        t.otsikko = ((TextBox)c).Name;
                        if (((TextBox)c).Text != String.Empty)
                        {
                            t.tieto = ((TextBox)c).Text;
                        }
                        else
                        {
                            t.tieto = null;
                        }
                        tekstitiedot.Add(t);
                    }
                }
                foreach (Control c in kiv.osoitearvot.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TekstiTiedot t = new TekstiTiedot();
                        t.otsikko = ((TextBox)c).Name;
                        if (((TextBox)c).Text != String.Empty)
                        {
                            t.tieto = ((TextBox)c).Text;
                        }
                        else
                        {
                            t.tieto = null;
                        }
                        tekstitiedot.Add(t);
                    }
                }

                //Otetaan talteen ohjealueet
                for (int i = 0; i < 4; i++)//Otetaan ohjealueet talteen yksi kolumni kerrallaan
                {
                    switch (i)
                    {
                        case 0:
                            int j = 17;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "sisAla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = j,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                sisOhjeAla.Add(ohj);

                                            }
                                            j--;

                                        }
                                    }

                                }
                            }
                            break;
                        case 1:
                            int k = 17;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "sisYla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = k,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                sisOhjeYla.Add(ohj);

                                            }
                                            k--;
                                        }
                                    }

                                }
                            }
                            break;
                        case 2:
                            int l = 17;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "uloAla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = l,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                uloOhjeAla.Add(ohj);

                                            }
                                            l--;
                                        }
                                    }

                                }
                            }
                            break;
                        case 3:
                            int m = 17;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "uloYla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = m,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                uloOhjeYla.Add(ohj);

                                            }
                                            m--;
                                        }
                                    }

                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                var kaikkilistat = new { nimi = "Actual", data = new List<object>() };
                //HUOMIOI TÄMÄ! tässä näkyy se järjestys mistä nämä listat löytyy tiedostosta
                kaikkilistat.data.Add(seulalista);
                kaikkilistat.data.Add(syotteet);
                kaikkilistat.data.Add(seulacombot);
                kaikkilistat.data.Add(pesuseulValinta);
                kaikkilistat.data.Add(tekstitiedot);
                kaikkilistat.data.Add(sisOhjeAla);
                kaikkilistat.data.Add(sisOhjeYla);
                kaikkilistat.data.Add(uloOhjeAla);
                kaikkilistat.data.Add(uloOhjeYla);

                json = JsonConvert.SerializeObject(kaikkilistat);

            }
            return json;
        }


        public string LoadAll(Window w, string json)
        {
            if (w.Name == "kiviohjelma")
            {
                Kiviohjelma kiv = (Kiviohjelma)w;
                List<KaikkiListat> kaikkilistat = new List<KaikkiListat>();

                List<SyotetytArvot> syotteet = new List<SyotetytArvot>(); //Kaikki käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)" 
                List<SeulaTallennus> seulacombot = new List<SeulaTallennus>();//Seulacomboboksin arvot laitetaan tänne (mikä seula valittu, mones seula listasta se on ja mikä on comboboksin sijainti)
                //bool pesuseulValinta = false;
                JakoAsetukset jako = new JakoAsetukset();//Laitetaan jaetun näytteen asetukset tänne talteen
                List<TekstiTiedot> tekstitiedot = new List<TekstiTiedot>();//Tähän laitetaan talteen muut tiedot ohjelman yläosasta (nimi, työmaa, lajite jne...)
                List<SeulaLapPros> sisOhjeAla = new List<SeulaLapPros>();//Sisempi ohjealue, alempi ohje%
                List<SeulaLapPros> sisOhjeYla = new List<SeulaLapPros>();//Sisempi ohjealue, ylempi ohje%
                List<SeulaLapPros> uloOhjeAla = new List<SeulaLapPros>();//Ulompi ohjealue, alempi ohje%
                List<SeulaLapPros> uloOhjeYla = new List<SeulaLapPros>();//Ulompi ohjealue, ylempi ohje%


                kaikkilistat = JsonConvert.DeserializeObject<List<KaikkiListat>>(json);
                foreach(SyotetytArvot s in kaikkilistat[1].data)
                {
                    syotteet.Add(s);
                }
                
            }

            return null;
        }



        private class SeulaTallennus
        {
            public double seularvo { get; set; }//Seulan arvo, käytetään vertailussa jos pääseulalista muuttuu tallennuksen ja latauksen välillä
            public int sijainti { get; set; }//Mistä seulacomboboxissa tämä on otettu, pitäisi olla 1-18
            public int seulansijainti { get; set; }//Mones seula Comboboxissa tämä on, pitäisi olla 0-(pääseulalistan maksimikoko), käytetään vertailussa jos pääseulalista muuttuu tallennuksen ja latauksen välillä
        }
        private class JakoAsetukset
        {
            public double? jakokerroin { get; set; }//Käyttäjän syöttämä jakokerroin
            public int jakoseula { get; set; }//valitun jakoseulan indeksi, arvot väliltä 0-[seulalistan koko]. 0 tarkoittaa että jakoa ei tehdä
            public string jakoseularvo { get; set; } //valitun jakoseulan teksti, käytetään vertailussa jos pääseulalista muuttuu tallennuksen ja latauksen välillä
        }
        private class TekstiTiedot
        {
            public string otsikko { get; set; }
            public string tieto { get; set; }
        }
        private class KaikkiListat
        {
            public string nimi { get; set; }
            public List<object> data { get; set; }
        }


    }
    
}
