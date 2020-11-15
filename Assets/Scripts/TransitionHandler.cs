using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{

    public GameObject Logo;
    public GameObject StartScreen;
    public Animator StartScreenAnimator;
    public float TimeToStartScreen = 2f;
    public float TimeOnStartScreen = 2f;
    public string NextScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowStartScreen(TimeToStartScreen));
    }

    private IEnumerator ShowStartScreen(float time)
    {
        yield return new WaitForSeconds(time);
        StartScreen.SetActive(true);
        StartCoroutine(ExitStartScreen(TimeOnStartScreen));
    }

    private IEnumerator ExitStartScreen(float time)
    {
        yield return new WaitForSeconds(time);
        StartScreenAnimator.SetTrigger("CanExit");
        StartCoroutine(MakeTransition(TimeOnStartScreen + 1));
    }

    private IEnumerator MakeTransition(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(NextScene);
    }

}
