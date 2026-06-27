using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour, Interactable
{
    private Animator animator;
    private BoxCollider collider;

    [SerializeField] private GameObject lockBoxKey;
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
    }

    public void Interact(Transform t_interactor)
    {
        if (lockBoxKey.activeInHierarchy == false)
        {
            collider.enabled = false;
            animator.SetBool("open", true);
        }
    }

}
