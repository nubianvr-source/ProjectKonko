using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject CurrentPanelVisible;
    public GameObject NextPanelVisible;
    public TextMeshProUGUI dialogueText;
    public string nextUIView;
    public Konko.UIManagement.UIManager uiManager;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void startDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with" + dialogue.name);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(nextUIView);
    }

    public void DisplayNextSentence(string name) 
    
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            uiManager.ShowOnlyScreenFadeOn(name);
           // FindObjectOfType<AudioManager>().StopSound("Theme");

            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceAnimate(sentence));
    }

    IEnumerator TypeSentenceAnimate(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        Debug.Log("End of Dialogue");

    }
}
