using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiviMassaApp
{
    public static class SeulaArvoJSONLuonti
    {
        public static void SeulaJSONLuonti()
        {
            List<Seulakirjasto> sl = new List<Seulakirjasto>();
            string json;
            float[] ar = new float[] { 200, 150, 125, 100, 90, 80, 63, 56, 50, 45, 40, 31.5f, 25, 22.4f, 20, 18, 16, 12.5f, 11.5f, 10, 8, 6.3f, 5.6f, 5, 4, 2, 1, 0.5f, 0.25f, 0.125f, 0.063f };
            for (int i = 0; i < ar.Length; i++)
            {
                Seulakirjasto s = new Seulakirjasto();
                s.seula = ar[i];
                s.index = i;
                sl.Add(s);
            }
            json = JsonConvert.SerializeObject(sl);



            /*foreach (Seula e in sl)
            {
                Console.WriteLine(e.seula);
            }*/
            try
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                    File.WriteAllText(@".\Asetukset\Seulat.json", json);
                }
                else
                {
                    File.WriteAllText(@".\Asetukset\Seulat.json", json);
                }
                
            }catch(Exception e)
            {
                Console.WriteLine("VIRHE SeulaArvoJSONLuonti.cs TIEDOSTOSSA: KANSIORAKENTEESSA ONGELMA: "+e.Message+",   "+e.StackTrace);
            }
            
        }
        
    }
}
