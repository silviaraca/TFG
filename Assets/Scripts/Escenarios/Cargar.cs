using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cargar : MonoBehaviour
{
    public Player player;

    public void load(string escena){
        SceneManager.LoadScene(escena);
    }
    
}
