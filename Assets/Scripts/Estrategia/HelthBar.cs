using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    public GameObject bar;
    public Image healthBar, prevIm;
    public float maxHealth;
    private List<Image> barras = new List<Image>();
    private float posIni, dist, widthA;
    private const float constScale = 68177E-5f;
    void Start()
    {
        RectTransform rt = healthBar.rectTransform;
        widthA = (((rt.rect.width - 15 - (maxHealth+1)*2)) / maxHealth);
        posIni = (healthBar.transform.position.x - ((rt.rect.width-15)*constScale/2)  + widthA*constScale/2 + 2);
        for(int i = 0; i < maxHealth; i++){
            GameObject a = Instantiate(bar);
            a.transform.SetParent(healthBar.transform, false);
            a.gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthA, healthBar.rectTransform.sizeDelta.y - 8);
            if(i == 0)
                a.transform.position = new Vector2(posIni, healthBar.transform.position.y);
            else{
                a.transform.position = new Vector2(prevIm.transform.position.x + widthA*constScale + 120E-2f, healthBar.transform.position.y);
            }
            prevIm =  a.gameObject.GetComponent<Image>();
            barras.Add(prevIm);
        }
    }
}
