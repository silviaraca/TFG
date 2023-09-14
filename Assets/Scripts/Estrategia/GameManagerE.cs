using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class GameManagerE : MonoBehaviour
{
  public List<Carta> mazo = new List<Carta>();
  public List<Carta> descarte = new List<Carta>();
  public GameObject zonaDescarte;
  public GameObject Canvas, Cards, Personajes;
  public GameObject cardAldeano, cardAgua, cardEstaca, cardMina, cardSangre, cardTumba, cardAjo, cardCuchillo, cardNosferatu, cardRatonela, cardCarreta, cardLamias, cardLogan; //Objetos de las cartas que existen ya, añadir conforme 
  public GameObject characterZombie, characterAbomination, characterWolf, characterVampire; //Lo de arriba pero enemigos 
  public Transform[] espacioMano;
  public Casilla[] tablero;
  public int tamTab;
  public bool[] espacioManoSinUsar;
  public List<Carta> mano = new List<Carta>();
  public List<Personaje> listaPnj = new List<Personaje>();
  public List<Personaje> listaPnjEnemigosEnTablero = new List<Personaje>();
  public List<GameObject> listaPnjEnemigos = new List<GameObject>();
  public List<GameObject> filasPnj = new List<GameObject>();
  public TextMeshProUGUI textoFase;
  public GameObject draggingPos;
  public Sprite casAta;
  public Sprite casAli;
  public Sprite casVacia;
  public Sprite casMarcada;
  public Sprite casEstandar;
  public Sprite casFuturoAtaque;
  public bool win, loose;
  public TextMeshProUGUI textoCartasPorJugar;
  public TextMeshProUGUI textoTurnos;
  public DialogueScripted ds;
  public GameObject aliado;
  //Sitema de turnos
  private const int maxRob = 2, maxJug = 2; //Contadores de máximo número de cartas a robar y a jugar
  private static int fase;//Identificador de fase que se usará para entrar o no a hacer cosas, 
  private static int nRobadas = 0, nCartasJugadas = 0; //contador de cartas robadas para no sobrepasar el límite y contador de cartas jugadas para no sobrepasar el límite
  private static bool primerTurno, pasaTurno, roba;

  private static List<string> listaCartas = new List<string>();
  private static List<string> listaEnemigos = new List<string>();
  private static List<Casilla> listaCasMovible = new List<Casilla>();
  private static List<Casilla> listaCasAtacable = new List<Casilla>();
  public GameObject puntero;
  public GameObject Dracula;
  public int turnosParaPerder; //Según el combate se configurará
  public int enemigosVivos;
  public bool tutorial, dracula;
  private static bool dialogo = true, spawnea2 = false, spawnea = true, mata = true, pasa = false, zoomed = false, moviendose = false;
  private GameObject drac;

  public void Start(){
    //Tomar las cartas de la lista de cartas que haya en deckData al iniciar la escena
    if (PlayerPrefs.HasKey("DeckData") && !tutorial)
    {
      string deckData = PlayerPrefs.GetString("DeckData");
      listaCartas = JsonConvert.DeserializeObject<List<string>>(deckData);
    }
    else{ //Si no hubiese data(cargar directamente estrategia para probar cosas o el tuto) hace esto
      listaCartas.Add("Ajo");
      listaCartas.Add("Ajo");
      listaCartas.Add("Agua");
      listaCartas.Add("Agua");
      listaCartas.Add("Estaca");
      listaCartas.Add("Estaca");
      List<string> inventory = new List<string>();
      if(PlayerPrefs.HasKey("InventoryCards")){
          string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
          inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
      }
      inventory.Add("Agua");
      inventory.Add("Estaca");
      inventory.Add("Ajo");
      string inventoryData2 = JsonConvert.SerializeObject(inventory);
      PlayerPrefs.SetString("InventoryCards", inventoryData2);
      PlayerPrefs.Save();
    }

    //Esto se tomará de una lista de enemigos según el contrincante
    if (PlayerPrefs.HasKey("EnemiesData") && !tutorial)
    {
        string enemiesData = PlayerPrefs.GetString("EnemiesData");
        listaEnemigos = JsonConvert.DeserializeObject<List<string>>(enemiesData);
    }
    else{//Si no hubiese data(cargar directamente estrategia para probar cosas o el tuto) hace esto
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
      listaEnemigos.Add("Zombie");
    }

    if (PlayerPrefs.HasKey("DaculaFight") && !tutorial)
    {
      drac = Instantiate(Dracula);
      listaPnjEnemigosEnTablero.Add(drac.gameObject.GetComponent<Personaje>());
      drac.transform.SetParent(filasPnj[tablero[61].fila].transform, false);
      drac.transform.position = new Vector3(tablero[61].transform.position.x + 5, tablero[61].transform.position.y + 40, tablero[61].transform.position.z+10);
      drac.gameObject.GetComponent<Personaje>().setCasAct(tablero[61]);
      tablero[61].pnj = drac.GetComponent<Personaje>();
      tablero[61].vacia = false;
      dracula = true;
    }

    textoCartasPorJugar.enabled = false;
    enemigosVivos = listaEnemigos.Count;
    pasaTurno = false;
    roba = false;
    primerTurno = true;
    fase = 0;
    win = false;
    loose = false;
    creaCartas();
    creaEnemigos();
  }
  public void Update(){
    if(!moviendose) {
      if(!tutorial){
        if(!win && !loose){
          if(fase == 0){ //fase inicial y efectos de inicio de turno
            textoTurnos.text = "Turns before enemy victory: " + turnosParaPerder;
            if(primerTurno){ //Roba dos cartas inicialmente
              DrawCard();
              DrawCard();
              primerTurno = false;
            }
            //efectos de inicio de turno
            nRobadas = 0;
            roba = false;
            fase++; //No pasa hasta que se ejecuten todos
            textoFase.text = "Phase: " + fase;
          }
          else if(fase == 1 && (nRobadas == 2 || pasaTurno)){ //Fase1 de robo de los mazos finalizada
            fase++; //Suma fase
            textoFase.text = "Phase: " + fase;
            nRobadas = 0;
            pasaTurno = false;
            textoCartasPorJugar.enabled = true;
            int porJugar = maxJug - nCartasJugadas;
            textoCartasPorJugar.text = "Cards remaining to play: " + porJugar;
            modificaEfectos();
          }
          else if((fase == 2 || fase == 4) && pasaTurno){ //Fase2 de juegar cartas1, Fase4 de jugar cartas2 finalizan
            pasaTurno = false;
            textoCartasPorJugar.enabled = false;
            if(fase == 4)
              nCartasJugadas = 0;
            fase++;
            textoFase.text = "Phase: " + fase;
          }
          else if(fase == 3 && (nRobadas == 2 || pasaTurno)){ //Fase3 de mover pnj finaliza
            activaFinTuro(fase);
            fase++;
            textoFase.text = "Phase: " + fase;
            for(int i = 0; i < listaPnj.Count; i++){
              listaPnj[i].setMovAct(listaPnj[i].getMaxMov());
              listaPnj[i].setNumAtaAct(listaPnj[i].getNumAta());
            }
            pasaTurno = false;
            textoCartasPorJugar.enabled = true;
            int porJugar = maxJug - nCartasJugadas;
            textoCartasPorJugar.text = "Cards remaining to play: " + porJugar;
            modificaEfectos(); //Cambia efectos de cartas de segunda fase y cuarta fase
          }
          else if(fase == 5){
            //efectos de final de turno y movimientos de enemigos
            moviendose = true;
            movEnemigos(); //Todos efectos de fin de turno
            desPintaCas(); //Limpia el tablero
          }
          if(fase == 1 && roba){ //permite robar si estás en fase1
            DrawCard();
            roba = false;
          }
          if(listaPnjEnemigos.Count == 0 && listaPnjEnemigosEnTablero.Count == 0){
            win = true;
            PlayerPrefs.SetString("WinCombate", "win"); //Manda victoria
            PlayerPrefs.Save();
            //Salir de escena ganando
            this.gameObject.GetComponent<CambioEscenaEstrategia>().cargaRPG(); //Carga RPG si ganas el combate
          }
          else if(turnosParaPerder == 0){
            loose = true;
            PlayerPrefs.SetString("LooseCombate", "loose"); //Manda derrota
            PlayerPrefs.Save();
            //Salir de escena perdiendo
            this.gameObject.GetComponent<CambioEscenaEstrategia>().cargaRPG(); //Carga RPG si pierde el combate
          }
          if(dracula){
            if(drac.GetComponent<Personaje>().getVida() <= 0){
              win = true;
              PlayerPrefs.SetString("WinCombate", "win"); //Manda victoria
              PlayerPrefs.Save();
              //Salir de escena ganando
              this.gameObject.GetComponent<CambioEscenaEstrategia>().cargaRPG(); //Carga RPG si ganas el combate
            }
          }
        }
      }
      else{ //Script del tutorial
        if(!win && !loose && !dialogo){
          if(fase == 0){ //fase inicial y efectos de inicio de turno
            textoTurnos.text = "Turns before enemy victory: " + turnosParaPerder;
            //Aquí primer diálogo explicando lo que es la estrategia
            if(primerTurno){
              primerTurno = false;
              nRobadas = 0;
              roba = false;
              fase++; //No pasa hasta que se ejecuten todos
              textoFase.text = "Phase: " + fase;
              //Roba las dos cartas que estás scripteadas para robar
              drawScripted(3);
              drawScripted(3);
            }
          }
          else if(nRobadas > 0 && fase == 1 && (nRobadas == 2 || pasaTurno)){ //Fase1 de robo de los mazos

            //Dialogo del robo de cartas mostrando que hay un contador de cartas por robar
            //Obliga a robar una carta antes de explicar poder pasar turno

            fase++;
            textoFase.text = "Phase: " + fase;
            nRobadas = 0;
            pasaTurno = false;
            textoCartasPorJugar.enabled = true;
            int porJugar = maxJug - nCartasJugadas;
            textoCartasPorJugar.text = "Cards remaining to play: " + porJugar;
            ds.setReactivable();
            ds.reactivarDialogo();
          }
          else if(((fase == 2 && nCartasJugadas == 1) || (fase == 4 && nCartasJugadas == 2)) && pasaTurno){ //Fase2 de juegar cartas1, Fase4 de jugar cartas2
            //FASE 2
            //Dialogo dice que mete un enemigo para que pueda probar las cartas
            //Spawn un enemigo
            //Explica que puede usar la carta de estaca que es de primer turno

            //FASE 4
            //Dialogo dice que puede usar cartas de segundo turno de jugar cartas 
            //Hace aparecer un aldeano herido rodeado de enemigos a uno de vida y explica que puede usar agua bendita
            //Solo permite usar agua bendita en la casilla elegida para curar y matar a todos los malos
            pasaTurno = false;
            textoCartasPorJugar.enabled = false;
            if(fase == 4)
              nCartasJugadas = 0;
            fase++;
            textoFase.text = "Phase: " + fase;
            spawnea2 = true;
            ds.setReactivable();
            ds.reactivarDialogo();
          }
          else if(fase == 3 && (nRobadas == 2 || pasaTurno) && pasa){ //Fase3 de mover pnj
            //Esta fase la explicará en tutorial 2 de personajes
            fase++;
            textoFase.text = "Phase: " + fase;
            for(int i = 0; i < listaPnj.Count; i++){
              listaPnj[i].setMovAct(listaPnj[i].getMaxMov());
              listaPnj[i].setNumAtaAct(listaPnj[i].getNumAta());
            }
            pasa = false;
            pasaTurno = false;
            textoCartasPorJugar.enabled = true;
            int porJugar = maxJug - nCartasJugadas;
            textoCartasPorJugar.text = "Cards remaining to play: " + porJugar;
            ds.setReactivable();
            ds.reactivarDialogo();
            listaPnj[0].danar(1, "");
            tablero[14].muyPintada = true;
            tablero[14].setColor(casMarcada);
          }
          else if(fase == 5){
            //Fin del tutorial, propuesta de una pelea de práctica con una pool de enemigos ya elegida
            enemigosVivos = 0;
            //efectos de final de turno y movimientos de enemigos
            moviendose = true;
            movEnemigos();
          }
          else{
            pasaTurno = false;
          }
          if(fase == 1 && roba && !ds.getReactivable()){ //Robo inicial
            drawScripted(2);
            nRobadas++;
            roba = false;
          }
          else{
            roba = false;
          }
          if(enemigosVivos == 0){
            win = true;
            this.gameObject.GetComponent<CambioEscenaEstrategia>().cargaRPG();
            //Salir de escena ganado
          }
          else if(turnosParaPerder == 0){
            loose = true;
            this.gameObject.GetComponent<CambioEscenaEstrategia>().cargaRPG();
            //Salir de escena perdiendo
          }
          if(fase == 2 && spawnea && nCartasJugadas == 0){ //Spawnea el enemigo para probar el hechizo
            spawnea = false;
            spawnEnemigo(); //Aparece un enemigo
          }
          else if (fase == 2 && mata && nCartasJugadas == 1){ //Si juega la carta activa el dialogo de nuevo
            mata = false;
            ds.setReactivable(); //permite reactivar el juego
            ds.reactivarDialogo(); //reactiva el dialogo
          }
          else if (fase == 3 && spawnea2){ //Al acabar el dialogo de spawnear lo activa
            spawnea2 = false;
            spawnEnemigoScripted(13); //Aparece un enemigo en la casilla seleccionada
            spawnAliadoScripted(15); //Aparece un aliado en la casilla seleccionada
          }
          else if (fase == 3 && mata){ //Si ataca al enemigo se activa dialogo
            mata = false;
            pasa = true;
            ds.setReactivable(); //permite reactivar el juego
            ds.reactivarDialogo(); //reactiva el dialogo
          }
          else if (fase == 4 && mata){ //Cuando usa el agua bendita se activa el dialogo de nuevo
            mata = false;
            pasa = true;
            ds.setReactivable();
            ds.reactivarDialogo();
          }
        }
      }
    }
  }
  public void danaVampsAliados(){
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].vampire)
        listaPnj[i].danar(1, "");
    }
  }
  public void ataqueLobos(){
    int countAtaque = 0;
    for(int i = 0; i < listaPnjEnemigosEnTablero.Count; i++){
      if(listaPnjEnemigosEnTablero[i].gameObject.name == "Wolf(Clone)"){
        countAtaque++;
      }
    }
    for(int i = 0; i < listaPnjEnemigosEnTablero.Count; i++){
      if(listaPnjEnemigosEnTablero[i].gameObject.name == "Wolf(Clone)"){
        listaPnjEnemigosEnTablero[i].setAtaque(countAtaque);
      }
    }
    countAtaque = 0;
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].gameObject.name == "Wolf(Clone)" || listaPnj[i].gameObject.name == "LoganWolf(Clone)"){
        countAtaque++;
      }
    }
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].gameObject.name == "Wolf(Clone)"){
        listaPnj[i].setAtaque(countAtaque);
      }
    }
  }
  public void reactivarDialogo(){
    if(!zoomed){
      ds.setReactivable();
      ds.reactivarDialogo();
      zoomed = true;
    }
  }
  private void transformaPnj(){
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].transformando && listaPnj[i].getContTrans() > 0){
        listaPnj[i].setContTrans(listaPnj[i].getContTrans() - 1);
      }
      else if(listaPnj[i].transformando && listaPnj[i].getContTrans() == 0){
        listaPnjEnemigosEnTablero.Add(listaPnj[i]);
        listaPnj[i].enemigo = true;
        listaPnj[i].setMovAct(listaPnj[i].getMaxMov());
        listaPnj[i].setNumAtaAct(listaPnj[i].getNumAta());
        listaPnj[i].transformando = false;
        listaPnj[i].GetComponent<Image>().color = new Color(255, 0, 100, 255);
        listaPnj.Remove(listaPnj[i]);
      }
    }
  }
  private void danaVenenorPnj(){
    for(int i = 0; i < listaPnjEnemigosEnTablero.Count; i++){
      if(listaPnjEnemigosEnTablero[i].turnosVeneno > 0){
        listaPnjEnemigosEnTablero[i].turnosVeneno--;
        listaPnjEnemigosEnTablero[i].danar(listaPnjEnemigosEnTablero[i].danoVeneno, "");
      }
      if(listaPnjEnemigosEnTablero[i].turnosVeneno == 0) listaPnjEnemigosEnTablero[i].danoVeneno = 0;
    }
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].turnosVeneno > 0){
        listaPnj[i].turnosVeneno--;
        listaPnj[i].danar(listaPnj[i].danoVeneno, "");
      }
      if(listaPnj[i].turnosVeneno == 0) listaPnj[i].danoVeneno = 0;
    }
  }
  private void contadorInmunes(){
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].turnosInmune > 0){
        listaPnj[i].turnosInmune--;
      }
      if(listaPnj[i].turnosInmune <= 0) listaPnj[i].inmune = false;
    }
    for(int i = 0; i < listaPnjEnemigosEnTablero.Count; i++){
      if(listaPnjEnemigosEnTablero[i].turnosInmune > 0){
        listaPnjEnemigosEnTablero[i].turnosInmune--;
      }
      if(listaPnjEnemigosEnTablero[i].turnosInmune <= 0) listaPnjEnemigosEnTablero[i].inmune = false;
    }
  }
  private void modificaEfectos(){
    for(int i = 0; i < mano.Count; i++){
      if(mano[i].nombreCarta == "Ajo" && fase == 2){
        mano[i].swapEnvenena(false);
        mano[i].swapInmuniza(true);
        mano[i].swapDefensaHechizo(true);
        mano[i].swapAtaqueHechizo(false);
      }
      else if(mano[i].nombreCarta == "Ajo" && fase == 4){
        mano[i].swapEnvenena(true);
        mano[i].swapInmuniza(false);
        mano[i].swapDefensaHechizo(false);
        mano[i].swapAtaqueHechizo(true);
      }
    }
  }
  private void activaFinTuro(int f){
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].turnoEfectoFin == f){
        listaPnj[i].activaEfectoFin();
      }
    }
  }
  private void spawnEnemigoScripted(int pos){
    GameObject randEnemigo = listaPnjEnemigos[Random.Range(0, listaPnjEnemigos.Count)];
    listaPnjEnemigosEnTablero.Add(randEnemigo.GetComponent<Personaje>());
    listaPnjEnemigos.Remove(randEnemigo);
    randEnemigo.transform.SetParent(filasPnj[tablero[pos].fila].transform, false);
    randEnemigo.transform.position = new Vector3(tablero[pos].transform.position.x + 5, tablero[pos].transform.position.y + 40, tablero[pos].transform.position.z+10);
    randEnemigo.gameObject.GetComponent<Personaje>().setCasAct(tablero[pos]);
    tablero[pos].pnj = randEnemigo.GetComponent<Personaje>();
    tablero[pos].vacia = false;
  }
  private void spawnAliadoScripted(int pos){
    GameObject ali = Instantiate(aliado);
    listaPnj.Add(ali.GetComponent<Personaje>());
    ali.transform.SetParent(filasPnj[tablero[pos].fila].transform, false);
    ali.transform.position = new Vector3(tablero[pos].transform.position.x + 5, tablero[pos].transform.position.y + 40, tablero[pos].transform.position.z+10);
    ali.gameObject.GetComponent<Personaje>().setCasAct(tablero[pos]);
    tablero[pos].pnj = ali.GetComponent<Personaje>();
    tablero[pos].vacia = false;
  }
  private void drawScripted(int index){
    Carta cardDraw = mazo[index];
    for(int i = 0; i < espacioManoSinUsar.Length; i++){
        if(espacioManoSinUsar[i]){
            cardDraw.gameObject.SetActive(true);
            cardDraw.handIndex = i;
            cardDraw.enMano = true;
            mano.Add(cardDraw);
            cardDraw.transform.position = espacioMano[i].position;
            espacioManoSinUsar[i] = false;
            mazo.Remove(cardDraw);
            return;
        }
    }
  }
  private void creaCartas(){ //Genera la lista de cartas en el mazo del jugador a partir de una lista de strings
    GameObject cartaAnadida = null;
    for(int i = 0; i < listaCartas.Count; i++){
      //Por cada nueva carta que añadamos al juego hacer la correspondiente aquí
      if(listaCartas[i] == "Aldeano"){
        cartaAnadida = Instantiate(cardAldeano);
      }
      else if(listaCartas[i] == "Agua"){
        cartaAnadida = Instantiate(cardAgua);
      }
      else if(listaCartas[i] == "Estaca"){
        cartaAnadida = Instantiate(cardEstaca);
      }
      else if(listaCartas[i] == "Mina"){
        cartaAnadida = Instantiate(cardMina);
      }
      else if(listaCartas[i] == "Sangre"){
        cartaAnadida = Instantiate(cardSangre);
      }
      else if(listaCartas[i] == "Tumba"){
        cartaAnadida = Instantiate(cardTumba);
      }
      else if(listaCartas[i] == "Ajo"){
        cartaAnadida = Instantiate(cardAjo);
      }
      else if(listaCartas[i] == "Cuchillo"){
        cartaAnadida = Instantiate(cardCuchillo);
      }
      else if(listaCartas[i] == "Nosferatu"){
        cartaAnadida = Instantiate(cardNosferatu);
      }
      else if(listaCartas[i] == "Ratonela"){
        cartaAnadida = Instantiate(cardRatonela);
      }
      else if(listaCartas[i] == "Carreta"){
        cartaAnadida = Instantiate(cardCarreta);
      }
      else if(listaCartas[i] == "Lamias"){
        cartaAnadida = Instantiate(cardLamias);
      }
      else if(listaCartas[i] == "Logan"){
        cartaAnadida = Instantiate(cardLogan);
      }
      cartaAnadida.transform.SetParent(Cards.transform, false);
      mazo.Add(cartaAnadida.gameObject.GetComponent<Carta>());
    }
  }
  public void DrawCard(){
    if(fase == 0 || fase == 1){
      nRobadas++;
      if(mazo.Count >= 1){
        Carta randCard = mazo[Random.Range(0,mazo.Count)];
        for(int i = 0; i < espacioManoSinUsar.Length; i++){
            if(espacioManoSinUsar[i]){
                randCard.gameObject.SetActive(true);
                randCard.handIndex = i;
                randCard.enMano = true;
                mano.Add(randCard);
                randCard.transform.position = espacioMano[i].position;
                espacioManoSinUsar[i] = false;
                mazo.Remove(randCard);
                return;
            }
        }
      }
      else if (descarte.Count >= 1){
        for(int i = 0; i < descarte.Count; i++){
          mazo.Add(descarte[i]);
        }
        descarte.Clear();
        DrawCard();
      }
    }
  }
  private void creaEnemigos(){ //Genera la lista de enemigos a partir de una lista de strings para que aparezcan en los turnos enemigos
    GameObject pnjAnadido = null;
    for(int i = 0; i < listaEnemigos.Count; i++){
      //Por cada nueva carta que añadamos al juego hacer la correspondiente aquí
      if(listaEnemigos[i] == "Zombie"){
        pnjAnadido = Instantiate(characterZombie);
      }
      if(listaEnemigos[i] == "Abomination"){
        pnjAnadido = Instantiate(characterAbomination);
      }
      if(listaEnemigos[i] == "Vampire"){
        pnjAnadido = Instantiate(characterVampire);
      }
      if(listaEnemigos[i] == "Wolf"){
        pnjAnadido = Instantiate(characterWolf);
      }
      pnjAnadido.transform.SetParent(Personajes.transform, false);
      listaPnjEnemigos.Add(pnjAnadido);
    }
  }
  private void movEnemigos(){
    StartCoroutine(muevePausado());
  }
  IEnumerator muevePausado(){
    ataqueLobos();
    for(int i = 0; i < listaPnjEnemigosEnTablero.Count; i++){
      mueveEnemigo(listaPnjEnemigosEnTablero[i]);
      yield return new WaitForSeconds(0.5f);
    }
    if(listaPnjEnemigos.Count > 0){
      spawnEnemigo();
      if(listaPnjEnemigos.Count > 0)
        spawnEnemigo();
    }
    transformaPnj();
    danaVenenorPnj();
    contadorInmunes();
    if(dracula){
      curaDraculaTurno();
      if(turnosParaPerder%2 == 1){
        print(turnosParaPerder);
        ataqueAleatorioDracula();
      }
      else{
        print(turnosParaPerder);
        activaAtaque();
      }
    }
    turnosParaPerder--;
    fase = 0; //Reinicia fases cuando haya terminado lo anterior
    moviendose = false;
  }
  private void mueveEnemigo(Personaje pnj){
    int posIniX, posIniY;
    if(listaPnj.Count > 0){
      while(pnj.getNumAtaAct() > 0){
        posIniX = pnj.cas.getPosX();
        posIniY = pnj.cas.getPosY();
        int posArr = posIniX+posIniY*8; //Casilla posición del personaje
        if(pnj.getNumAtaAct() > 0)
          pintaAta(pnj, posArr, pnj.getRang(), pnj.cas); //Pinta ataques de casillas adyacentes
        pintaCas(pnj, posArr, pnj.getMovAct(), pnj.getRang()); //Pinta los movimientos para ver a dónde puede moverse
        
        if(listaCasAtacable.Count > 0){ //Si hay personajes a los que atacar busca cual es el más óptimo
          Personaje enemigoAtacar = listaCasAtacable[0].pnj;
          bool matable = false;
          if(pnj.getAtaque() >= enemigoAtacar.getVida()) matable = true; //Si puede matar al pnj lo marca
          for(int i = 1; i < listaCasAtacable.Count;i++){
            if(cambioObjetivo(pnj, enemigoAtacar, listaCasAtacable[i].pnj, posIniX, posIniY, matable) ){ //Busca si hay un objetivo más óptimo al que atacar
                                                                                                         //Se basa en la distancia, numataques y vida que le quede
              enemigoAtacar = listaCasAtacable[i].pnj; //Selecciona al enemigo si es el má sóptimo al que atacar
            }
          }
          //Ahora se movería hacia el enemigo seleccionado y le atacaría
          pnj.setNumAtaAct(pnj.getNumAtaAct()-1); //Usa el ataque
          Casilla cas = enemigoAtacar.cas; //Casilla a la que atacará
          if(pnj.vampire){ //Se cura uno si es vampiro
            pnj.danar(-1, "");
          }
          if(pnj.envenena){ //Envenena si envenena el pnj
              enemigoAtacar.envenenarPnj(pnj.turnosMeteVeneno, pnj.danoMeteVeneno);
          }
          if(enemigoAtacar.danar(pnj.getAtaque(), "")){ //Si mata vacía la casilla
              //Aquí cosas que pasen si se muere el enemigo
              cas.vacia = true;
              cas.pnj = null;
          }
          pnj.transform.SetParent(filasPnj[cas.getCasAnt().fila].transform, false); //Mueve el personaje
          pnj.transform.position = new Vector3(cas.getCasAnt().transform.position.x + 5, cas.getCasAnt().transform.position.y + 40, cas.getCasAnt().transform.position.z+10);
          pnj.setMovAct(pnj.getMovAct()-cas.getCasAnt().getConsumeMov()); //Descuenta el movimiento
          pnj.cas.vacia = true; //Vacía la casilla anterior
          pnj.cas.pnj = null;
          cas.getCasAnt().vacia = false; //Ocupa la casilla actual
          cas.getCasAnt().pnj = pnj;
          pnj.cas = cas.getCasAnt();
        }
        else { //Si no puede atacar se mueve hacia personajes
          Personaje enemigoAcercar = listaPnj[0];
          for(int i = 1; i< listaPnj.Count; i++){
            if((listaPnj[i].getVida() <= pnj.getAtaque() && listaPnj[i].getVida() > enemigoAcercar.getVida()) ||  listaPnj[i].getVida() < enemigoAcercar.getVida()){
              enemigoAcercar = listaPnj[i]; //Busca el personaje más óptimo al que atacar
            }
          }
          mueveHacia(enemigoAcercar.getCasAct().getPosX(), enemigoAcercar.getCasAct().getPosY(), pnj); //Se mueve hacia el personaje
          pnj.setNumAtaAct(0); //Gasta ataques
        }
        desPintaCas();
      }
    }
    else{ //Si no hay personajes aliados mueve hacia el spawn aliado
      posIniX = pnj.cas.getPosX();
      posIniY = pnj.cas.getPosY();
      int posArr = posIniX+posIniY*8;
      if(pnj.getNumAtaAct() > 0)
        pintaAta(pnj, posArr, pnj.getRang(), pnj.cas);
      pintaCas(pnj, posArr, pnj.getMovAct(), pnj.getRang());
      mueveHacia(1, 1, pnj);
      desPintaCas();
    }
    pnj.setMovAct(pnj.getMaxMov());
    pnj.setNumAtaAct(pnj.getNumAta());
  }
  public bool hayCarreta(){
    for(int i = 0; i < listaPnj.Count; i++){
      if(listaPnj[i].gameObject.name == "Carreta(Clone)")
        return true;
    }
    return false;
  }
  private bool cambioObjetivo(Personaje pnj, Personaje p1, Personaje p2, int posIniX, int posIniY, bool matable){
    if (matable && pnj.getAtaque() >= p2.getVida() && 
        ((p1.getVida() < p2.getVida()) || 
        (p1.getVida() == p2.getVida() && 
        p1.getCasAct().getConsumeMov() > p2.getCasAct().getConsumeMov()))) return true; //Si ambos son matables pero el segundo tiene más vida o tienen la misma vida pero el segundo está más cerca matará el segundo
    else if(!matable && pnj.getAtaque() >= p2.getVida()){ //Si el primero no es matable y el segundo sí prefiere el segundo
      matable = true;
      return true;
    }
    else if(!matable && ((p1.getVida() > p2.getVida()) || 
        (p1.getVida() == p2.getVida() && 
        p1.getCasAct().getConsumeMov() > p2.getCasAct().getConsumeMov())))return true; //Si ninguno es matable va a por el de menor vida si tienen la misma vida va al más cercano
    return false; //Si no cumple ninguna anterior se queda con el primer objetivo
  }
  private void pintaCas(Personaje pnj, int pos, int mov, int rang){
    int posAux;
    int movAux = mov;
    if(movAux > 0){
        if(((posAux = pos+1)%8) != 0 && tablero[posAux].vacia && (!tablero[posAux].pintada || tablero[posAux].getConsumeMov() > 3 - movAux)){
            ejecutaPintado(pnj, posAux, movAux, rang);
            if(pnj.getNumAtaAct() > 0)
                pintaAta(pnj, posAux, rang, tablero[posAux]);
        }
        if((((posAux = pos-1)+1) %8) != 0 && tablero[posAux].vacia && (!tablero[posAux].pintada || tablero[posAux].getConsumeMov() > 3 - movAux)){
            ejecutaPintado(pnj, posAux, movAux, rang);
            if(pnj.getNumAtaAct() > 0)
                pintaAta(pnj, posAux, rang, tablero[posAux]);
        }
        if((posAux = pos+8) < tablero.Length && tablero[posAux].vacia && (!tablero[posAux].pintada || tablero[posAux].getConsumeMov() > 3 - movAux)){
            ejecutaPintado(pnj, posAux, movAux, rang);
            if(pnj.getNumAtaAct() > 0)
                pintaAta(pnj, posAux, rang, tablero[posAux]);
        }
        if((posAux = pos-8) >= 0 && tablero[posAux].vacia && (!tablero[posAux].pintada || tablero[posAux].getConsumeMov() > 3 - movAux)){
            ejecutaPintado(pnj, posAux, movAux, rang);
            if(pnj.getNumAtaAct() > 0)
                pintaAta(pnj, posAux, rang, tablero[posAux]);
        }
    }
  }
  private void pintaAta(Personaje pnj, int pos, int rang, Casilla cas){
      int posAux;
      for(int i = rang; i > 0; i--)
          if(i > 0){
              if(((posAux = pos+i)%8) != i-1 && !tablero[posAux].vacia && !tablero[posAux].pintada && !tablero[posAux].pnj.inmune){
                  ejecutaAtacable(posAux, cas);
              }
              if((((posAux = pos-i)+1) %8) != rang-1 && !tablero[posAux].vacia && !tablero[posAux].pintada && !tablero[posAux].pnj.inmune){
                  ejecutaAtacable(posAux, cas);
              }
              if((posAux = pos+8*i) < tablero.Length && !tablero[posAux].vacia && !tablero[posAux].pintada && !tablero[posAux].pnj.inmune){
                  ejecutaAtacable(posAux, cas);
              }
              if((posAux = pos-8*i) >= 0 && !tablero[posAux].vacia && !tablero[posAux].pintada && !tablero[posAux].pnj.inmune){
                  ejecutaAtacable(posAux, cas);
              }
          }
  }
  private void ejecutaPintado(Personaje pnj, int posAux, int mov, int rang){
      tablero[posAux].pintada = true;
      tablero[posAux].setConsumeMov(pnj.getMaxMov() - mov + 1);
      pintaCas(pnj, posAux, mov-1, rang);
  }
  private void ejecutaAtacable(int posAux, Casilla cas){
      if(!tablero[posAux].pnj.enemigo){
          tablero[posAux].pintada = true;
          tablero[posAux].setCasAnt(cas);
          listaCasAtacable.Add(tablero[posAux]);
      }
  }
  private void desPintaCas(){
      for(int i = 0; i < tablero.Length;i++){
          tablero[i].pintada = false;
          tablero[i].setConsumeMov(0);
          tablero[i].setCasAnt(null);
          tablero[i].setColor(tablero[i].getImagenIni());
      }
      listaCasMovible.Clear();
      listaCasAtacable.Clear();
  }
  private void mueveHacia(int posX, int posY, Personaje pnj){
    int minDist = 10000;
    int dist;
    int pos = 0;
    Casilla casillaMueve = null;
    for(int i = 0; i < tablero.Length; i++){
      if(tablero[i].pintada){
        dist = Mathf.Abs(tablero[i].getPosX()-posX) + Mathf.Abs(tablero[i].getPosY()-posY);
        if(dist < minDist){
          pos = i;
          minDist = dist;
          casillaMueve = tablero[i];
        }
      }
    }
    if(casillaMueve != null){
      pnj.cas.vacia = true;
      pnj.cas.pnj = null;
      pnj.cas = casillaMueve;
      casillaMueve.vacia = false;
      casillaMueve.pnj = pnj;
      pnj.transform.SetParent(filasPnj[casillaMueve.fila].transform, false);
      pnj.transform.position = new Vector3(casillaMueve.transform.position.x + 5, casillaMueve.transform.position.y + 40, casillaMueve.transform.position.z+10);
      pnj.setMovAct(pnj.getMovAct()-casillaMueve.getConsumeMov());
    }
  }
  private void spawnEnemigo(){
    GameObject randEnemigo = listaPnjEnemigos[Random.Range(0, listaPnjEnemigos.Count)];
    listaPnjEnemigosEnTablero.Add(randEnemigo.GetComponent<Personaje>());
    listaPnjEnemigos.Remove(randEnemigo);
    int xCas = Random.Range(0, 64);
    while(!tablero[xCas].vacia || !tablero[xCas].esSpawnEne()){
      xCas = Random.Range(0, 64);
    }
    randEnemigo.transform.SetParent(filasPnj[tablero[xCas].fila].transform, false);
    randEnemigo.transform.position = new Vector3(tablero[xCas].transform.position.x + 5, tablero[xCas].transform.position.y + 40, tablero[xCas].transform.position.z+10);
    randEnemigo.gameObject.GetComponent<Personaje>().setCasAct(tablero[xCas]);
    tablero[xCas].pnj = randEnemigo.GetComponent<Personaje>();
    tablero[xCas].vacia = false;
  }
  public int getFase(){
    return fase;
  }
  public int getCarJugadas(){
    return nCartasJugadas;
  }
  public void setCarJugadas(int n){
    nCartasJugadas = n;
    int porJugar = maxJug - nCartasJugadas;
    textoCartasPorJugar.text = "Cartas por jugar: " + porJugar;
  }
  public void pasaT(){
    pasaTurno = true;
  }
  public void robaCarta(){
    roba = true;
  }
  public void finDialogo(){
    dialogo = false;
  }
  public void reactivaDialogo(){
    dialogo = false;
  }
  public void setMata(){
    mata = true;
  }
  private void ataqueAleatorioDracula(){
    int randCas = 0;
    for(int i = 0; i < 6; i++){
      randCas = Random.Range(0, 64);
      tablero[randCas].futuroAtaque = true;
      tablero[randCas].setColor(casFuturoAtaque);
    }
  }
  private void activaAtaque(){
    for(int i = 0; i < tablero.Length; i++){
      if(tablero[i].futuroAtaque && !tablero[i].vacia){
        tablero[i].pnj.danar(2, "");
      }
      tablero[i].recuperaCasOriginal();
      tablero[i].setColor(tablero[i].getImagenIni());
      tablero[i].futuroAtaque = false;
    }
  }
  private void curaDraculaTurno(){
    drac.GetComponent<Personaje>().danar(-1, "");
  }
}
