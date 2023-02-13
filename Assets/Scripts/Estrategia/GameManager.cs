using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public List<Carta> deck = new List<Carta>();
  public Transform[] cardSlots;
  public casilla[] casillas;
  public bool[] availableCardSlots;

  public void DrawCard(){
    if(deck.Count >= 1){
        Carta randCard = deck[Random.Range(0,deck.Count)];

        for(int i = 0; i < availableCardSlots.Length; i++){
            if(availableCardSlots[i] == true){
                randCard.gameObject.SetActive(true);
                randCard.handIndex = i;
                randCard.transform.position = cardSlots[i].position;
                availableCardSlots[i] = false;
                deck.Remove(randCard);
                return;
            }
        }
    }
  }

  public Vector3 selecCasilla(){
    while(true){
      for(int i = 0; i < casillas.Length; i++){
        if(casillas[i].OnMouseDown()){
          return casillas[i].transform.position;
        }
      }
    }
  }
}
