using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JSONActivator : MonoBehaviour
{
    [SerializeField] private string metodo;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ajo(){
        string casa = "P. House1";
        PlayerPrefs.SetString("P. House", casa);
        PlayerPrefs.Save();
        string town = "Town";
        PlayerPrefs.SetString("Town", town);
        PlayerPrefs.Save(); 
    }

    public void VHHabla1(){
        string casa = "P. House2";
        PlayerPrefs.SetString("P. House", casa);
        PlayerPrefs.Save();
        string town = "Town2";
        PlayerPrefs.SetString("Town", town);
        PlayerPrefs.Save();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (collision.gameObject.name.Equals("Player"))
        {
            if(metodo == "Ajo"){
                Ajo();
            }
            else if(metodo == "VHHabla1"){
                VHHabla1();
            }
        }
    }

}
