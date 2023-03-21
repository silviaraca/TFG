using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    private int vida = 2;
    private int ataque = 1;

    private int movMax = 2;

    public int movAct = 2;
    public bool enemigo = false;
    private Casilla cas;


    public bool danar(int dano){
        vida -= dano;
        return muerto();
    }

    private bool muerto(){
        if(vida <= 0){            
            cas.pnj = null;
            Destroy(this);
            return true;
        }
        return false;
    }   

    public Casilla getCasAct(){
        return cas;
    }

    public void setCasAct(Casilla casAct){
        cas = casAct;
    }

    public void setAtaque(int at){
        ataque = at;
    }

    public int getAtaque(){
        return ataque;
    }
}
