using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public string LevelName;
    public GameObject Hover;


    // Should remove
    public string Gift;
    [TextArea]
    public string AcceptDialog;
    [TextArea]
    public string DenialDialog;
    [TextArea]
    public string ThanksText;

    public List<DialogOptions> giftToDialog;
}

[System.Serializable]
public struct DialogOptions {
    public string Gift;
    [TextArea]
    public string AcceptDialog;
    [TextArea]
    public string DenialDialog;
    [TextArea]
    public string ThanksText;
}
