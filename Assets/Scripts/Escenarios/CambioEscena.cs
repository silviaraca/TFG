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
   public bool apagado;
   private Player player;
   public Cargar cargar;
   public Vector3 playerPosition;
   public VectorPosition playerStorage;
   public AudioSource audioSource;
   [SerializeField] private string escena;

   private void Start(){
        textoE.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
   }

   private void Update(){
        if(usable && Input.GetKeyDown(KeyCode.E)){
            string escena2 = escena;
            if(PlayerPrefs.HasKey(escena)) {
                escena2 = PlayerPrefs.GetString(escena);
                audioSource.Play();
            }
            cargar.load(escena2);
        }
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player") && !apagado){
            
            //Guardar el inventario
            string inventoryData = JsonUtility.ToJson(player.inventory);
            PlayerPrefs.SetString("InventoryData", inventoryData);
            PlayerPrefs.Save();

            textoE.gameObject.SetActive(true);
            usable = true; 
            string posRPG = JsonUtility.ToJson(playerPosition);
            PlayerPrefs.SetString("PosicionPlayer", posRPG);
            PlayerPrefs.Save();
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

    public void desactivaPuerta(){
        apagado = true;
        this.enabled = false;
    }
}
