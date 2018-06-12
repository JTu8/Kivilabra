using laskutesti1;//käyttää pros-luokkaa, jossa on lista läpäisyprosenteista, korjaa joskus
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Luokka sisältää kaikki tarvittavat laskut sovelluksen käyttämiseen
/// Ei tietääkseni tarvitse muuttaa näitä
/// </summary>
public static class Laskut
{
    //-------------------KIVI-OHJELMAN LASKUT---------------------
    public static double seulalleJai(double r, double m)
    {
        //Monta prosenttia jäi seulalle materiaalia
        //R * 100 / M
        //R = seulalle jääneen materiaalin massa
        //M = Koko näytemäärä grammoina
        return (r * (100 / m));
	}
    public static double punnitusYhteensa(double[] arvot)
    {
        //Punnitusmäärä yhteensä grammoina
        //r1+r2+r3...
        //r1 jne = seulalle jääneen materiaalin massa
        //arvot tulee arrayna funktioon

        double tulos = 0;
        for (int i = 0; i < arvot.Length; i++)
        {
            tulos += arvot[i];
        }
        return tulos;
    }
    public static List<pros> lapaisyProsentti(List<pros> r, double m)//List<pros> r tai double?[] r
    {
        //Prosenttimäärä massasta mikä meni seulasta läpi
        //100-SUM(100*r/m)
        //r = seulalle jääneen materiaalin massa
        //m = koko näytemäätä grammoina

        List<pros> tulos = new List<pros>();
        for (int i = 0; i < r.Count; i++)
        {
            if (i == 0)
            {
                if (r[i].tulos.HasValue)
                {
                    //tulos[i].index = r[i].index;
                    //tulos[i].tulos = (100 - (100 * r[i].tulos / m));
                    tulos.Add(new pros(){index = r[i].index, tulos = (100 - (100 * r[i].tulos / m)) });
                }  
            }
            else
            {
                if (r[i].tulos.HasValue)
                {
                    //tulos[i].index = r[i].index;
                    //tulos[i].tulos = tulos[i - 1].tulos - (100 * r[i].tulos / m);
                    tulos.Add(new pros() { index = r[i].index, tulos = (tulos[i - 1].tulos - (100 * r[i].tulos / m)) });
                }
                
            }
            
        }
        
        return tulos;
        
    }
    public static double kosteusprosentti(double kuv, double kos)
    {
        //Kosteusprosentti w (%)
        //w = [(kos-kuv)/kuv]*100
        //kos = kostean näytteen massa grammoina
        //kuv = kuivan näytteen massa grammoina
        return ((kos-kuv)/kuv)*100;
    }
    //-------------------MASSA-OHJELMAN LASKUT--------------------
    public static double fillerinMaara(double sf1, double sf2)
    {
        //Lasketaan fillerin määrä
        //f = sf2 - sf1
        //sf1 = sentrifuugi + paperi (g)
        //sf2 = sentrifuugi + paperi + filleri (g)
        return sf2 - sf1;
    }
    public static double sideainepitoisuus(double M1, double M2, double R, double f)
    {
        //Lasketaan sideaineen määrä prosentteina
        //s = [M1 - (M2 - R + f)] / M1 * 100
        //M1 = näytemäärä ennen testiä (g)
        //M2 = Rummun ja näytteen yhteismassa testin jälkeen (g)
        //R = rummun paino (g)
        //f = filleri (g)
        return (M1 - (M2 - R + f)) / M1 * 100;
    }
    public static double sideainemaara(double M1, double M2, double R, double f)
    {
        //Lasketaan sideaineen määrä grammoina
        //s = M1 - (M2 - R + f)
        //M1 = näytemäärä ennen testiä (g)
        //M2 = Rummun ja näytteen yhteismassa testin jälkeen (g)
        //R = rummun paino (g)
        //f = filleri (g)
        return M1 - (M2 - R + f);
    }
}
