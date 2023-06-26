using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Letter : MonoBehaviour
{
 public GameObject uiPanel; // Referencia al panel de la UI que se muestra al recoger el objeto
    public TextMeshProUGUI textoE;
    private Player player;
    private bool isUIVisible; // Variable para controlar la visibilidad de la UI

    private void Start()
    {
        uiPanel.SetActive(false);
        textoE.gameObject.SetActive(false);
        isUIVisible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isUIVisible)
            {
                // Ocultar la UI
                uiPanel.SetActive(false);
                isUIVisible = false;
                PauseMenu.isPaused = false;
                Destroy(this.gameObject);
            }
            else
            {
                // Mostrar la UI
                uiPanel.SetActive(true);
                isUIVisible = true;
                PauseMenu.isPaused = true; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player)
        {
            textoE.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player)
        {
            textoE.gameObject.SetActive(false);
        }
    }
}

   

