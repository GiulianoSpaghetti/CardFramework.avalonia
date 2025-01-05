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
    /// <summary>
    /// Indica la struttura carta che identifica una carta del mazzo
    /// </summary>
    public class Carta
    {
        private readonly UInt16 seme,
                   valore,
                   punteggio;
        private string semeStr;
        private static CartaHelper helper;
        private static Carta[] carte;
        private Bitmap img;

        /// <summary>
        /// Costruttore privato perché le carte devono essere immutabili.
        /// </summary>
        /// <param name="n">numero intero indicante il numero intero della carta</param>
        private Carta(UInt16 n)
        {
            seme = helper.GetSeme(n);
            valore = helper.GetValore(n);
            punteggio = helper.GetPunteggio(n);
        }
        /// <summary>
        /// Vero costruttore, identifica un numero di carte pari ad n, careica le immagini dal filesystem se possibile ed inizializza il vettore delle carte
        /// </summary>
        /// <param name="path">path da cui caricare le immagini sul filesystem</param>
        /// <param name="m">prende il nome del mazzo ed identifica se va caricata dal filesystemo dalle risorse</param>
        /// <param name="n">numero delle carte</param>
        /// <param name="h">per evitare una ereditarietà selvaggia, si è scelto di usare una classe a parte per il comportamento specifico della classe</param>
        /// <param name="s0">prima delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s1">seconda delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s2">terza delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s3">quarta delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s4">prima delle 4 stringhe indicante il seme francese</param>
        /// <param name="s5">seconda delle 4 stringhe indicante il seme francese</param>
        /// <param name="s6">terza delle 4 stringhe indicante il seme francese</param>
        /// <param name="s7">quarta delle 4 stringhe indicante il seme francese</param>
        /// <param name="assembly">assembly dal quale caricare le carte se sono risorse</param>
        /// <returns>true se non ci sono stati errore, false se si è ricaduti nel caricare le immagini dalle risorse invece che dal filesysteme e non era richiesto</returns>
        public static bool Inizializza(string path, Mazzo m, ushort n, CartaHelper h, string s0, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string assembly)
        {
            helper = h;
            carte = new Carta[n];
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i] = new Carta(i);

            }
            return CaricaImmagini(path,m, n, s0,s1,s2,s3,s4,s5,s6,s7, assembly);
        }
        /// <summary>
        /// Restituisce la struttura indicante la carta numero quale
        /// </summary>
        /// <param name="quale">numero della carta da prendere</param>
        /// <returns>la struttura indicante la carta presa</returns>
        public static Carta GetCarta(UInt16 quale) { return carte[quale]; }
        /// <summary>
        /// Getter che ritorna il seme della carta
        /// </summary>
        /// <returns>seme della carta</returns>
        public UInt16 GetSeme() { return seme; }
        /// <summary>
        /// Getter che ritorna il valore della carta
        /// </summary>
        /// <returns>valore della carta</returns>
        public UInt16 GetValore() { return valore; }
        /// <summary>
        /// Getter che restuistuisce il punteggio della carta
        /// </summary>
        /// <returns>punteggio della carta</returns>
        public UInt16 GetPunteggio() { return punteggio; }
        /// <summary>
        /// Getter che restituisce il seme in valore stringa, uno degli 8 passati ad inizializza
        /// </summary>
        /// <returns>il seme in formato stringa della carta</returns>
        public string GetSemeStr() { return semeStr; }
        /// <summary>
        /// Dice se due carte hanno lo stesso seme
        /// </summary>
        /// <param name="c1">carta con cui confrontare il seme, può essere null</param>
        /// <returns>true se la carta chiamante ha lo stesso seme di c1</returns>
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return seme == c1.GetSeme(); }
        /// <summary>
        /// Compara due carte
        /// </summary>
        /// <param name="c1">carta con cui confrontare il seme, può essere null</param>
		/// <returns>-1 se maggiore la prima, zero se uguale, 1 se maggiore la seconda</returns>
        public int CompareTo(Carta c1)
        {
            if (c1 == null)
                return 1;
            else
                return helper.CompareTo(helper.GetNumero(GetSeme(), GetValore()), helper.GetNumero(c1.GetSeme(), c1.GetValore()));
        }
        /// <summary>
        /// Retituisce l'immagine della carta quale
        /// </summary>
        /// <param name="quale">numero della carta</param>
        /// <returns>l'immagine della carta</returns>
        public static Bitmap GetImmagine(UInt16 quale)
        {
            return carte[quale].img;
        }
        /// <summary>
        /// Restituisce l'immagine della carta chiamata
        /// </summary>
        /// <returns>l'immagine della carta</returns>
        public Bitmap GetImmagine()
        {
            return img;
        }
        /// <summary>
        /// Carica in memoria le immagini delle carte
        /// </summary>
        /// <param name="path">path in cui cercare le carte</param>
        /// <param name="m">prende il nome del mazzo e stabilisce se caricarle dalle risorse o dal filesystem</param>
        /// <param name="n">numero di carte da caricare</param>
        /// <param name="s0">prima delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s1">seconda delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s2">terza delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s3">quarta delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s4">prima delle 4 stringhe indicante il seme francese</param>
        /// <param name="s5">seconda delle 4 stringhe indicante il seme francese</param>
        /// <param name="s6">terza delle 4 stringhe indicante il seme francese</param>
        /// <param name="s7">quarta delle 4 stringhe indicante il seme francese</param>
        /// <param name="assembly">assembly dal quale caricare le carte se sono risorse</param>
        /// <returns>true se le carte sono state caricate, false se le carte sono state caricate dalle risorse e non era richiesto</returns>
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

        /// <summary>
        /// setter dela classe che identifica il comportamento che le carte devono avere
        /// </summary>
        /// <param name="h">classe che incapsula il comportamti dekke caere</param>
        public static void SetHelper(CartaHelper h) { helper = h; }

        /// <summary>
        /// restituisce la struttura che identifica il valore di vbriscola
        /// </summary>
        /// <returns></returns>
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
