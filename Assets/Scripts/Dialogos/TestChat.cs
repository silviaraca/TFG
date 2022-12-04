using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestChat : MonoBehaviour
{

   private string sentence;
   private TMP_Text dialogueText;

   public TyperEffect typer;

   public void Run(string sentence, TMP_Text dialogueText){
        this.sentence = sentence;
        this.dialogueText = dialogueText;
        typer.Run(sentence, dialogueText);

   }
}
