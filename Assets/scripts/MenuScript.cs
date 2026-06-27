using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void StartDemo()
    {
        CutsceneRouter.VideoPath = "CutScenes/FarmIntro";
        CutsceneRouter.NextSceneName = "Scenes/Farm";
        SceneManager.LoadScene("CutsceneScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
