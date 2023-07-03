using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //todo esto debe poder cambiarse dependiendo del personaje que se juega por lo que debe haber algún tipo de función constructora
    private int vida = 2;
    private int vidaMax = 2;
    private int ataque = 1;
    private const int movMax = 2;
    private int movAct = 2;
    private int numAtaques = 1;

    private int rango = 1;
    public bool enemigo, enRango;
    public Casilla cas;


    void Start(){
    }
    public bool danar(int dano){
        vida -= dano;
        if(vida > vidaMax) vida = vidaMax;
        this.transform.GetComponentInChildren<HelthBar>().pierdeVida(vida);
        return muerto();
    }

    private bool muerto(){
        if(vida <= 0){     
            cas.vacia = true;
            cas.pnj = null;
            Destroy(this.gameObject);
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
    public int getRang(){
        return rango;
    }
    public void setRang(int r){
        rango = r;
    } 
    public int getNumAta(){
        return numAtaques;
    }
    public void setNumAta(int a){
        numAtaques = a;
    } 
    public int getVida(){
        return vida;
    }
}
