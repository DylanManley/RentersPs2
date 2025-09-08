using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public enum InteractableType
{
    Door,
    AreaDoor
}

public class AItriggers : MonoBehaviour
{
    [SerializeField] private Transform interactableTransform;
    [SerializeField] private InteractableType type;
    [SerializeField] private float timeBetween;
    private GuardBehaviour aiScript;
    private Interactable interactable;
    private NavMeshAgent agent;
    public bool PlayerInTrigger;


    private void Start()
    {
        switch (type)
        {
            case InteractableType.Door:
                interactable = interactableTransform.GetComponent<Door>();
                break;

            case InteractableType.AreaDoor:
                interactable = interactableTransform.GetComponent<AreaDoor>();
                break;

            default:
                Debug.LogWarning("Unknown interactable type!");
                break;
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInTrigger = true;
        }
        else
        {
            PlayerInTrigger = false;
        }

        if(other.CompareTag("NPC"))
        {
            aiScript = other.GetComponent<GuardBehaviour>();
            if (aiScript.canInteract && !PlayerInTrigger)
            {  
               interactable.Interact(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        aiScript = other.GetComponent<GuardBehaviour>();
        aiScript.canInteract = false;
        StartCoroutine(disabledTime());

        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = false;
        }
    } 

    private IEnumerator disabledTime()
    {
        yield return new WaitForSeconds(timeBetween);
        aiScript.canInteract = true;
    }

}
