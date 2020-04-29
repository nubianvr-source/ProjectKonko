using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Video;


    public class DialogueManager : MonoBehaviour
    {
        private Queue<string> sentences;
        public TextMeshProUGUI dialogueText;
        public UnityEvent _dialogueSentencesFinished = new UnityEvent();



        // Start is called before the first frame update
        void Start()
        {
            sentences = new Queue<string>();

        }

        public void startDialogue(Dialogue dialogue)
        {
            Debug.Log("Starting conversation with" + dialogue.name);
            sentences.Clear();
           _dialogueSentencesFinished = dialogue.displaySentencesFinished; 
            foreach (var sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()

        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                _dialogueSentencesFinished.Invoke();
                // FindObjectOfType<AudioManager>().StopSound("Theme");

                return;
            }

            var sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentenceAnimate(sentence));
        }

        IEnumerator TypeSentenceAnimate(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                //AudioManager.Instance.PlaySound("Typing");
                yield return null;
            }
        }

        void EndDialogue()
        {
            Debug.Log("End of Dialogue");

        }


    }