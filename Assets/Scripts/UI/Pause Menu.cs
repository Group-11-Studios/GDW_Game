using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Image pauseMenu;
    Button resumeButton;
    Button quitButton;

    Audio audioScript;

    // Start is called before the first frame update
    void Start()
    {
        Transform pauseBackground = transform.Find("Pause Background");

        pauseMenu = pauseBackground.GetComponent<Image>();
        resumeButton = pauseBackground.Find("Resume Button").GetComponent<Button>();
        quitButton = pauseBackground.Find("Quit Button").GetComponent<Button>();

        resumeButton.onClick.AddListener(OnResumeButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);

        audioScript = pauseBackground.gameObject.AddComponent<Audio>();
    }

    void OnEscapePress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0.0f; 
        }
    }

    void OnResumeButtonClick()
    {
        pauseMenu.gameObject.SetActive(false);

        //resume game speed again
        Time.timeScale = 1.0f;
    }

    void OnQuitButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        OnEscapePress();
      
    }
}
