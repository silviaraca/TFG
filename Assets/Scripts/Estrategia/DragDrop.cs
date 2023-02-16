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
        if(sobreCasilla){
            transform.position = cas.transform.position;
            int i = card.handIndex;
            while(i+1 < gm.espacioMano.Length && !gm.espacioManoSinUsar[i+1]){
                gm.mano[i+1].transform.position = gm.espacioMano[i].position;
                gm.mano[i] = gm.mano[i+1];
                gm.mano[i].handIndex = i;
                i++;
            }
            gm.espacioManoSinUsar[i] = true;
            gm.mano.Remove(gm.mano[i]);//Esto es mu cutre, mejoralo
        }
        else{
            transform.position = posIni;
        }
    }
    void Update()
    {
        if(enMovimiento){
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        if(collision.gameObject.name.Equals("Casilla")){
            sobreCasilla = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cas = collision.GetComponent<Casilla>();
        if(collision.gameObject.name.Equals("Casilla")){
            sobreCasilla = false;
        }
    }
}
