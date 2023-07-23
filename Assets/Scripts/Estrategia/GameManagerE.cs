using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerE : MonoBehaviour
{
  public List<Carta> mazo = new List<Carta>();
  public List<Carta> descarte = new List<Carta>();
  public GameObject zonaDescarte;
  public GameObject Canvas, Cards, Personajes;
  public GameObject cardAldeano, cardAgua, cardEstaca; //Objetos de las cartas que existen ya, añadir conforme 
  public GameObject characterZombie; //Lo de arriba pero enemigos 
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
  public bool win, loose;

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


public void Start(){
  //Esto será tomar las cartas de la lista de cartas que haya en otro lado al iniciar la escena
  listaCartas.Add("Aldeano");
  listaCartas.Add("Aldeano");
  listaCartas.Add("Agua");
  listaCartas.Add("Agua");
  listaCartas.Add("Estaca");
  listaCartas.Add("Estaca");

  //Esto se tomará de una lista de enemigos según el contrincante
  listaEnemigos.Add("Zombie");
  listaEnemigos.Add("Zombie");
  listaEnemigos.Add("Zombie");
  listaEnemigos.Add("Zombie");
  listaEnemigos.Add("Zombie");

  pasaTurno = false;
  roba = false;
  primerTurno = true;
  fase = 0;
  creaCartas();
  creaEnemigos();
}

  public void Update(){
    if(fase == 0){ //fase inicial y efectos de inicio de turno
      if(primerTurno){
        DrawCard();
        DrawCard();
        primerTurno = false;
      }
      //efectos de inicio de turno
      nRobadas = 0;
      roba = false;
      fase++; //No pasa hasta que se ejecuten todos
      textoFase.text = "Fase Actual: " + fase;
    }
    else if(fase == 1 && (nRobadas == 2 || pasaTurno)){ //Fase1 de robo de los mazos
      fase++;
      textoFase.text = "Fase Actual: " + fase;
      nRobadas = 0;
      pasaTurno = false;
    }
    else if((fase == 2 || fase == 4) && pasaTurno){ //Fase2 de juegar cartas1, Fase4 de jugar cartas2
      fase++;
      textoFase.text = "Fase Actual: " + fase;
      pasaTurno = false;
      nCartasJugadas = 0;
    }
    else if(fase == 3 && (nRobadas == 2 || pasaTurno)){ //Fase3 de mover pnj
      fase++;
      textoFase.text = "Fase Actual: " + fase;
      for(int i = 0; i < listaPnj.Count; i++){
        listaPnj[i].setMovAct(listaPnj[i].getMaxMov());
        listaPnj[i].setNumAtaAct(listaPnj[i].getNumAta());
      }
      pasaTurno = false;
    }
    else if(fase == 5){
      //efectos de final de turno y movimientos de enemigos
      movEnemigos();
      if(listaPnjEnemigos.Count > 0){
        spawnEnemigo();
      }
      fase = 0; //Reinicia fases cuando haya terminado lo anterior
    }
    if(fase == 1 && roba){
      DrawCard();
      roba = false;
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
      pnjAnadido.transform.SetParent(Personajes.transform, false);
      listaPnjEnemigos.Add(pnjAnadido);
    }
  }
private void movEnemigos(){
  for(int i = 0; i < listaPnjEnemigosEnTablero.Count; i++){
    mueveEnemigo(listaPnjEnemigosEnTablero[i]);
  }
}
private void mueveEnemigo(Personaje pnj){
  pnj.setMovAct(pnj.getMaxMov());
  pnj.setNumAtaAct(pnj.getNumAta());
  int posIniX, posIniY;
  if(listaPnj.Count > 0){
    while(pnj.getNumAtaAct() > 0){
      posIniX = pnj.cas.getPosX();
      posIniY = pnj.cas.getPosY();
      int posArr = posIniX+posIniY*8;
      if(pnj.getNumAtaAct() > 0)
        pintaAta(pnj, posArr, pnj.getRang(), pnj.cas);
      pintaCas(pnj, posArr, pnj.getMovAct(), pnj.getRang());
      
      if(listaCasAtacable.Count > 0){
        Personaje enemigoAtacar = listaCasAtacable[0].pnj;
        bool matable = false;
        if(pnj.getAtaque() >= enemigoAtacar.getVida()) matable = true;
        for(int i = 1; i < listaCasAtacable.Count;i++){
          if(cambioObjetivo(pnj, enemigoAtacar, listaCasAtacable[i].pnj, posIniX, posIniY, matable) ){
            enemigoAtacar = listaCasAtacable[i].pnj;
          }
        }
        //Ahora se movería hacia el enemigo seleccionado y le atacaría
        pnj.setNumAtaAct(pnj.getNumAtaAct()-1);
        Casilla cas = enemigoAtacar.cas;
        if(enemigoAtacar.danar(pnj.getAtaque())){
            //Aquí cosas que pasen si se muere el enemigo
            cas.vacia = true;
            cas.pnj = null;
        }
        pnj.transform.SetParent(filasPnj[cas.getCasAnt().fila].transform, false);
        pnj.transform.position = new Vector3(cas.getCasAnt().transform.position.x + 5, cas.getCasAnt().transform.position.y + 35, cas.getCasAnt().transform.position.z+10);
        pnj.setMovAct(pnj.getMovAct()-cas.getCasAnt().getConsumeMov());
        pnj.cas.vacia = true;
        pnj.cas.pnj = null;
        cas.getCasAnt().vacia = false;
        cas.getCasAnt().pnj = pnj;
        pnj.cas = cas.getCasAnt();
      }
      else {
        Personaje enemigoAcercar = listaPnj[0];
        for(int i = 1; i< listaPnj.Count; i++){
          if((listaPnj[i].getVida() <= pnj.getAtaque() && listaPnj[i].getVida() > enemigoAcercar.getVida()) ||  listaPnj[i].getVida() < enemigoAcercar.getVida()){
            enemigoAcercar = listaPnj[i];
          }
        }
        mueveHacia(enemigoAcercar.getCasAct().getPosX(), enemigoAcercar.getCasAct().getPosY(), pnj);
        pnj.setNumAtaAct(0);
      }
      desPintaCas();
    }
  }
  else{
    posIniX = pnj.cas.getPosX();
    posIniY = pnj.cas.getPosY();
    int posArr = posIniX+posIniY*8;
    if(pnj.getNumAtaAct() > 0)
      pintaAta(pnj, posArr, pnj.getRang(), pnj.cas);
    pintaCas(pnj, posArr, pnj.getMovAct(), pnj.getRang());
    mueveHacia(1, 1, pnj);
    desPintaCas();
  }
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
        if(mov > 0){
            if(((posAux = pos+1)%8) != 0 && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov, rang);
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(pnj, posAux, rang, tablero[posAux]);
            }
            if((((posAux = pos-1)+1) %8) != 0 && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov, rang);
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(pnj, posAux, rang, tablero[posAux]);
            }
            if((posAux = pos+8) < tablero.Length && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov, rang);
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(pnj, posAux, rang, tablero[posAux]);
            }
            if((posAux = pos-8) >= 0 && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov, rang);
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(pnj, posAux, rang, tablero[posAux]);
            }
        }
    }
