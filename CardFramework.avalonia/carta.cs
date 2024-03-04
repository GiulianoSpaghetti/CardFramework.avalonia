/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */


using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CBriscola.Avalonia;
using System;
using System.IO;
using System.Reflection;
using System.Resources;

namespace org.altervista.numerone.framework
{
    public class Carta
    {
        private readonly UInt16 seme,
                   valore,
                   punteggio;
        private string semeStr;
        private readonly CartaHelperBriscola helper;
        private readonly static Carta[] carte = new Carta[40];
        private Bitmap img;

        private Carta(UInt16 n, CartaHelperBriscola h)
        {
            helper = h;
            seme = helper.GetSeme(n);
            valore = helper.GetValore(n);
            punteggio = helper.GetPunteggio(n);
        }
        public static void Inizializza(string path, Mazzo m, ushort n, CartaHelperBriscola h, ResourceDictionary d)
        {
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i] = new Carta(i, h);

            }
            CaricaImmagini(path,m, n, h, d);
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

        public override string ToString()
        {
            return $"{valore + 1} di {semeStr}{(StessoSeme(helper.GetCartaBriscola()) ? "*" : " ")} ";
        }

        public static Bitmap GetImmagine(UInt16 quale)
        {
            return carte[quale].img;
        }

        public Bitmap GetImmagine()
        {
            return img;
        }

        public static bool CaricaImmagini(String path, Mazzo m, ushort n, CartaHelperBriscola helper, ResourceDictionary d)
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
                        CaricaImmagini(path, m, n, helper, d);
                        return false;
                    }
                else
                {
                    carte[i].img = new Bitmap(AssetLoader.Open(new Uri($"avares://{Assembly.GetEntryAssembly().GetName().Name}/Assets/{i}.png")));
                }
                carte[i].semeStr = helper.GetSemeStr(i, m.GetNome(), d);
            }
            return true;
        }
    }
}