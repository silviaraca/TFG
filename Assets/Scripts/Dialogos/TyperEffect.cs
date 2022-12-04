using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TyperEffect : MonoBehaviour
{
    public void Run(string sentence, TMP_Text dialogueText){
        StartCoroutine(TyperText(sentence, dialogueText));
    }

    private IEnumerator TyperText(string sentence, TMP_Text dialogueText){
        float t = 0;
        int charIndex = 0;
        Debug.Log("llego aquí");
        while(charIndex < sentence.Length){
            t += Time.deltaTime * 30f;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, sentence.Length);

            dialogueText.text = sentence.Substring(0, charIndex);

            yield return null;

            if(charIndex == sentence.Length){
                dialogueText.text = sentence;
                yield break;
            }
        }
        
    }
}
