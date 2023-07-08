using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropPnj : MonoBehaviour
{
    private bool enMovimiento = false;
    private Vector2 posIni;
    public bool sobreCasilla;
    public Personaje pnj; 
    private bool cerrojo = true;
    private Casilla cas;
    private bool nuevaCas = false;
    private GameManagerEstrategia gm;

    void Start()
    {
        gm = FindObjectOfType<GameManagerEstrategia>();
        posIni = transform.position;
    }

    public void cogePnj(){
        if(gm.getFase() == 3 && !pnj.enemigo){
            enMovimiento = true;
            if(cerrojo)
                posIni = transform.position;
            cerrojo = false;
            movible();
        }
    }

    public void sueltaPnj(){
        if(gm.getFase() == 3 && !pnj.enemigo){
            enMovimiento = false;
            cerrojo = true;
            if(sobreCasilla && cas.vacia && cas.pintada){ 
                Casilla casAct = pnj.getCasAct();
                casAct.vacia = true;
                pnj.transform.position = cas.transform.position;
                pnj.setMov(pnj.getMovAct()-cas.getConsumeMov());
                cas.pnj = pnj;
                cas.vacia = false;
                pnj.setCasAct(cas);
            }
            else if(sobreCasilla && !cas.vacia && cas.pintada){
                Casilla casAct = pnj.getCasAct();
                Personaje pnjAct = cas.pnj;
                if(pnjAct.danar(pnj.getAtaque())){
                    //Aquí cosas que pasen si se muere el enemigo

                    //casAct.vacia = true;
                    //Destroy(pnjAct.gameObject);
                    //pnj.transform.position = cas.transform.position;
                    //cas.pnj = pnj;
                    //pnj.setCasAct(cas);
                    pnj.transform.position = posIni;
                }
                else{
                    pnj.transform.position = posIni;
                }
            }
            else {
                pnj.transform.position = posIni;
            }
            desPintaCas();
        }
    }
    void Update()
    {
        if(enMovimiento){
            pnj.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }
    public void movible(){
        int posXAct = pnj.getCasAct().getPosX();
        int posYAct = pnj.getCasAct().getPosY();
        int posArr = posXAct+posYAct*8;
        pintaCas(posArr, pnj.getMovAct());
        pintaAta(posArr, pnj.getRang());
    }

    private void pintaCas(int pos, int mov){
        int posAux;
        if(mov > 0){
            if(((posAux = pos+1)%8) != 0 && gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaPintado(posAux, mov);
            }
            if((((posAux = pos-1)+1) %8) != 0 && gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaPintado(posAux, mov);
            }
            if((posAux = pos+8) < gm.tablero.Length && gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaPintado(posAux, mov);
            }
            if((posAux = pos-8) >= 0 && gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaPintado(posAux, mov);
            }
        }
    }

    private void pintaAta(int pos, int rang){
        int posAux;
        if(rang > 0){
            if(((posAux = pos+1)%8) != 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaAtacable(posAux, rang);
            }
            if((((posAux = pos-1)+1) %8) != 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaAtacable(posAux, rang);
            }
            if((posAux = pos+8) < gm.tablero.Length && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaAtacable(posAux, rang);
            }
            if((posAux = pos-8) >= 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                ejecutaAtacable(posAux, rang);
            }
        }
    }

    private void ejecutaPintado(int posAux, int mov){
        if(gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
            gm.tablero[posAux].gameObject.GetComponent<Image>().color = new Color32(0, 200, 0, 100);
            gm.tablero[posAux].pintada = true;
            gm.tablero[posAux].setConsumeMov(pnj.getMaxMov() - mov + 1);
            pintaCas(posAux,mov-1);
        }
    }

    private void ejecutaAtacable(int posAux, int rang){
        if(!gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada && gm.tablero[posAux].pnj.enemigo){
            gm.tablero[posAux].gameObject.GetComponent<Image>().color = new Color32(200, 0, 0, 100);
            gm.tablero[posAux].pintada = true;
            pintaCas(posAux, rang-1);
        }
    }

    private void desPintaCas(){
        for(int i = 0; i < gm.tablero.Length;i++){
            gm.tablero[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gm.tablero[i].pintada = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla")){ 
            if(sobreCasilla) nuevaCas = true;
            sobreCasilla = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla")){
            if(nuevaCas) nuevaCas = false;
            else sobreCasilla = false;
        }
    }
}
