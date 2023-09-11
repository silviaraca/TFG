using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class AutoDialogue : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private Image pnj;
    [SerializeField] private Movement move;
    [SerializeField] private Player playerExistente;
    [SerializeField] private GameObject npcHablando;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textName;
    public string[] lines;
    public string[] names;
    public float textSpeed;
 
    private int index;
    private bool activeE, dialogue = false;
    private Player player;
    public TimelineActivator tl;
    [SerializeField] private Sprite personajeImage = null;


    void Start()
    {
        panel.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        textName.text = string.Empty;
        pnj.gameObject.SetActive(false);
        TimelineActivator.play = false;
        dialogue = false;
        if(npcHablando.GetComponent<Image>() != null)
            personajeImage = npcHablando.GetComponent<Image>().sprite;
    }

    void Update()
    {
        //Sin necesidad de pulsar la E
        if(activeE){
            move.allowMove = false;
            gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
            dialogue = true;
        }
        
        //Continuar el texto
        if(dialogue && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E)))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                pnj.gameObject.SetActive(false);
                textComponent.text = lines[index];
            }
        }
        if(dialogue && textName.text != string.Empty && textName.text != "Me" && personajeImage != null){
            pnj.sprite = personajeImage;
            pnj.gameObject.SetActive(true);
        }
        else if (dialogue && textName.text != string.Empty && personajeImage != null){
            pnj.gameObject.SetActive(false);
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            textName.text = names[index];
            StartCoroutine(TypeLine());
        }
        else
        {
            panel.gameObject.SetActive(false);
            move.allowMove = true;
            textComponent.text = string.Empty;
            textName.text = string.Empty;
            TimelineActivator.play = true;
            pnj.gameObject.SetActive(false);
            dialogue = false;
            Destroy(this.gameObject);
        }
    }

  
private void OnTriggerEnter2D(Collider2D collision)
{
    player = collision.GetComponent<Player>();
    if (collision.gameObject.name.Equals("Player"))
    {
        activeE = true;
        move.allowMove = false;
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    player = collision.GetComponent<Player>();
    if (collision.gameObject.name.Equals("Player"))
    {
        activeE = false;
        move.allowMove = true;
    }
}
}
