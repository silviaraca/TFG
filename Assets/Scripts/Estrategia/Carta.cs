using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public bool hasBeenPlayed;

    public int handIndex;
    
    /*public GameManager gm;

    public void Start(){
        gm = FindObjectOfType<GameManager>();
    }
    
    private void OnMouseDown(){
        if(!hasBeenPlayed){
            hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;
        }
    }*/
}
