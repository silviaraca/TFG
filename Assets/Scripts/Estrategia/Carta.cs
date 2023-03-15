using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public bool hasBeenPlayed;
    public bool enMano;
    public int handIndex;
    private Casilla casAct;

    public Casilla getCasAct(){
        return casAct;
    }

    public void setCasAct(Casilla cas){
        casAct = cas;
    }
    
}
