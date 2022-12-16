using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
 
    public static int ControlType;       
    public static float GameDifficulty;   

    
    [SerializeField]
    private GameObject MenuContainer;
    [SerializeField]
    private TMPro.TextMeshProUGUI MenuButtonText;
    [SerializeField]
    private TMPro.TextMeshProUGUI MessageText;

    private GameStat gameStat;   

    void Start()
    {
        ControlType = 0;
        GameDifficulty = .5f;
        gameStat =                       
            GameObject.Find("GameStat")   
            .GetComponent<GameStat>();    

        ShowMenu(MenuContainer.activeInHierarchy, "Start");
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu(!MenuContainer.activeInHierarchy,
                message: "Paused on time: " +
                gameStat.GameTime   
            );
        }
    }

    public void ShowMenu(bool isVisible = true, string buttonText = "Resume", string message = "")
    {
        if (isVisible)  
        {
            MenuContainer.SetActive(true);
            Time.timeScale = 0;
            MenuButtonText.text = buttonText;
            MessageText.text = message;
        }
        else  
        {
            MenuContainer.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void GameOver()
    {
        ShowMenu(buttonText: "Resume", message: "You loose\n" + $"Score:{gameStat.GameScore}\n");
    }


    public void MenuButtonClick()
    {
        ShowMenu(false);
    }
    public void ControlTypeChanged(int index)  
    {
        
        GameMenu.ControlType = index;
    }
    public void DifficultyChanged(float value)
    {
        
        GameMenu.GameDifficulty = value;
    }
}
