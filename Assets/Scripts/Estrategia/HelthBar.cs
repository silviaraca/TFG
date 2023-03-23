using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    public GameObject canvas, bar;
    public Image healthBar, prevIm;
    public float maxHealth;
    private List<Image> barras = new List<Image>();
    private float posIni, dist, widthA;
    private const float constScale = 68177E-5f;
    void Start()
    {
        RectTransform rt = healthBar.rectTransform;
        widthA = (((rt.rect.width - 10 - (maxHealth+1)*2)) / maxHealth);
        posIni = (healthBar.transform.position.x - ((rt.rect.width-10)*constScale/2)  + widthA*constScale/2 + 2);
        for(int i = 0; i < maxHealth; i++){
            GameObject a = Instantiate(bar);
            a.transform.SetParent(healthBar.transform, false);
            a.gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthA, healthBar.rectTransform.sizeDelta.y - 8);
            if(i == 0)
                a.transform.position = new Vector2(posIni, healthBar.transform.position.y);
            else{
                a.transform.position = new Vector2(prevIm.transform.position.x + widthA*constScale + 125E-2f, healthBar.transform.position.y);
            }
            prevIm =  a.gameObject.GetComponent<Image>();
            barras.Add(prevIm);
        }
    }
    void Update()
    {

    }

    /*
        GameObject imgObject = new GameObject("testAAA");

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);


        Image image = imgObject.AddComponent<Image>();
        Texture2D tex = Resources.Load<Texture2D>("red");
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObject.transform.SetParent(canvas.transform);
    */
}