private void pintaAta(Personaje pnj, int pos, int rang, Casilla cas){
    int posAux;
    for(int i = rang; i > 0; i--)
        if(i > 0){
            if(((posAux = pos+i)%8) != i-1 && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(posAux, cas);
            }
            if((((posAux = pos-i)+1) %8) != rang-1 && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(posAux, cas);
            }
            if((posAux = pos+8*i) < tablero.Length && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(posAux, cas);
            }
            if((posAux = pos-8*i) >= 0 && !tablero[posAux].vacia && !tablero[posAux].pintada){
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
    pnj.transform.position = new Vector3(casillaMueve.transform.position.x + 5, casillaMueve.transform.position.y + 35, casillaMueve.transform.position.z+10);
    pnj.setMovAct(pnj.getMovAct()-casillaMueve.getConsumeMov());
  }
}
private void spawnEnemigo(){
  GameObject randEnemigo = listaPnjEnemigos[Random.Range(0, listaPnjEnemigos.Count)];
  listaPnjEnemigosEnTablero.Add(randEnemigo.GetComponent<Personaje>());
  listaPnjEnemigos.Remove(randEnemigo);
  int xCas = Random.Range(0, 48);
  while(!tablero[xCas].vacia || !tablero[xCas].esSpawnEne()){
    xCas = Random.Range(0, 48);
  }
  randEnemigo.transform.SetParent(filasPnj[tablero[xCas].fila].transform, false);
  randEnemigo.transform.position = new Vector3(tablero[xCas].transform.position.x + 5, tablero[xCas].transform.position.y + 35, tablero[xCas].transform.position.z+10);
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
  }
  public void pasaT(){
    pasaTurno = true;
  }
  public void robaCarta(){
    roba = true;
  }
}
