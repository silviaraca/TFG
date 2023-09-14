using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mirror : MonoBehaviour
{
    public TextMeshProUGUI textoE;
    public BoxCollider2D boxCollider;
    public BoxCollider2D boxCollider2;
    private SpriteRenderer spriteRenderer;
    private Player player;
    private bool dejable;
    private bool done;
    private string mirrorPlaced;

    void Start()
    {
        if(PlayerPrefs.HasKey("RatSecretary"))
        {
            if(PlayerPrefs.HasKey("MirrorPlaced")) mirrorPlaced = PlayerPrefs.GetString("MirrorPlaced");
            if(mirrorPlaced != "done") //Si ya se ha colocado el espejo
            {
                done = false;
                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                textoE.gameObject.SetActive(false);
                boxCollider2.enabled = false;
            }
            else
            {
                boxCollider.enabled = false;
            }
        }
        else
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            boxCollider2.enabled = false;
        } 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(player && !done){
            textoE.gameObject.SetActive(true);
            dejable = true;
        }                  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player") && !done){
            textoE.gameObject.SetActive(false);
            dejable = false;
        } 
    }

    void Update()
    {
        if (dejable && Input.GetKeyDown(KeyCode.E) && !done)
        {
            spriteRenderer.enabled = true;
            textoE.gameObject.SetActive(false);
            done = true;
            boxCollider.enabled = false;
            boxCollider2.enabled = true;

            string mirror = "done";
            PlayerPrefs.SetString("MirrorPlaced", mirror);
            PlayerPrefs.Save();

        }
    }
}
