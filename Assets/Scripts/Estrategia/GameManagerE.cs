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
  public GameObject Canvas, Cards;
  public GameObject cardAldeano, cardAgua, cardEstaca; //Objetos de las cartas que existen ya, añadir conforme 
  public Transform[] espacioMano;
  public Casilla[] tablero;
  public int tamTab;
  public bool[] espacioManoSinUsar;
  public List<Carta> mano = new List<Carta>();
  public List<Personaje> listaPnj = new List<Personaje>();
  public TextMeshProUGUI textoFase;

  //Sitema de turnos
  private const int maxRob = 2, maxJug = 2; //Contadores de máximo número de cartas a robar y a jugar
  private static int fase;//Identificador de fase que se usará para entrar o no a hacer cosas, 
  private static int nRobadas = 0, nCartasJugadas = 0; //contador de cartas robadas para no sobrepasar el límite y contador de cartas jugadas para no sobrepasar el límite
  private static bool primerTurno, pasaTurno, roba;

  private static List<string> listaCartas = new List<string>();


public void Start(){
  //Esto será tomar las cartas de la lista de cartas que haya en otro lado al iniciar la escena
  listaCartas.Add("Aldeano");
  listaCartas.Add("Aldeano");
  listaCartas.Add("Agua");
  listaCartas.Add("Agua");
  listaCartas.Add("Estaca");
  listaCartas.Add("Estaca");
  pasaTurno = false;
  roba = false;
  primerTurno = true;
  fase = 0;

  creaCartas();
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

  public void Update(){
    if(fase == 0){ //fase inicial y efectos de inicio de turno
      if(primerTurno){
        DrawCard();
        DrawCard();
        primerTurno = false;
      }
      //efectos de inicio de turno
      nRobadas = 0;
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
