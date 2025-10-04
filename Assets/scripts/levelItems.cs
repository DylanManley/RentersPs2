using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelItems : MonoBehaviour, Interactable
{
    [SerializeField] private LevelExit exitScript;
    [SerializeField] private bool heavyItem;

    void Start()
    {
        
    }

    public void Interact(Transform t_interactor)
    {
        PlayerController controller = t_interactor.GetComponent<PlayerController>();
        if (controller == null)
        {
            return;
        }

        if (heavyItem)
        {
            if (controller.encumbered)
            {
                return;
            }
            else
            {
                controller.encumbered = true;
            }
        }

        exitScript.itemsCollected++;
        this.gameObject.SetActive(false);

        if (heavyItem) 
        {
            AudioSource otherSource = t_interactor.GetComponent<AudioSource>();
            otherSource.clip = t_interactor.GetComponent<PlayerController>().heavyClip;
            otherSource.Play();
        }
    }
}
