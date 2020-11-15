using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public string Gift;
    public string LevelName;
    public GameObject Hover;
    [TextArea]
    public string AcceptDialog;
    [TextArea]
    public string DenialDialog;
    [TextArea]
    public string ThanksText;
}
