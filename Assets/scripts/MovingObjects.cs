using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    [SerializeField] private Transform movingObject;
    [SerializeField] private Transform ogParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            ogParent = other.transform.parent;
            other.transform.SetParent(movingObject, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.transform.SetParent(movingObject, false);
            other.transform.SetParent(ogParent, true);
        }
    }
}
