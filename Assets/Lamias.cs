using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamias : MonoBehaviour
{
    public Dialogue dialogoNormal;
    public DialogoDecisiones dialogoDecisiones; 
    private string lamias;
    public static int rightAnswers;

    // Start is called before the first frame update
    void Start()
    {
        rightAnswers = 0;
        lamias = "";
        PlayerPrefs.SetString("Lamias", lamias);
        if(PlayerPrefs.HasKey("Lamias")) lamias = PlayerPrefs.GetString("Lamias");
    }

    // Update is called once per frame
    void Update()
    {
        lamias = PlayerPrefs.GetString("Lamias");
        //Debug.Log(lamias);

        if(lamias == "done")
        {
            dialogoNormal.enabled = true;
            dialogoDecisiones.gameObject.SetActive(false);
            dialogoDecisiones.enabled = false;
            dialogoNormal.gameObject.SetActive(false);

        }
        else
        {
            dialogoNormal.enabled = false;
            dialogoDecisiones.enabled = true;
        }

        if(rightAnswers == 3)  
        {
            lamias = "done";
            string lamiasData = JsonUtility.ToJson(lamias);
            PlayerPrefs.SetString("Lamias", lamias);
            PlayerPrefs.Save();
        }
        
    }

}
