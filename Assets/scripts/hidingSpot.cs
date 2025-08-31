using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingSpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerscript = other.GetComponent<PlayerController>();

            if (playerscript.IsCrouching)
            {
                playerscript.hidden = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController playerscript = other.GetComponent<PlayerController>();

        if (playerscript.IsCrouching)
        {
            playerscript.hidden = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerscript = other.GetComponent<PlayerController>();
            playerscript.hidden = false;
        }
    }
}
