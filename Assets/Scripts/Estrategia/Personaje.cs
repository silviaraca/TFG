using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personaje : MonoBehaviour
{
    //todo esto debe poder cambiarse dependiendo del personaje que se juega por lo que debe haber algún tipo de función constructora
    [SerializeField] private int vidaMax, ataque, movMax, numAtaques, rango;
    private int movAct, vida, numAtaAct;
    public bool enemigo, enRango, transformando;
    public Casilla cas;
    private bool ini = true;
    private GameManagerE gm;
    private Carta card;
    public int turnoEfectoFin = 0;
    public string efecto;
    private int contTransform = -1;


    void Start(){
        gm = FindObjectOfType<GameManagerE>();
        vida = vidaMax;
        movAct = movMax;
        numAtaAct = numAtaques;
        if(vidaMax > 0 && ini){
            this.transform.GetComponentInChildren<HelthBar>().iniVida();
            ini = false;
        }
    }
    public bool danar(int dano){
        vida -= dano;
        if(dano >= 0){
            if(vida < 0) vida = 0;
            if(vida > 0)
                this.transform.GetComponentInChildren<HelthBar>().pierdeVida(vida, vidaMax);
        }
        else{
            if(vida > vidaMax) vida = vidaMax;
            this.transform.GetComponentInChildren<HelthBar>().ganaVida(vida, vidaMax);
        }
        return muerto();
    }
    public void paralizar(){
        movAct = 0;
        numAtaAct = 0;
    }

    public void subeAtaque(int a){
        ataque += a;
    }

    public void transformaPnj(int t){
        contTransform = t;
        transformando = true;
    }

    private bool muerto(){
        if(vida == 0){ 
            if(enemigo){
                gm.listaPnjEnemigosEnTablero.Remove(this);
                gm.enemigosVivos--;
            }
            else{
                gm.listaPnj.Remove(this);
            }
            if(!enemigo)
                gm.descarte.Add(card);
            cas.vacia = true;
            cas.pnj = null;
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    
    public void activaEfectoFin(){
        if(efecto == "CuraArea"){
            int posAux;
            int pos = cas.getPosX()+cas.getPosY()*8;
            if(((posAux = pos+1)%8) != 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pnj.enemigo){
                gm.tablero[posAux].pnj.danar(-1);
            }
            if((((posAux = pos-1)+1) %8) != 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pnj.enemigo){
            gm.tablero[posAux].pnj.danar(-1);
            }
            if((posAux = pos+8) < gm.tablero.Length && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pnj.enemigo){
                gm.tablero[posAux].pnj.danar(-1);
            }
            if((posAux = pos-8) >= 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pnj.enemigo){
                gm.tablero[posAux].pnj.danar(-1);
            }
        }
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
    public void setCarta(Carta c){
        card = c;
    }
    public Carta getCarta(){
        return card;
    }
    public int getContTrans(){
        return contTransform;
    }
    public void setContTrans(int c){
        contTransform = c;
    }
}
