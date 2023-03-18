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
  public Casilla[] casillas;
  public bool[] espacioManoSinUsar;

  public List<Carta> mano = new List<Carta>();

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
}
