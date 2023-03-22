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