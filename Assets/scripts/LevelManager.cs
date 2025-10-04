using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [Header("cutscene Components")]

    [SerializeField] private Transform introCutscene;
    [SerializeField] private Transform outroCutscene;
    
    [SerializeField] private bool hasIntroCutscene = false;
    [SerializeField] private bool hasOutroCutscene = false;

    [SerializeField] private float introLength;
    [SerializeField] private float outroLength;

    [Header("Spawn Objects")]
    [SerializeField] private GameObject[] spawnObjects;

    [Header("Plyer Components")] 
    [SerializeField] private PlayerController controller;
    [SerializeField] private CharacterController charControl;
    [SerializeField] private PlayerManager playerManager;

    [Header("Next Scene")]
    [SerializeField] private int nextSceneNum;


    void Start()
    {  
        if(hasOutroCutscene)
        {
            outroCutscene.gameObject.SetActive(false);
        }
        levelStart();
    }

    void levelStart()
    {
        if(hasIntroCutscene)
        {
            StartCoroutine(playIntroCutscene(introLength));
        }
        else
        {
            for (int i = 0; i < spawnObjects.Length; i++)
            {
                spawnObjects[i].SetActive(true);
            }
            introCutscene.gameObject.SetActive(false);
            
            playerManager.enabled = true;
            charControl.enabled = true;
            controller.enabled = true;
            controller.Activate();

        }
      
          
    }


    void skipCutscene()
    {
        introCutscene.gameObject.SetActive(false);

        playerManager.enabled = true;
        charControl.enabled = true;
        controller.enabled = true;
        controller.Activate();

        for (int i = 0; i < spawnObjects.Length; i++)
        {
            spawnObjects[i].SetActive(true);
        }
    }


    IEnumerator playIntroCutscene(float length)
    {
            yield return new WaitForSeconds(length);
            introCutscene.gameObject.SetActive(false);



            playerManager.enabled = true;
            charControl.enabled = true;
            controller.enabled = true;
            controller.Activate();

            for (int i = 0; i < spawnObjects.Length; i++)
            {
                spawnObjects[i].SetActive(true);
            }
    }

    public void EndLevel()
    {
        if(hasOutroCutscene)
        {
            StartCoroutine(playOutroCutscene(outroLength));
        }
        else
        {
            SceneManager.LoadScene(nextSceneNum);
        }
    }

    IEnumerator playOutroCutscene(float length)
    {
        outroCutscene.gameObject.SetActive(true);
        for (int i = 0; i < spawnObjects.Length; i++)
        {
            spawnObjects[i].SetActive(false);
        }
        playerManager.enabled = false;
        charControl.enabled = false;
        controller.enabled = false;
        controller.Deactivate();

        yield return new WaitForSeconds(length);
        SceneManager.LoadScene(nextSceneNum);

    }
}
