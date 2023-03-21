using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void cogeCarta(){
        enMovimiento = true;
        if(cerrojo)
            posIni = transform.position;
        cerrojo = false;
    }

    public void sueltaCarta(){
        enMovimiento = false;
        cerrojo = true;
        if(sobreCasilla && (cas.vacia && card.enMano)){ 
            GameObject a = Instantiate(personajePrefab);
            a.transform.SetParent(gm.Canvas.transform, false);
            a.transform.position = cas.transform.position; 
            a.gameObject.GetComponent<Personaje>().setCasAct(cas);
            card.transform.position = gm.zonaDescarte.transform.position;
            cas.pnj = a.GetComponent<Personaje>();
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
            
        }
        else{
            
            card.transform.position = posIni;
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


/*
        else if(sobreCasilla && (!cas.vacia && !card.enMano)){ //Esto ya no es necesario en las cartas
            Casilla casAct = card.getCasAct();
            casAct.vacia = true;
            Personaje pnjAct = cas.pnj;
            Destroy(pnjAct);
            casAct.pnj = null;
            pnj.transform.position = cas.transform.position;
            cas.pnj = pnj;
            pnj.setCasAct(cas);
        }
*/