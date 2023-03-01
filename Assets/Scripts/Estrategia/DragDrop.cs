using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool enMovimiento = false;
    private Vector2 posIni;
    public bool sobreCasilla;
    public Carta card; //Esto no debería hacerlo public, mirar pa que pille su propia carta como hace el gm
    private bool cerrojo = true;

    private Casilla cas;

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
        if(sobreCasilla && cas.vacia){
            card.transform.position = cas.transform.position;
            cas.card = card;
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
            sobreCasilla = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        string nombreObjeto = collision.gameObject.name.Substring(0,7);
        if(nombreObjeto.Equals("Casilla")){
            sobreCasilla = false;
        }
    }
}
