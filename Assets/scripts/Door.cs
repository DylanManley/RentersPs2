using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private float openAngle = -90f;
    [SerializeField] private float openSpeed = 90f;

    private bool canInteract = true;
    private bool open = false;
    [SerializeField] private bool locked = false;

    private Quaternion closedRotation;
    private Quaternion targetRotation;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private GameObject doorKey;

    private void Start()
    {
        closedRotation = transform.rotation;
    }

    void Update()
    {
        if (doorKey.activeInHierarchy == false)
        {
            locked = false;
        }
    }

    public void Interact(Transform t_interactor)
    {
        if (canInteract && !locked)
        {
            canInteract = false;

            if (!open)
            {
                targetRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
                StartCoroutine(RotateDoor(targetRotation, true));
                audioSource.clip = openSound;
                audioSource.Play();
            }
            else
            {
                StartCoroutine(RotateDoor(closedRotation, false));
                audioSource.clip = closeSound;
                audioSource.Play();
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