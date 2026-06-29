using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private GameObject[] spawnObjects;

    [Header("Plyer Components")] 
    [SerializeField] private PlayerController controller;
    [SerializeField] private CharacterController charControl;
    [SerializeField] private PlayerManager playerManager;

    [Header("Next Scene")]
    [SerializeField] private string nextScene;
    [SerializeField] private string outroCutscene;


    void Start()
    {
        for (int i = 0; i < spawnObjects.Length; i++)
        {
            spawnObjects[i].SetActive(true);
        }

        playerManager.enabled = true;
        charControl.enabled = true;
        controller.enabled = true;
        controller.Activate();
    }

    public void EndLevel()
    {
        CutsceneRouter.VideoPath = "CutScenes/" + outroCutscene;
        CutsceneRouter.NextSceneName = "Scenes/" + nextScene;
        HubVersion.levelsFinished++;
        SceneManager.LoadScene("CutsceneScene");
    }
}
