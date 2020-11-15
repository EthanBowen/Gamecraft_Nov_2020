using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;
   
    private string levelToLoad;
    private bool isActive = false;

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            // Do the level transition
            SceneManager.LoadScene(levelToLoad);
        }
    }

    public void StartShowingDialogue(string dialogText, string scene)
    {
        isActive = true;
        panel.SetActive(true);
        text.text = dialogText;
        levelToLoad = scene;
    }
}   
