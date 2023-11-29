using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    Button startButton;
    Button rulesButton;
    Button quitButton;
    Image rules;
    TextMeshProUGUI title;
    TextMeshProUGUI rulesText;
    Button mainMenuButton;
    
    


    // Start is called before the first frame update
    void Start()
    {
        startButton = transform.Find("Start Button").GetComponent<Button>();
        rulesButton = transform.Find("Start Button").transform.Find("Rules Button").GetComponent<Button>();
        quitButton = transform.Find("Start Button").transform.Find("Quit Button").GetComponent<Button>();
        title = transform.Find("Live Cells Text").GetComponent<TextMeshProUGUI>();
        rulesText = transform.Find("Rules Text").GetComponent<TextMeshProUGUI>();
        mainMenuButton = transform.Find("Main Menu Button").GetComponent<Button>();

        startButton.onClick.AddListener(OnStartButtonClick);
        rulesButton.onClick.AddListener(OnRulesButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    void OnQuitButtonClick()
    {
        Application.Quit();
        Debug.Log("Game has been closed");
    }

    void OnStartButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    void OnRulesButtonClick()
    {
        startButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);

        rulesText.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);


    }
    void OnMainMenuButtonClick()
    {
        startButton.gameObject.SetActive(true);
        title.gameObject.SetActive(true);

        rulesText.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
