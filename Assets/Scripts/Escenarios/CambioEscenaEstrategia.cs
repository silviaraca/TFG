using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CambioEscenaEstrategia : MonoBehaviour
{
   public TextMeshProUGUI textoE;
   private bool usable;
   private Player player;
   public Cargar cargar;
   public Vector2 playerPosition;
   public VectorPosition playerStorage;
   [SerializeField] private string escena;

   private void Start(){
        textoE.gameObject.SetActive(false);
   }

   private void Update(){
        if(usable && Input.GetKeyDown(KeyCode.E))
            cargar.load("Estrategia");
   }
}
