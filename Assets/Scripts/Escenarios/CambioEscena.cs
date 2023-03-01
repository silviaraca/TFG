using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CambioEscena : MonoBehaviour
{
   public TextMeshProUGUI textoE;
   private bool usable;
   private Player player;
   public Cargar cargar;
   [SerializeField] private string escena;

   private void Start(){
        textoE.gameObject.SetActive(false);
   }

   private void Update(){
        if(usable && Input.GetKeyDown(KeyCode.E))
            cargar.load(escena);
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(true);
            usable = true; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(false);
            usable = false;
        }            
    }
}
