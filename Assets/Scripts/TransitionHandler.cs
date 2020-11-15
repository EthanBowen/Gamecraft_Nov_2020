using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public GameObject Logo;
    public GameObject Title;
    public Animator logoAnimator;
    public Animator StartScreenAnimator;
    public float IntervalTime = 2f;
    public string NextScene;

    private void Start()
    {
        Logo.SetActive(true);
    }

    public void LogoShown()
    {
        StartCoroutine(nameof(HideLogo));
    }

    public void LogoHidden()
    {
        Logo.SetActive(false);
        StartCoroutine(nameof(ShowTitle));
    }

    public void TitleShown()
    {
        StartCoroutine(nameof(MakeTransition));
    }

    private IEnumerator HideLogo()
    {
        yield return new WaitForSeconds(IntervalTime);
        logoAnimator.SetBool("HideLogo", true);
    }

    private IEnumerator ShowTitle()
    {
        yield return new WaitForSeconds(IntervalTime);
        Title.SetActive(true);
    }

    private IEnumerator MakeTransition()
    {
        yield return new WaitForSeconds(IntervalTime);
        SceneManager.LoadScene(NextScene);
    }
}
