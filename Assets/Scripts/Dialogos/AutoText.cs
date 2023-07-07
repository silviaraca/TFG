using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoText : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI textoNombre;
   [SerializeField] private TextMeshProUGUI textoE;
   [SerializeField] private TextMeshProUGUI textoChat;
   [SerializeField] private TestChat test;
   [SerializeField] private Dialogue d;
   [SerializeField] private Image panel;
   [SerializeField] private Movement move;
   private bool activeE;
   private Player player;
    private bool finFrase;
    private int index;
   

   private void Start(){
        panel.gameObject.SetActive(false);
        finFrase = true;
        index = 0;
   }

   private void Update(){
        
        panel.gameObject.SetActive(true);
        move.allowMove = false;
        textoNombre.text = "d.name";
        if(index <= d.sentences.Length){
            if(index != d.sentences.Length)
            {
                test.Run(d.sentences[index], textoChat); 
            }
                index++;
            
        }
        if (index > d.sentences.Length){
            panel.gameObject.SetActive(false);
            move.allowMove = true;
            index = 0;
            textoE.gameObject.SetActive(true);

            //Destroy(this.gameObject);
        }

   }

    public void SetFin(bool b){
        finFrase = b;
    }
}



