using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //todo esto debe poder cambiarse dependiendo del personaje que se juega por lo que debe haber algún tipo de función constructora
    private int vida = 2;
    private int ataque = 1;
    private const int movMax = 2;
    private int movAct = 2;
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


    public int getMaxMov(){
        return movMax;
    }
    public int getMovAct(){
        return movAct;
    }
    public void setMov(int m){
        movAct = m;
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
