/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */

namespace org.altervista.numerone.framework
{
	public interface CartaHelper
	{
		/// <summary>
		/// retituisce il seme della carta indicata
		/// </summary>
		/// <param name="Carta">carta di cui prendere il seme</param>
		/// <returns>un numero intero, verosimilmemnte da 0 a 4</returns>
		UInt16 GetSeme(UInt16 Carta);
		/// <summary>
		/// rettuisce il valore della carta indicata
		/// </summary>
		/// <param name="Carta">carta di cui prendere il valore</param>
		/// <returns>un numero intero, verosimilmente da 0 a 9</returns>
		UInt16 GetValore(UInt16 Carta);
		/// <summary>
		/// resituisce il punteggio della carta indicata
		/// </summary>
		/// <param name="Carta">carta di cui prendere il punteggio</param>
		/// <returns>restituisce il punteggio sulla base del gioco che si sta facendo</returns>
		UInt16 GetPunteggio(UInt16 Carta);
		/// <summary>
		/// retituisce il seme in formato stringa della carta presa in esame (uno tra s0 e s7)
		/// </summary>
		/// <param name="carta">carta da prendere in esame</param>
		/// <param name="mazzo">stabilisce se � italiano o francese</param>
		/// <param name="s0">primo seme italiano</param>
		/// <param name="s1">secondo seme italiano</param>
		/// <param name="s2">terzo seme italiano</param>
		/// <param name="s3">quarto seme italiano</param>
		/// <param name="s4">primo seme francese</param>
		/// <param name="s5">secondo seme francese</param>
		/// <param name="s6">terzo seme francese</param>
		/// <param name="s7">quarto seme francese</param>
		/// <returns>uno tra s0 e s7 a seconda del seme della carta</returns>
		string GetSemeStr(UInt16 carta, String mazzo, string s0, string s1, string s2, string s3, string s4, string s5, string s6, string s7);
		/// <summary>
		/// Accoppia seme e valore per tornare l'intero indicante la carta
		/// </summary>
		/// <param name="seme">seme della carta</param>
		/// <param name="valore">valore della carta</param>
		/// <returns>intero indicante la carta</returns>
		UInt16 GetNumero(UInt16 seme, UInt16 valore);
		/// <summary>
		/// Compara le due carte per stabilire ch sia la maggiore
		/// </summary>
		/// <param name="Carta">prima carta presa in esame</param>
		/// <param name="Carta1">seconda carta presa in esame</param>
		/// <returns>-1 se maggiore la prima, zero se uguale, 1 se maggiore la seconda</returns>
		public int CompareTo(UInt16 Carta, UInt16 Carta1);
	}
}