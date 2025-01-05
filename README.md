Framework di numerone in maui per la realizzazione di giochi di carte.
La codebase è in .net, con l'aggiunta di un resourcedictionary da passare.
Il resource dictionary deve includere 4 campi: bastoni, coppe, spade e denari da tradurre dall'italiano nella lingua desiderata, i 4 semi dei mazzi di carte italiane, o eventualmente francesi.
Il codice di apertura deve essere:

    o = LeggiOpzioni();
    briscolaDaPunti = o.briscolaDaPunti;

    e = new ElaboratoreCarteBriscola(briscolaDaPunti, 0, 39, 40);
    m = new Mazzo(e);
   
    m.SetNome(o.nomeMazzo);
    Carta.Inizializza(path dei mazzi, m, numerocarte, new org.altervista.numerone.framework.briscola.CartaHelper(e.GetCartaBriscola), "bastoni", "coppe", "denari", "spade", "fiori", "quadri", "cuori", "picche");

    if (o.nomeMazzo == "Napoletano")
    {
      asset = AssetLoader.Open(new Uri($"avares://{Assembly.GetEntryAssembly().GetName().Name}/Assets/retro_carte_pc.png"));
      cartaCpu.Source = new Bitmap(asset);
    }
    else
      try
          {
              cartaCpu.Source = new Bitmap(System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.Combine(App.path, "Mazzi"), m.GetNome()), "retro carte pc.png"));
          }
          catch (Exception ex)
          {
             asset = AssetLoader.Open(new Uri($"avares://{Assembly.GetEntryAssembly().GetName().Name}/Assets/retro_carte_pc.png"));
             cartaCpu.Source = new Bitmap(asset);
          }
    g = new Giocatore(new GiocatoreHelperUtente(), o.NomeUtente, dimensionemano);
    switch (o.livello) {
          case 1: helper = new GiocatoreHelperCpu0(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
          case 2: helper = new GiocatoreHelperCpu1(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
          default: helper = new GiocatoreHelperCpu2(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
    }
    cpu = new Giocatore(helper, o.NomeCpu, dimensionemano);
    briscola = Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola());
    for (UInt16 i = 0; i < dimensionemano; i++)
    {
      g.AddCarta(m);
      cpu.AddCarta(m);
    }
    Utente0.Source = g.GetImmagine(0);
    Utente1.Source = g.GetImmagine(1);
    Utente2.Source = g.GetImmagine(2);
    ....
    Cpu0.Source = cartaCpu.Source;
    Cpu1.Source = cartaCpu.Source;
    Cpu2.Source = cartaCpu.Source;
    ....
una volta fatto questo, in carta si avrà un vettore di numerocarte elementi, in g e cpu si avrà un vettore di dimensionemano elementi corrispondenti alle prime 2*dimensionemano carte del mazzo, 
che saranno riempite con addcarta.
Quando addcarta restituisce un IndexOutOfRangeException exception si avrà la fine del mazzo.
Utente0-dimensionemano sono le Image XAML corrispondenti alle carte del giocatore, mentre Cpu0-dimensionemano sono le Image corrispondenti alle carte della cpu.

Se avete un gioco di carte sul piatto, il modo di agire dei giocatori professionisti è quello di crearsi mentalmente il grafo di presa.
Mi spiego: per la scopa in mano ho un 8, è prendibile tramite 7+1 e 6+2, entrambi vanno bene. Vanno messi in un grafo e va così scomposto l'8, per poi prendere sulla base del piatto quello che consuma il maggior numero di carte del piatto stesso.
Quindi se ho 8 e 10 e posso prendere più carte che con l'8 invece che col 10, va giocato l'8.
