using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public List<Carta> mazo = new List<Carta>();
  public List<Carta> descarte = new List<Carta>();
  public GameObject zonaDescarte;
  public GameObject Canvas;
  public Transform[] espacioMano;
  public Casilla[] tablero;
  public int tamTab;
  public bool[] espacioManoSinUsar;
  public List<Carta> mano = new List<Carta>();

  //Sitema de turnos
  private const int maxRob = 2, maxJug = 2; //Contadores de máximo número de cartas a robar y a jugar
  private int fase = 0, nRobadas = 0, nCartasJugadas = 0; //Identificador de fase que se usará para entrar o no a hacer cosas, 
                                                          //contador de cartas robadas para no sobrepasar el límite, 
                                                          //contador de cartas jugadas para no sobrepasar el límite
  private bool primerTurno = true, pasaTurno = false;

  public void DrawCard(){
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

  public void Update(){
    if(fase == 0){ //fase inicial y efectos de inicio de turno
      if(primerTurno){
        DrawCard();
        DrawCard();
        primerTurno = false;
      }
      //efectos de inicio de turno
      fase++; //No pasa hasta que se ejecuten todos
    }
    else if(fase == 1 && (nRobadas < 2 || pasaTurno)){ //Fase1 de robo de los mazos
      fase++;
      nRobadas = 0;
      pasaTurno = false;
    }
    else if(fase > 1 && fase < 5 && pasaTurno){ //Fase2 de juegar cartas1, Fase3 de mover pnj, Fase4 de jugar cartas2
      fase++;
      pasaTurno = false;
      nCartasJugadas = 0;
    }
    else if(fase == 5){
      //efectos de final de turno y movimientos de enemigos
      fase = 0; //Reinicia fases cuando haya terminado lo anterior
    }
  }
}
