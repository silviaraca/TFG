using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropCard : MonoBehaviour
{
    public bool sobreCasilla;
    public Carta card;
    private Casilla cas;
    private bool nuevaCas = false;
    public GameObject personajePrefab;
    private GameManagerE gm;


    void Start()
    {
        gm = FindObjectOfType<GameManagerE>();
    }
    public void sueltaCarta(){
        if(gm.getFase() == 2 || gm.getFase() == 4){
            if (cas != null && card.esPersonaje() && (cas.esSpawnAli() || cas.esSpawnAliTemp())){ //Cartas de personaje, ahora mismo hace que no funcione porque no hay datos iniciales en cada carta
                if(sobreCasilla && (cas.vacia && card.enMano) && gm.getCarJugadas() < 2){ 
                    GameObject personajeCreado = Instantiate(personajePrefab);
                    //Cuando se haga una constructora se tiene que pasar los datos desde la carta al pnj
                    personajeCreado.transform.SetParent(gm.filasPnj[cas.fila].transform, false);
                    personajeCreado.transform.position = new Vector3(cas.transform.position.x, cas.transform.position.y + 35, cas.transform.position.z+10);
                    personajeCreado.gameObject.GetComponent<Personaje>().setCasAct(cas);
                    setCharacterValues(personajeCreado.GetComponent<Personaje>());
                    card.transform.position = gm.zonaDescarte.transform.position;
                    cas.pnj = personajeCreado.GetComponent<Personaje>();
                    if(cas.pnj.spawner){
                        int posIniX = cas.getPosX();
                        int posIniY = cas.getPosY();
                        int posArr = posIniX+posIniY*8;
                        limpiaSpawn();
                        creaSpawn(posArr);
                    }
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
                }
                else if(card.esHechizoArea() && !gm.tutorial){ //Cartas de hechizo en area sin necesidad de objetivo
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
                }
                else if(card.esHechizoArea() && gm.tutorial){ //Cartas de hechizo en area sin necesidad de objetivo
                    if(sobreCasilla && gm.getCarJugadas() < 2 && cas.muyPintada){
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
                        cas.setColor(gm.casEstandar);
                        gm.setMata();
                    }
                }
            }
            card.GetComponent<DragDrop>().stopMov();
            gm.puntero.transform.position = new Vector3(-10000, -10000, 0);
        }
        card.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla") && !card.zoom){ 
            //Tiene que haber también uno de estos por cartas de hechizo y demas porque funcionan distinto, no colorean igual ni de verde
            if(cas.vacia && card.esPersonaje() && (cas.esSpawnAli() || cas.esSpawnAliTemp())) pintaAzul(cas);
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
                cas2.gameObject.GetComponent<Image>().sprite = cas2.getImagenIni();
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
        casAux.gameObject.GetComponent<Image>().sprite = casAux.getImagenIni();
        int pos = casAux.getPosX()+cas.getPosY()*8;
        while(areaAux > 0){
            if(((posAux = pos+1)%8) != 0){
                gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = gm.tablero[posAux].getImagenIni();
            }
            if((((posAux = pos-1)+1) %8) != 0){
                gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = gm.tablero[posAux].getImagenIni();
            }
            if((posAux = pos+8) < gm.tablero.Length){
                gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = gm.tablero[posAux].getImagenIni();
            }
            if((posAux = pos-8) >= 0){
                gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = gm.tablero[posAux].getImagenIni();
            }
            areaAux--;
        }
    }
    private void pintaRojo(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().sprite = gm.casAta;
    }
    private void pintaVerde(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().sprite = gm.casAli;
    }
    private void pintaAzul(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().sprite = gm.casVacia;
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
    private void limpiaSpawn(){
        for(int i = 0; i < gm.tablero.Length; i++){
            gm.tablero[i].quitaSpawnAliTem();
        }
    }
    private void creaSpawn(int index){
        if((index + 1)%8 != 0)
            gm.tablero[index + 1].poneSpawnAliTem();
        if((index)%8 != 0)
            gm.tablero[index - 1].poneSpawnAliTem();
        if((index + 8) < gm.tablero.Length)
            gm.tablero[index + 8].poneSpawnAliTem();
        if((index - 8) >= 0)
            gm.tablero[index - 8].poneSpawnAliTem();
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