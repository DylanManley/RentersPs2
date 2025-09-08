using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform introCutscene;
    [SerializeField] private Transform OutroCutscene;
    
    [SerializeField] private bool hasIntroCutscene = false;
    [SerializeField] private bool hasOutroCutscene = false;

    [SerializeField] private float introLength;
    [SerializeField] private float OutroLength;

    [SerializeField] private GameObject[] spawnObjects;
    [SerializeField] private PlayerController controller;
    [SerializeField] private CharacterController charControl;
    [SerializeField] private PlayerManager playerManager;

    void Start()
    {   
        levelStart();
    }

    void levelStart()
    {
        if(hasIntroCutscene)
        {
            StartCoroutine(playCutscene(introLength));
        }
        else
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


    IEnumerator playCutscene(float length)
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
}
