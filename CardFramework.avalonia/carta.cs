/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */


using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Reflection;

namespace org.altervista.numerone.framework
{
    public class Carta
    {
        private readonly UInt16 seme,
                   valore,
                   punteggio;
        private string semeStr;
        private static CartaHelper helper;
        private static Carta[] carte;
        private Bitmap img;

        private Carta(UInt16 n)
        {
            seme = helper.GetSeme(n);
            valore = helper.GetValore(n);
            punteggio = helper.GetPunteggio(n);
        }
        public static void Inizializza(string path, Mazzo m, ushort n, CartaHelper h, string s0, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string assembly)
        {
            helper = h;
            carte = new Carta[n];
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i] = new Carta(i);

            }
            CaricaImmagini(path,m, n, s0,s1,s2,s3,s4,s5,s6,s7, assembly);
        }
        public static Carta GetCarta(UInt16 quale) { return carte[quale]; }
        public UInt16 GetSeme() { return seme; }
        public UInt16 GetValore() { return valore; }
        public UInt16 GetPunteggio() { return punteggio; }
        public string GetSemeStr() { return semeStr; }
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return seme == c1.GetSeme(); }
        public int CompareTo(Carta c1)
        {
            if (c1 == null)
                return 1;
            else
                return helper.CompareTo(helper.GetNumero(GetSeme(), GetValore()), helper.GetNumero(c1.GetSeme(), c1.GetValore()));
        }

        public static Bitmap GetImmagine(UInt16 quale)
        {
            return carte[quale].img;
        }

        public Bitmap GetImmagine()
        {
            return img;
        }

        public static bool CaricaImmagini(String path, Mazzo m, ushort n, string s0, string s1, string s2, string s3, string s4, string s5, string s6, string s7, String assembly)
        {
            String s = $"{System.IO.Path.Combine(path, "Mazzi")}";
            for (UInt16 i = 0; i < n; i++)
            {
                Stream asset;
                if (m.GetNome() != "Napoletano")
                    try
                    {
                        carte[i].img = new Bitmap(System.IO.Path.Combine(System.IO.Path.Combine(Path.Combine(s, m.GetNome())),$"{i}.png"));
                    }
                    catch (Exception ex)
                    {
                        m.SetNome("Napoletano");
                        CaricaImmagini(path, m, n, s0, s1, s2, s3, s4, s5, s6, s7, assembly);
                        return false;
                    }
                else
                {
                    carte[i].img = new Bitmap(AssetLoader.Open(new Uri($"avares://{assembly}/Assets/{i}.png")));
                }
                carte[i].semeStr = helper.GetSemeStr(i, m.GetNome(), s0, s1, s2, s3, s4, s5, s6, s7);
            }
            return true;
        }

        public static void SetHelper(CartaHelper h) { helper = h; }

        public static Carta GetCartaBriscola() { return (helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola(); }
        public override string ToString()
        {
            string s = $"{valore + 1} di {semeStr}";
            if (helper is org.altervista.numerone.framework.briscola.CartaHelper)
                s += StessoSeme((helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola()) ? "*" : " ";
            else
                s += " ";
            return s;
        }
    }
}
