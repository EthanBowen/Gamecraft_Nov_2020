using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public UnityEvent dialogFinishEvent;

    public GameObject panel;
    public TextMeshProUGUI text;
   
    private string levelToLoad;
    private bool isActive = false;
    private bool loadedFromLevel = false;

    private void Start()
    {
        if(DataManager.LoadedIntoLevel)
        {
            loadedFromLevel = true;
            string dialogText = DataManager.WonLevel ? DataManager.SuccessText : DataManager.FailText;

            StartShowingDialogue(dialogText, "");    
        }
    }

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            // If we've come from a level, then we shouldn't go into another level when you finish the dialogue
            if (loadedFromLevel)
                StopShowingDialogue();
            else
                SceneManager.LoadScene(levelToLoad);
        }
    }

    // Called from the SelectLevel UnityEvent
    public void StartShowingDialogue(string dialogText, string scene)
    {
        isActive = true;
        panel.SetActive(true);
        text.text = dialogText;
        levelToLoad = scene;
    }

    private void StopShowingDialogue()
    {
        isActive = false;
        panel.SetActive(false);
        loadedFromLevel = false;
        dialogFinishEvent?.Invoke();
    }
}   
