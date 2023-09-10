using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropPnj : MonoBehaviour
{
    private bool enMovimiento = false;
    private Vector2 posIni;
    private int filaIni;
    public bool sobreCasilla;
    public Personaje pnj; 
    private bool cerrojo = true;
    private Casilla cas;
    private bool nuevaCas = false;
    private GameManagerE gm;
    [SerializeField] private Sprite casAta;
    [SerializeField] private Sprite casMov;

    void Start()
    {
        gm = FindObjectOfType<GameManagerE>();
        posIni = transform.position;
    }

    public void cogePnj(){
        if(gm.getFase() == 3 && !pnj.enemigo){ 
            enMovimiento = true;
            if(cerrojo){
                posIni = transform.position;
                filaIni = cas.fila;
            }
            cerrojo = false;
            movible();
        }
    }

    public void sueltaPnj(){
        if(gm.getFase() == 3 && !pnj.enemigo){ //Permite jugar personajes solo en fase 3
            enMovimiento = false;
            cerrojo = true;
            if(!gm.tutorial){
                if(sobreCasilla && cas.vacia && cas.pintada){  //Mover sobre una casilla vacía
                    Casilla casAct = pnj.getCasAct();
                    casAct.vacia = true;
                    pnj.transform.SetParent(gm.filasPnj[cas.fila].transform, false); //Mueve personaje a la casilla
                    pnj.transform.position = new Vector3(cas.transform.position.x, cas.transform.position.y + 40, cas.transform.position.z+10);
                    pnj.setMovAct(pnj.getMovAct()-cas.getConsumeMov());
                    cas.pnj = pnj;
                    cas.vacia = false;
                    pnj.setCasAct(cas);
                    if(pnj.spawner){ //Mueve las zonas de spawn si es un personaje spawner
                        int posIniX = pnj.cas.getPosX();
                        int posIniY = pnj.cas.getPosY();
                        int posArr = posIniX+posIniY*8;
                        limpiaSpawn(); //Quita el spawn anterior
                        creaSpawn(posArr); //Crea el nuevo spawn
                    }
                }
                else if(sobreCasilla && !cas.vacia && cas.pintada && (!cas.pnj.dracula || gm.hayCarreta())){ //Si ataca a un enemigo o cura a un aliado
                    Casilla casAct = pnj.getCasAct();
                    Personaje pnjAct = cas.pnj;
                    pnj.setNumAtaAct(pnj.getNumAtaAct()-1);
                    if(pnj.vampire){
                        pnj.danar(-1, "");
                    }
                    if(pnj.envenena){
                        pnjAct.envenenarPnj(pnj.turnosMeteVeneno, pnj.danoMeteVeneno);
                    }
                    if(pnj.healer){ //Si es sanador cura al objetivo
                        pnjAct.danar(-pnj.getAtaque(), "");
                    }
                    else{
                        if(pnjAct.danar(pnj.getAtaque(), "")){ //Si mata
                            //Aquí cosas que pasen si se muere el enemigo
                            cas.vacia = true;
                            cas.pnj = null;
                            if(pnj.rage){ //Si tiene rage recupera un ataque para popder seguir atacando
                                pnj.setNumAtaAct(pnj.getNumAtaAct()+1);
                            }
                        }
                    }
                    pnj.transform.SetParent(gm.filasPnj[cas.getCasAnt().fila].transform, false); //mueve hasta la zona
                    pnj.transform.position = new Vector3(cas.getCasAnt().transform.position.x, cas.getCasAnt().transform.position.y + 40, cas.getCasAnt().transform.position.z+10);
                    pnj.setMovAct(pnj.getMovAct()-cas.getCasAnt().getConsumeMov());
                    pnj.cas.vacia = true;
                    pnj.cas.pnj = null;
                    cas.getCasAnt().vacia = false;
                    cas.getCasAnt().pnj = pnj;
                    pnj.cas = cas.getCasAnt();
                    if(pnj.spawner){
                        int posIniX = pnj.cas.getPosX();
                        int posIniY = pnj.cas.getPosY();
                        int posArr = posIniX+posIniY*8;
                        limpiaSpawn();
                        creaSpawn(posArr);
                    }
                }
                else {
                    pnj.transform.SetParent(gm.filasPnj[filaIni].transform, false);
                    pnj.transform.position = posIni;
                }
                desPintaCas();
            }
            else{
                if(sobreCasilla && !cas.vacia && cas.pintada){
                    Casilla casAct = pnj.getCasAct();
                    Personaje pnjAct = cas.pnj;
                    pnj.setNumAtaAct(pnj.getNumAtaAct()-1);
                    if(pnj.vampire){
                        pnj.danar(-1, "");
                    }
                    if(pnj.envenena){
                        pnjAct.envenenarPnj(pnj.turnosMeteVeneno, pnj.danoMeteVeneno);
                    }
                    if(pnjAct.danar(pnj.getAtaque(), "")){
                        //Aquí cosas que pasen si se muere el enemigo
                        cas.vacia = true;
                        cas.pnj = null;
                        if(pnj.rage){
                            pnj.setNumAtaAct(pnj.getNumAtaAct()+1);
                        }
                    }
                    pnj.transform.SetParent(gm.filasPnj[cas.getCasAnt().fila].transform, false);
                    pnj.transform.position = new Vector3(cas.getCasAnt().transform.position.x, cas.getCasAnt().transform.position.y + 40, cas.getCasAnt().transform.position.z+10);
                    pnj.setMovAct(pnj.getMovAct()-cas.getCasAnt().getConsumeMov());
                    pnj.cas.vacia = true;
                    pnj.cas.pnj = null;
                    cas.getCasAnt().vacia = false;
                    cas.getCasAnt().pnj = pnj;
                    pnj.cas = cas.getCasAnt();
                    gm.setMata();
                }
                else {
                    pnj.transform.SetParent(gm.filasPnj[filaIni].transform, false);
                    pnj.transform.position = posIni;
                }
                desPintaCas();
            }
        }
    }
    void Update()
    {
        if(enMovimiento){
            pnj.transform.SetParent(gm.draggingPos.transform, false);
            pnj.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }
    public void movible(){ //Busca las casillas a las que puede mover un personaje al cogerlo
        int posXAct = pnj.getCasAct().getPosX();
        int posYAct = pnj.getCasAct().getPosY();
        int posArr = posXAct+posYAct*8;
        if(pnj.getNumAtaAct() > 0)
            pintaAta(posArr, pnj.getRang(), pnj.cas); //Pinta las casillas atacables adyacentes
        pintaCas(posArr, pnj.getMovAct(), pnj.getRang()); //Pinta el resto de casillas
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
    private void pintaCas(int pos, int mov, int rang){
        int posAux;
        int movAux = mov;
        if(movAux > 0){
            if(((posAux = pos+1)%8) != 0 && gm.tablero[posAux].vacia && (!gm.tablero[posAux].pintada || gm.tablero[posAux].getConsumeMov() > 3 - movAux)){
                ejecutaPintado(posAux, movAux, rang); //Busca la casilla a la derecha si es movible
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(posAux, rang, gm.tablero[posAux]); //Busca ataques adyacentes
            }
            if((((posAux = pos-1)+1) %8) != 0 && gm.tablero[posAux].vacia && (!gm.tablero[posAux].pintada || gm.tablero[posAux].getConsumeMov() > 3 - movAux)){
                ejecutaPintado(posAux, movAux, rang); //Busca la casilla a la izquierda si es movible
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(posAux, rang, gm.tablero[posAux]); //Busca ataques adyacentes
            }
            if((posAux = pos+8) < gm.tablero.Length && gm.tablero[posAux].vacia && (!gm.tablero[posAux].pintada || gm.tablero[posAux].getConsumeMov() > 3 - movAux)){
                ejecutaPintado(posAux, movAux, rang); //Busca la casilla de arriba si es movible
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(posAux, rang, gm.tablero[posAux]); //Busca ataques adyacentes
            }
            if((posAux = pos-8) >= 0 && gm.tablero[posAux].vacia && (!gm.tablero[posAux].pintada || gm.tablero[posAux].getConsumeMov() > 3 - movAux)){
                ejecutaPintado(posAux, movAux, rang); //Busca la casilla de debajo si es movible
                if(pnj.getNumAtaAct() > 0)
                    pintaAta(posAux, rang, gm.tablero[posAux]); //Busca ataques adyacentes
            }

        }
    }

    private void pintaAta(int pos, int rang, Casilla cas){
        int posAux;
        for(int i = rang; i > 0; i--)
            if(i > 0){
                if(((posAux = pos+i)%8) != i-1 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                    ejecutaAtacable(posAux, cas);
                }
                if((((posAux = pos-i)+1) %8) != rang-1 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                    ejecutaAtacable(posAux, cas);
                }
                if((posAux = pos+8*i) < gm.tablero.Length && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                    ejecutaAtacable(posAux, cas);
                }
                if((posAux = pos-8*i) >= 0 && !gm.tablero[posAux].vacia && !gm.tablero[posAux].pintada){
                    ejecutaAtacable(posAux, cas);
                }
            }
    }

    private void ejecutaPintado(int posAux, int mov, int rang){
        if(gm.tablero[posAux].gameObject.GetComponent<Image>().sprite != gm.casVacia)
            gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = casMov;
        gm.tablero[posAux].pintada = true;
        gm.tablero[posAux].setConsumeMov(pnj.getMaxMov() - mov + 1);
        pintaCas(posAux, mov-1, rang);
    }

    private void ejecutaAtacable(int posAux, Casilla cas){
        if(gm.tablero[posAux].pnj.enemigo && (!pnj.healer || pnj.mejoradoAta)){
            gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = casAta;
            gm.tablero[posAux].pintada = true;
            gm.tablero[posAux].setCasAnt(cas);
        }
        else if(!gm.tablero[posAux].pnj.enemigo && pnj.healer && gm.tablero[posAux].pnj != pnj){
            gm.tablero[posAux].gameObject.GetComponent<Image>().sprite = gm.casMarcada;
            gm.tablero[posAux].pintada = true;
            gm.tablero[posAux].setCasAnt(cas);
        }
    }

    private void desPintaCas(){
        for(int i = 0; i < gm.tablero.Length;i++){
            gm.tablero[i].gameObject.GetComponent<Image>().sprite = gm.tablero[i].getImagenIni();
            gm.tablero[i].setConsumeMov(0);
            gm.tablero[i].pintada = false;
            gm.tablero[i].setCasAnt(null);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla")){
            if(cas.pintada){
                cas.setColorAnt(cas.GetComponent<Image>().sprite);
                pintaAzul(cas);
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
            if(cas2.pintada){
                cas2.GetComponent<Image>().sprite = cas2.getColorAnt();
            }
            if(nuevaCas) nuevaCas = false;
            else sobreCasilla = false;
        }
    }
    private void pintaAzul(Casilla casilla){
        casilla.gameObject.GetComponent<Image>().sprite = gm.casVacia;
    }
}
