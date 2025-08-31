using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private bool itemCondition = false;
    [SerializeField] private int itemsNeeded = 0;
    public int itemsCollected = 0;
    public int characterCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            characterCount++;
            if (itemCondition == true)
            {
                if (itemsCollected >= itemsNeeded)
                {
                    Debug.Log("area Finished");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            characterCount--;
        }
    }
}
