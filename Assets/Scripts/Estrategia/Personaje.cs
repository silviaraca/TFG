using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //todo esto debe poder cambiarse dependiendo del personaje que se juega por lo que debe haber algún tipo de función constructora
    [SerializeField] private int vidaMax, ataque, movMax, numAtaques, rango;
    private int movAct, vida, numAtaAct;
    public bool enemigo, enRango;
    public Casilla cas;


    void Start(){
        vida = vidaMax;
        movAct = movMax;
        numAtaAct = numAtaques;
        this.transform.GetComponentInChildren<HelthBar>().iniVida();

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
    public void setMovAct(int m){
        movAct = m;
    }
    public void setNumAtaAct(int a){
        numAtaAct = a;
    }
    public int getNumAtaAct(){
        return numAtaAct;
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
        numAtaAct = a;
    } 
    public int getVida(){
        return vida;
    }
    public void setVidaMax(int v){
        vidaMax = v;
        vida = v;
    }
    public void setMovMax(int m){
        movMax = m;
        movAct = m;
        this.transform.GetComponentInChildren<HelthBar>().iniVida();
    }
}
