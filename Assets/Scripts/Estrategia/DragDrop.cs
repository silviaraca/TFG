using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragDrop : MonoBehaviour
{
    private bool enMovimiento = false;
    public Carta card;    
    public GameObject personajePrefab;
    private GameManagerE gm;


    void Start()
    {
        gm = FindObjectOfType<GameManagerE>();
        
    }

    public void cogeCarta(){
        if(gm.tutorial){
            if(((gm.getFase() == 2 && card.nombreCarta == "Estaca" && gm.getCarJugadas() == 0) || (gm.getFase() == 4 && card.nombreCarta == "Agua" && gm.getCarJugadas() == 1)) && gm.getCarJugadas() < 2){
                GetComponent<Image>().color = new Color(0, 255, 255, 255);
                enMovimiento = true;
                gm.puntero.GetComponent<DropCard>().card = card;
                gm.puntero.GetComponent<DropCard>().personajePrefab = personajePrefab;
                
            }
        }
        else{
            if((gm.getFase() == 2 && (card.getFaseCarta() == 2 || card.getFaseCarta() == 24)) && gm.getCarJugadas() < 2){
                GetComponent<Image>().color = new Color(0, 255, 255, 255);
                enMovimiento = true;
                gm.puntero.GetComponent<DropCard>().card = card;
                gm.puntero.GetComponent<DropCard>().personajePrefab = personajePrefab;
                
            }
            else if((gm.getFase() == 4 && (card.getFaseCarta() == 4 || card.getFaseCarta() == 24)) && gm.getCarJugadas() < 2){
                GetComponent<Image>().color = new Color(0, 255, 255, 255);
                enMovimiento = true;
                gm.puntero.GetComponent<DropCard>().card = card;
                gm.puntero.GetComponent<DropCard>().personajePrefab = personajePrefab;
                
            }
        }
    }
    void Update()
    {
        if(enMovimiento){
            gm.puntero.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 200);
        }
    }
    public void stopMov(){
        enMovimiento = false;
    }
}