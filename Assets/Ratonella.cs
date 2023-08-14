using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratonella : MonoBehaviour
{
    public Dialogue dialogueScript1;
    public Dialogue dialogueScript2; 

    void Start()
    {

        string ratData = PlayerPrefs.GetString("RatSecretary");

        if(ratData == "done")
        {
            dialogueScript1.enabled = true;
            dialogueScript2.enabled = false;
        }
        else
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = true;
        }
        
    }
}
