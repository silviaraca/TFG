using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public List<string> deck;
    public int numItem;
    public Vector2 playerPosition;
    public AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI textoE;
    [SerializeField] private string nombreActivo = " ";


    private void Awake()
    {
        textoE.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("PosicionPlayer"))
        {
            string posicionJSON = PlayerPrefs.GetString("PosicionPlayer");
            Vector3 posicion= JsonUtility.FromJson<Vector3>(posicionJSON);
            this.gameObject.transform.position = posicion;
        }
        nombreActivo = " ";
    }

    void Update()
    {
        playerPosition = transform.position;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.Play();
            }
        }

    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        nombreActivo = collision.gameObject.name.ToString().Trim();  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nombreActivo = " ";          
    }
    public string getActivo(){
        return nombreActivo;
    }
}
