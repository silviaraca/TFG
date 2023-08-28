using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public Cargar cargar;
    public AudioSource audioSource;
    [SerializeField] private string escena;   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        audioSource.Play();
        cargar.load(escena);
    }

    public void exitGame()
    {
        audioSource.Play();
        Application.Quit();
    }
}
