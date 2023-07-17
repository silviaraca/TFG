using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    public GameObject bar;
    public Image healthBar;
    public float maxHealth;
    private List<Image> barras = new List<Image>();
    public void iniVida()
    {
        barras.Clear();
        maxHealth = this.transform.parent.gameObject.GetComponent<Personaje>().getVida();
        for(int i = 0; i < maxHealth;i++){
            GameObject a = Instantiate(bar);
            a.transform.SetParent(healthBar.transform, false);
            barras.Add(a.gameObject.GetComponent<Image>());
        }
    }

    public void pierdeVida(int vidaAct, int vidaMax){
        for(int i = 0; i < vidaMax - vidaAct; i++){
            barras[i].gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 50);
        }
    }
    public void ganaVida(int vidaAct, int vidaMax){
        for(int i = 0; i < vidaAct; i++){
            barras[i].gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
    }
}
