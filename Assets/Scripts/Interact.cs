using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interact : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;

    [Header("Dialogue")]
    [SerializeField] GameObject inGameDialogueBox;
    [SerializeField] TextMeshProUGUI inGameDialogueText;
    [TextArea(3, 10)] // The parameters define the minimum and maximum lines for the text area
    [SerializeField] string dialogue;

    [Header("In Game Tutorials, pop ups etc.")]
    [SerializeField] TextMeshProUGUI speaker;
    [SerializeField] TextMeshProUGUI inGameTutorialsText;
    [SerializeField] GameObject InteractedObjectUI;
    [SerializeField] TextMeshProUGUI interactedObjectUIText;
    [SerializeField] MeshRenderer meshOfInteracted;
    [SerializeField] MeshRenderer meshOfInteracted2;
    [SerializeField] GameObject effectsOfInteracted;
    [SerializeField] string speakersName;
    [TextArea(3, 10)] // The parameters define the minimum and maximum lines for the text area
    [SerializeField] string interactionMessage;
    [TextArea(3, 10)] // The parameters define the minimum and maximum lines for the text area
    [SerializeField] string inGameTutorialMessage;

    [Header("Text Writer and interaction Settings")]
    [SerializeField] float writeSpeed;
    [SerializeField] bool interactedWithThis = false;
    [SerializeField] float disappearTime;





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !interactedWithThis)
        {
            if (inGameTutorialsText != null && this.gameObject.tag == "Tutorial")
            {
                inGameTutorialsText.gameObject.SetActive(true);
                inGameTutorialsText.text = inGameTutorialMessage;
            }
            else if (inGameDialogueBox != null && dialogue != null && this.gameObject.tag == "Dialogue")
            {
                inGameDialogueBox.SetActive(true);
                textWriterSingle = TextWriter.AddWriter_Static(inGameDialogueText, dialogue, writeSpeed, true, true);
            }
            else if (InteractedObjectUI != null && interactedObjectUIText != null && this.gameObject.tag == "Interaction")
            {
                if (meshOfInteracted != null)
                {
                    meshOfInteracted.enabled = false;
                    meshOfInteracted2.enabled = false;
                    effectsOfInteracted.SetActive(false);
                }
                InteractedObjectUI.SetActive(true);
                speaker.text = speakersName;
                textWriterSingle = TextWriter.AddWriter_Static(interactedObjectUIText, interactionMessage, writeSpeed, true, true);
                
            }

            interactedWithThis = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Dialogue")
            {
                StartCoroutine(Disappear(inGameDialogueBox));
            }
            else if (this.gameObject.tag == "Tutorial")
            {
                StartCoroutine(Disappear(inGameTutorialsText.gameObject));
            }
            else if (this.gameObject.tag == "Interaction")
            {
                StartCoroutine(Disappear(InteractedObjectUI.gameObject));
            }       
            
            

        }
    }

    private IEnumerator Disappear(GameObject gameObject)
    {
        yield return new WaitForSeconds(disappearTime);
        gameObject.SetActive(false);
        dialogue = null;
        interactedObjectUIText = null;
        inGameTutorialsText = null;       

    }


}
