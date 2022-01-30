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
    public GameObject winMenu;
    private Color temp;

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
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
}
