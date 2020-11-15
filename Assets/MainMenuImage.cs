using UnityEngine;
using UnityEngine.Events;

public class MainMenuImage : MonoBehaviour
{
    public UnityEvent finishedShowing;
    public UnityEvent finishedHiding;

    public void Shown()
    {
        finishedShowing?.Invoke();
    }

    public void Hidden()
    {
        finishedHiding?.Invoke();
    }
}
