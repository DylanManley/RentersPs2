using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject[] HubItems;
    [SerializeField] private int[] unlockLevel;
    [SerializeField] private GameObject HubUI;
    private bool inUI = false;

     void Start()
    {
        for (int i = 0; i < HubItems.Length; i++)
        {
            if (HubVersion.levelsFinished >= unlockLevel[i])
            {
                HubItems[i].SetActive(true);
            }
        }

        
    }

    void Update()
    {
        if (inUI)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                HubUI.SetActive(false);
                inUI = false;
            }
        }
    }

    public void Interact(Transform t_interactor)
    {
        HubUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        inUI = true;
    }


    public void LoadFarm()
    {
        CutsceneRouter.VideoPath = "CutScenes/FarmIntro";
        CutsceneRouter.NextSceneName = "Scenes/Farm";
        SceneManager.LoadScene("CutsceneScene");
    }

    public void LoadBeach()
    {
        if(HubVersion.levelsFinished > 0)
        {
            CutsceneRouter.VideoPath = "CutScenes/BeachCutscene";
            CutsceneRouter.NextSceneName = "Scenes/Beach";
            SceneManager.LoadScene("CutsceneScene");
        }
    }
}
