using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriterDeneme : MonoBehaviour
{
    public TextMeshProUGUI storyText;

    private TextWriter.TextWriterSingle textWriterSingle;

    public float writeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        textWriterSingle = TextWriter.AddWriter_Static(storyText, storyText.text, writeSpeed, true, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
