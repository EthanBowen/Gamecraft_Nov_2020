using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public GameObject textContainer;
    public TextMeshProUGUI text;
    
    [TextArea]
    public List<string> startText;
    [TextArea]
    public List<string> endText;

    public bool showingDialogue = true;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Dequeue(startText);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (startText.Count > 0)
                text.text = Dequeue(startText);
            else
                textContainer.SetActive(false);
        }
    }

    private string Dequeue(List<string> textList)
    {
        string val = textList[0];
        textList.RemoveAt(0);
        return val;
    }
}   
