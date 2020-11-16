using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public UnityEvent dialogFinishEvent;

    public GameObject panel;
    public Image elfImage;
    public TextMeshProUGUI text;
    public Animator animator;

    public Sprite successElf;
    public Sprite neutralElf;
    public Sprite failElf;

    private string levelToLoad;
    private bool isActive = false;
    private bool loadedFromLevel = false;

    private void Start()
    {
        if (DataManager.AllUsed())
            return;

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
        // We're coming back from a level, show success or fail
        if (scene == "")
            elfImage.sprite = DataManager.WonLevel ? successElf : failElf;
        else
            elfImage.sprite = neutralElf;

        panel.SetActive(true);
        text.text = dialogText;
        levelToLoad = scene;
    }

    private void StopShowingDialogue()
    {
        isActive = false;
        animator.SetBool("Hide", true);
    }

    public void DisablePanel()
    {
        panel.SetActive(false);
        animator.SetBool("Hide", false);
        loadedFromLevel = false;
        dialogFinishEvent?.Invoke();
    }
}
