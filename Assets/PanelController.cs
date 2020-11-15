using UnityEngine;
using UnityEngine.Events;

public class PanelController : MonoBehaviour
{
    public UnityEvent fadeOutComplete;

    public void DisablePanel()
    {
        fadeOutComplete?.Invoke();
    }
}
