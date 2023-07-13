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
  public List<GameObject> listaPnjEnemigos = new List<GameObject>();
  public TextMeshProUGUI textoFase;

  //Sitema de turnos
  private const int maxRob = 2, maxJug = 2; //Contadores de máximo número de cartas a robar y a jugar
  private static int fase;//Identificador de fase que se usará para entrar o no a hacer cosas, 
  private static int nRobadas = 0, nCartasJugadas = 0; //contador de cartas robadas para no sobrepasar el límite y contador de cartas jugadas para no sobrepasar el límite
  private static bool primerTurno, pasaTurno, roba;

  private static List<string> listaCartas = new List<string>();
  private static List<string> listaEnemigos = new List<string>();
  private static List<Casilla> listaCasMovible = new List<Casilla>();
  private static List<Casilla> listaCasAtacable = new List<Casilla>();


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
  for(int i = 0; i < listaPnjEnemigos.Count; i++){
    mueveEnemigo(listaPnjEnemigos[i].GetComponent<Personaje>());
  }
}
private void mueveEnemigo(Personaje pnj){
  int posIniX = pnj.cas.getPosX();
  int posIniY = pnj.cas.getPosY();
  int posArr = posIniX+posIniY*8;
  pintaCas(pnj, posArr, pnj.getMovAct());
  if(pnj.getNumAtaAct() > 0){
    pintaAta(pnj, posArr, pnj.getRang());
  }

  if(listaCasAtacable.Count > 0){
    Personaje enemigoAtacar = listaCasAtacable[0].pnj;
    bool matable = false;
    if(pnj.getAtaque() >= listaCasAtacable[0].pnj.getVida()) matable = true;
    for(int i = 1; i < listaCasAtacable.Count;i++){
      if(cambioObjetivo(pnj, enemigoAtacar, listaCasAtacable[i].pnj, posIniX, posIniY, matable) ){
        enemigoAtacar = listaCasAtacable[i].pnj;
      }
      //Ahora se movería hacia el enemigo seleccionado y le atacaría
    }
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
private void pintaCas(Personaje pnj, int pos, int mov){
        int posAux;
        if(mov > 0){
            if(((posAux = pos+1)%8) != 0 && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov);
            }
            if((((posAux = pos-1)+1) %8) != 0 && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov);
            }
            if((posAux = pos+8) < tablero.Length && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov);
            }
            if((posAux = pos-8) >= 0 && tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaPintado(pnj, posAux, mov);
            }
        }
    }

    private void pintaAta(Personaje pnj, int pos, int rang){
        int posAux;
        if(rang > 0){
            if(((posAux = pos+1)%8) != 0 && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(pnj, posAux, rang);
            }
            if((((posAux = pos-1)+1) %8) != 0 && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(pnj, posAux, rang);
            }
            if((posAux = pos+8) < tablero.Length && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(pnj, posAux, rang);
            }
            if((posAux = pos-8) >= 0 && !tablero[posAux].vacia && !tablero[posAux].pintada){
                ejecutaAtacable(pnj, posAux, rang);
            }
        }
    }
    private void ejecutaPintado(Personaje pnj, int posAux, int mov){
        if(tablero[posAux].vacia && !tablero[posAux].pintada){
            tablero[posAux].pintada = true;
            tablero[posAux].setConsumeMov(pnj.getMaxMov() - mov + 1);
            listaCasMovible.Add(tablero[posAux]);
            pintaCas(pnj, posAux, mov-1);
        }
    }

    private void ejecutaAtacable(Personaje pnj, int posAux, int rang){
        if(!tablero[posAux].vacia && !tablero[posAux].pintada && !tablero[posAux].pnj.enemigo){
            tablero[posAux].pintada = true;
            listaCasAtacable.Add(tablero[posAux]);
            pintaCas(pnj, posAux, rang-1);
        }
    }
    private void desPintaCas(){
        for(int i = 0; i < tablero.Length;i++){
            tablero[i].pintada = false;
        }
        listaCasMovible.Clear();
        listaCasAtacable.Clear();
    }
private void spawnEnemigo(){
  GameObject randEnemigo = listaPnjEnemigos[Random.Range(0, listaPnjEnemigos.Count)];
  listaPnjEnemigos.Remove(randEnemigo);
  int xCas = Random.Range(0, tamTab);
  while(!tablero[24 + xCas].vacia){
    xCas = Random.Range(0, tamTab);
  }
  
  randEnemigo.transform.position = tablero[24 + xCas].transform.position;
  randEnemigo.gameObject.GetComponent<Personaje>().setCasAct(tablero[24 + xCas]);
  tablero[24 + xCas].pnj = randEnemigo.GetComponent<Personaje>();
  tablero[24 + xCas].vacia = false;
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
