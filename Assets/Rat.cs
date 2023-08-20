using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    private bool activeE;
    private Player player;
    private string rat;

    // Start is called before the first frame update
    void Start()
    {
        rat = "";
        //string ratData = JsonUtility.ToJson(rat);
        PlayerPrefs.SetString("RatSecretary", rat);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if(activeE)
        {
            rat = "done";
            string ratData = JsonUtility.ToJson(rat);
            PlayerPrefs.SetString("RatSecretary", rat);
            PlayerPrefs.Save();
        }
        
        //Si aún no se ha hecho lo del café
        if(!Ratonella.machine)
        {
            dialogue1.enabled = true;
            dialogue2.enabled = false;
        }
        else //Si se ha hecho lo del café
        {
            dialogue1.enabled = false;
            dialogue2.enabled = true;
        }

    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = true; 
        }                   
    }
}
