using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int destroyedBuildings;
    public int numOfBuildingsToWin;

    public Image winPanel;
    public Image loosePanel;
    public GameObject winMenu;
    public GameObject looseMenu;
    private Color temp;

    public GameObject pauseMenu, playMenu, menu3D;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        playMenu.SetActive(false);
        menu3D.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
    private void Start()
    {
        temp = winPanel.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void DestroyedABuilding()
    {
        destroyedBuildings++;

        if(destroyedBuildings >= numOfBuildingsToWin)
        {
            StartCoroutine(WinLevel());
        }
    }

    public IEnumerator WinLevel()
    {
        while(temp.a < .5)
        {
            yield return new WaitForSeconds(.1f);
            temp.a = temp.a + 0.01f;
            winPanel.color = temp;
        }
        winMenu.SetActive(true);
    }

    public void loose()
    {
        StartCoroutine(LooseLevel());
    }
    public IEnumerator LooseLevel()
    {
        while (temp.a < .5)
        {
            yield return new WaitForSeconds(.1f);
            temp.a = temp.a + 0.01f;
            winPanel.color = temp;
        }
        looseMenu.SetActive(true);
    }
}
