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
    [SerializeField] TextMeshProUGUI inGameTutorialsText;
    [SerializeField] GameObject InteractedObjectUI;
    [SerializeField] TextMeshProUGUI interactedObjectUIText;
    [TextArea(3, 10)] // The parameters define the minimum and maximum lines for the text area
    [SerializeField] string interactionMessage;
    [TextArea(3, 10)] // The parameters define the minimum and maximum lines for the text area
    [SerializeField] string inGameTutorialMessage;

    [Header("Text Writer Settings")]
    [SerializeField] float writeSpeed;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inGameTutorialsText.gameObject.SetActive(true);
            inGameTutorialsText.text = inGameTutorialMessage;
            inGameDialogueBox.SetActive(true);
            textWriterSingle = TextWriter.AddWriter_Static(inGameDialogueText, dialogue, writeSpeed, true, true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inGameTutorialsText.gameObject.SetActive(false);
            inGameDialogueBox.SetActive(false);
        }
    }


}
