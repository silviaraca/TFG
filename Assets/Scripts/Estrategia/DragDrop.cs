using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    private bool enMovimiento = false;
    private Vector2 posIni;
    public bool sobreCasilla;
    public Carta card; 
    private bool cerrojo = true;
    private Casilla cas;
    private bool nuevaCas = false;
    public GameObject personajePrefab;
    private GameManagerE gm;

    void Start()
    {
        gm = FindObjectOfType<GameManagerE>();
        posIni = transform.position;
    }

    public void cogeCarta(){
        if(gm.getFase() == 2 || gm.getFase() == 4){
            enMovimiento = true;
            if(cerrojo)
                posIni = transform.position;
            cerrojo = false;
        }
    }

    public void sueltaCarta(){
        if(gm.getFase() == 2 || gm.getFase() == 4){
            enMovimiento = false;
            cerrojo = true;
            if(card.esPersonaje()){ //Cartas de personaje, ahora mismo hace que no funcione porque no hay datos iniciales en cada carta
                if(sobreCasilla && (cas.vacia && card.enMano) && gm.getCarJugadas() < 2){ 
                    GameObject a = Instantiate(personajePrefab);
                    //Cuando se haga una constructora se tiene que pasar los datos desde la carta al pnj
                    a.transform.SetParent(gm.Canvas.transform, false);
                    a.transform.position = cas.transform.position; 
                    a.gameObject.GetComponent<Personaje>().setCasAct(cas);
                    card.transform.position = gm.zonaDescarte.transform.position;
                    cas.pnj = a.GetComponent<Personaje>();
                    gm.listaPnj.Add(cas.pnj);
                    cas.vacia = false;
                    int i = card.handIndex;
                    if(card.enMano){
                        while(i+1 < gm.espacioMano.Length && !gm.espacioManoSinUsar[i+1]){
                            gm.mano[i+1].transform.position = gm.espacioMano[i].position;
                            gm.mano[i] = gm.mano[i+1];
                            gm.mano[i].handIndex = i;
                            i++;
                        }
                        card.enMano = false;
                        card.setCasAct(cas);
                        gm.espacioManoSinUsar[i] = true;                
                        gm.mano.Remove(gm.mano[i]);//***
                    }
                    gm.setCarJugadas(gm.getCarJugadas() + 1);
                }
                else{
                    card.transform.position = posIni;
                }
            }
            else if(!card.esPersonaje() && card.esHechizoTablero()){
                if(card.esHechizoUnico()){ //Cartas de hechizo que tienen que lanzarse sobre un personaje
                    if(sobreCasilla && !cas.vacia && gm.getCarJugadas() < 2 && ((card.esHechizoAtaque() && cas.pnj.enemigo) || (card.esHechizoDefensa() && !cas.pnj.enemigo))){
                        card.transform.position = gm.zonaDescarte.transform.position;
                        card.efectoHechizo(cas.pnj);
                        int i = card.handIndex;
                        if(card.enMano){
                            while(i+1 < gm.espacioMano.Length && !gm.espacioManoSinUsar[i+1]){
                                gm.mano[i+1].transform.position = gm.espacioMano[i].position;
                                gm.mano[i] = gm.mano[i+1];
                                gm.mano[i].handIndex = i;
                                i++;
                            }
                            card.enMano = false;
                            card.setCasAct(cas);
                            gm.espacioManoSinUsar[i] = true;                
                            gm.mano.Remove(gm.mano[i]);//***
                        }
                        gm.setCarJugadas(gm.getCarJugadas() + 1);
                    }
                    else{
                        card.transform.position = posIni;
                    }
                }
                else if(card.esHechizoArea()){ //Cartas de hechizo en area sin necesidad de objetivo
                    if(sobreCasilla && gm.getCarJugadas() < 2){
                        card.transform.position = gm.zonaDescarte.transform.position;
                        int posAux, areaAux;
                        areaAux = card.getAreaHechizo();
                        card.efectoHechizo(cas.pnj);
                        int pos = cas.getPosX()+cas.getPosY()*8;
                        while(areaAux > 0){
                            if(((posAux = pos+1)%8) != 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                                card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            if((((posAux = pos-1)+1) %8) != 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                               card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            if((posAux = pos+8) < gm.tablero.Length && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                                card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            if((posAux = pos-8) >= 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                                card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            areaAux--;
                        }                        
                        int i = card.handIndex;
                        if(card.enMano){
                            while(i+1 < gm.espacioMano.Length && !gm.espacioManoSinUsar[i+1]){
                                gm.mano[i+1].transform.position = gm.espacioMano[i].position;
                                gm.mano[i] = gm.mano[i+1];
                                gm.mano[i].handIndex = i;
                                i++;
                            }
                            card.enMano = false;
                            card.setCasAct(cas);
                            gm.espacioManoSinUsar[i] = true;                
                            gm.mano.Remove(gm.mano[i]);//***
                        }
                        gm.setCarJugadas(gm.getCarJugadas() + 1);
                    }
                    else{
                        card.transform.position = posIni;
                    }
                }
            }
        }
    }
    void Update()
    {
        if(enMovimiento){
            card.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Casilla casAux = cas;
        cas = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla")){ 
            //Tiene que haber también uno de estos por cartas de hechizo y demas porque funcionan distinto, no colorean igual ni de verde
            if(cas.vacia)
                cas.gameObject.GetComponent<Image>().color = new Color32(0, 200, 0, 100);
                if(casAux != cas){
                    casAux.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            if(sobreCasilla) nuevaCas = true;
            sobreCasilla = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Casilla cas2 = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla")){
            cas2.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if(nuevaCas) nuevaCas = false;
            else sobreCasilla = false;
        }
   }

   private void ejecutaPintado(int posAux){
        if(gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
            gm.tablero[posAux].gameObject.GetComponent<Image>().color = new Color32(0, 200, 0, 100);
            gm.tablero[posAux].pintada = true;
        }
    }
}