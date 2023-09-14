using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownAutoDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public AutoDialogue auto;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("ADTown"))
        {
            auto.enabled = false;
        }

        if(auto.indexFin())
        {
            string s = "done";
            PlayerPrefs.SetString("ADTown", s);
        }
    
    }
}
