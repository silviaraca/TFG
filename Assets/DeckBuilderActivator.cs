using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilderActivator : MonoBehaviour
{
    public static bool okey;
    public CambioEscena cambioEsc;
    public AutoDialogue auto;
    // Start is called before the first frame update
    void Start()
    {
      okey = false;
      if(PlayerPrefs.HasKey("DeckActivator"))  
        {
            auto.delete();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(okey && !PlayerPrefs.HasKey("DeckActivator"))
        {
            auto.delete();
            string deck = "done";
            PlayerPrefs.SetString("DeckActivator", deck);
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("DeckActivator"))  
        {
            cambioEsc.enabled = true;
            cambioEsc.apagado = false;
            
        } 
    }
}
