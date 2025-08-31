using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, Interactable
{
    private Vector3 closedPos;
    private Vector3 openPos;

    [SerializeField] private float openX;
    [SerializeField] private float openY;
    [SerializeField] private float openZ;
    [SerializeField] private float moveDuration = 1f; // Duration in seconds

    private bool open = false;
    private bool isMoving = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + new Vector3(openX, openY, openZ);
    }

    public void Interact(Transform t_interactor)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDrawer(open ? closedPos : openPos));
            open = !open;
        }
    }

    private IEnumerator MoveDrawer(Vector3 targetPos)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
