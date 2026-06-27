using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AreaDoor : MonoBehaviour, Interactable
{
    [SerializeField] private Transform tpArea;
    private bool canInteract = true;
    [SerializeField] private AudioSource sound;
    [SerializeField] private bool locked = false;
    [SerializeField] private GameObject doorKey;
    public void Interact(Transform t_interactor)
    {
        if (canInteract && !locked)
        {
            canInteract = false;
            transform.Rotate(0, -10, 0, Space.World);
            StartCoroutine(EnterArea(t_interactor));
            sound.Play();
        }
    }

    private IEnumerator EnterArea(Transform t_interactor)
    {
        yield return new WaitForSeconds(0.3f);
        CharacterController controller = t_interactor.GetComponent<CharacterController>();
        if(controller != null)
        {
            controller.enabled = false;
            t_interactor.SetPositionAndRotation(tpArea.position, tpArea.rotation);
            controller.enabled = true;
        }
        else
        {
            NavMeshAgent aiController = t_interactor.GetComponent<NavMeshAgent>();
            aiController.enabled = false;
            t_interactor.SetPositionAndRotation(tpArea.position, tpArea.rotation);
            aiController.enabled = true;
        }

        yield return new WaitForSeconds(0.3f);
        transform.Rotate(0, 10, 0, Space.World);
        canInteract = true;
    }

    void Update()
    {
        if (doorKey.activeInHierarchy == false)
        {
            locked = false;
        }
    }
}
