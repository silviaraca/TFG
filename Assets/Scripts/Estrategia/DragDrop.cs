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
            if(card.esPersonaje() && cas.esSpawnAli()){ //Cartas de personaje, ahora mismo hace que no funcione porque no hay datos iniciales en cada carta
                if(sobreCasilla && (cas.vacia && card.enMano) && gm.getCarJugadas() < 2){ 
                    GameObject personajeCreado = Instantiate(personajePrefab);
                    //Cuando se haga una constructora se tiene que pasar los datos desde la carta al pnj
                    personajeCreado.transform.SetParent(gm.Personajes.transform, false);
                    personajeCreado.transform.position = cas.transform.position; 
                    personajeCreado.gameObject.GetComponent<Personaje>().setCasAct(cas);
                    setCharacterValues(personajeCreado.GetComponent<Personaje>());
                    card.transform.position = gm.zonaDescarte.transform.position;
                    cas.pnj = personajeCreado.GetComponent<Personaje>();
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
                        //Correr posición de las cartas de la mano para dejar hueco
                        if(card.enMano){
                            while(i+1 < gm.espacioMano.Length && !gm.espacioManoSinUsar[i+1]){
                                gm.mano[i+1].transform.position = gm.espacioMano[i].position;
                                gm.mano[i] = gm.mano[i+1];
                                gm.mano[i].handIndex = i;
                                i++;
                            }
                            card.enMano = false;
                            card.setCasAct(cas);
                            gm.descarte.Add(card);
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
                        //Añadir a una lista de descartes
                        //---------------
                        int posAux, areaAux;
                        areaAux = card.getAreaHechizo();
                        //Una vez soltada la carta de hechizo en area hace si efecto en todas las casillas ocupadas por algún personaje
                        if(!cas.vacia)
                            card.efectoHechizo(cas.pnj);
                        int pos = cas.getPosX()+cas.getPosY()*8;
                        while(areaAux > 0){
                            if(((posAux = pos+1)%8) != 0 && !gm.tablero[posAux].vacia){
                                card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            if((((posAux = pos-1)+1) %8) != 0 && !gm.tablero[posAux].vacia){
                               card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            if((posAux = pos+8) < gm.tablero.Length && !gm.tablero[posAux].vacia){
                                card.efectoHechizo(gm.tablero[posAux].pnj);
                            }
                            if((posAux = pos-8) >= 0 && !gm.tablero[posAux].vacia){
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
                            gm.descarte.Add(card);
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
            else{
                card.transform.position = posIni;
            }
        }
    }
    void Update()
    {
        if(enMovimiento){
            card.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 200);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla") && !card.zoom){ 
            //Tiene que haber también uno de estos por cartas de hechizo y demas porque funcionan distinto, no colorean igual ni de verde
            if(cas.vacia && card.esPersonaje() && cas.esSpawnAli()) pintaAzul(cas);
            else if(card.esHechizoArea()) pintaArea();
            else if(card.esHechizoUnico() && !cas.vacia){
                if(card.esHechizoAtaque() && cas.pnj.enemigo) pintaRojo(cas);
                else if(card.esHechizoDefensa() && !cas.pnj.enemigo) pintaVerde(cas);
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
            if(card.esPersonaje() || card.esHechizoUnico())
                cas2.gameObject.GetComponent<Image>().color = cas2.getColIni();
            else if(card.esHechizoArea() && cas2 != null){
                limpiaArea(cas2);
            }
            if(nuevaCas) nuevaCas = false;
            else sobreCasilla = false;
        }
   }

    private void pintaArea(){//Pinta el área de acción de un hechizo de área según qué objetivo haya (En vez de pintar en un futuro cambiará colores de tiles)
        int posAux, areaAux;
        areaAux = card.getAreaHechizo();
        pintaCasillaHechizo(cas);
        int pos = cas.getPosX()+cas.getPosY()*8;
        while(areaAux > 0){
            if(((posAux = pos+1)%8) != 0){
                pintaCasillaHechizo(gm.tablero[posAux]);
            }
            if((((posAux = pos-1)+1) %8) != 0){
                pintaCasillaHechizo(gm.tablero[posAux]);
            }
            if((posAux = pos+8) < gm.tablero.Length){
                pintaCasillaHechizo(gm.tablero[posAux]);
            }
            if((posAux = pos-8) >= 0){
                pintaCasillaHechizo(gm.tablero[posAux]);
            }
            areaAux--;
        }
    }
    private void limpiaArea(Casilla casAux){//Devuelve a la forma original las casillas de un área
        int posAux;
        int areaAux = card.getAreaHechizo();
        casAux.gameObject.GetComponent<Image>().color = casAux.getColIni();
        int pos = casAux.getPosX()+cas.getPosY()*8;
        while(areaAux > 0){
            if(((posAux = pos+1)%8) != 0){
                gm.tablero[posAux].gameObject.GetComponent<Image>().color = gm.tablero[posAux].getColIni();
            }
            if((((posAux = pos-1)+1) %8) != 0){
                gm.tablero[posAux].gameObject.GetComponent<Image>().color = gm.tablero[posAux].getColIni();
            }
            if((posAux = pos+8) < gm.tablero.Length){
                gm.tablero[posAux].gameObject.GetComponent<Image>().color = gm.tablero[posAux].getColIni();
            }
            if((posAux = pos-8) >= 0){
                gm.tablero[posAux].gameObject.GetComponent<Image>().color = gm.tablero[posAux].getColIni();
            }
            areaAux--;
        }
    }
    private void pintaRojo(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().color = new Color32(200, 0, 0, 100);
    }
    private void pintaVerde(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().color = new Color32(0, 200, 0, 100);
    }
    private void pintaAzul(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().color = new Color32(0, 0, 200, 100);
    }

    private void pintaCasillaHechizo(Casilla casilla){
        if(casilla.vacia){
            pintaAzul(casilla);
        }
        else if(!casilla.vacia && casilla.pnj.enemigo){
            pintaRojo(casilla);
        }
        else if(!casilla.vacia && !casilla.pnj.enemigo){
            pintaVerde(casilla);
        }
    }

    private void setCharacterValues(Personaje pnj){
        pnj.setAtaque(card.getAtaque());
        pnj.setMovMax(card.getMovMax());
        pnj.setNumAta(card.getNumAta());
        pnj.setRang(card.getRango());
        pnj.setVidaMax(card.getVidaMax());
        pnj.setCarta(card);
    }
}