using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private float openAngle = -90f;
    [SerializeField] private float openSpeed = 90f;

    private bool canInteract = true;
    private bool open = false;

    private Quaternion closedRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
    }

    public void Interact(Transform t_interactor)
    {
        if (canInteract)
        {
            canInteract = false;

            if (!open)
            {
                targetRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
                StartCoroutine(RotateDoor(targetRotation, true));
            }
            else
            {
                StartCoroutine(RotateDoor(closedRotation, false));
            }
        }
    }

    private IEnumerator RotateDoor(Quaternion toRotation, bool opening)
    {
        while (Quaternion.Angle(transform.rotation, toRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, openSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = toRotation;
        open = opening;
        canInteract = true;
    }
}