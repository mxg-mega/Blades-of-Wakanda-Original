using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEvents : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameplayPanel;
    public bool isMessageActive;

    // Start is called before the first frame update
    void Start()
    {
        isMessageActive= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");

    }

    public void Pause()
    {
        Time.timeScale= 0;
        pausePanel.SetActive(true);
        gameplayPanel.SetActive(false);

    }
    public void Resume()
    {
        Time.timeScale= 1;
        pausePanel.SetActive(false);
        gameplayPanel.SetActive(true);

    }

}
