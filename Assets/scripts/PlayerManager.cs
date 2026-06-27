using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public PlayerController Dylan;
    bool DylanActive = true;

    public PlayerController Darragh;
    bool DarraghActive = false;
    public bool canSwitch = true;


    [SerializeField] private Camera UIcam;
    void Update()
    {
        if (canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Q) && canSwitch)
            {

                Switch();
            }
        }
    }

    public void Switch()
    {
        if(DylanActive == true && Dylan.isGrounded)
        {
            Darragh.Activate();
            DarraghActive = true;
            Vector3 changedPos = new Vector3(Darragh.transform.position.x, Darragh.transform.position.y + 0.8f, Darragh.transform.position.z);
            UIcam.transform.SetPositionAndRotation(changedPos, Darragh.transform.rotation);
            UIcam.transform.SetParent(Darragh.transform);

            DylanActive = false;
            Dylan.Deactivate();
        }
        else if(DarraghActive == true && Darragh.isGrounded)
        {
            Dylan.Activate();
            DylanActive = true;
            Vector3 changedPos = new Vector3(Dylan.transform.position.x, Dylan.transform.position.y + 0.8f, Dylan.transform.position.z);
            UIcam.transform.SetPositionAndRotation(changedPos, Dylan.transform.rotation);
            UIcam.transform.SetParent(Dylan.transform);

            DarraghActive = false;
            Darragh.Deactivate();
        }

    }

    public void Downed()
    {
        if (Darragh.isDowned && Dylan.isDowned)
        {
            SceneManager.LoadScene(5);
            return;
        }

        if (canSwitch)
        {
            HandleSwitching();
        }
    }

    private void HandleSwitching()
    {
        if (Dylan.isDowned && !Darragh.isDowned)
        {
            Darragh.Activate();
            DarraghActive = true;

            Vector3 changedPos = Darragh.transform.position + new Vector3(0, 0.8f, 0);
            UIcam.transform.SetPositionAndRotation(changedPos, Darragh.transform.rotation);
            UIcam.transform.SetParent(Darragh.transform);

            DylanActive = false;
            Dylan.Deactivate();
            canSwitch = false;
        }
        else if (Darragh.isDowned && !Dylan.isDowned)
        {
            Dylan.Activate();
            DylanActive = true;

            Vector3 changedPos = Dylan.transform.position + new Vector3(0, 0.8f, 0);
            UIcam.transform.SetPositionAndRotation(changedPos, Dylan.transform.rotation);
            UIcam.transform.SetParent(Dylan.transform);

            DarraghActive = false;
            Darragh.Deactivate();
            canSwitch = false;
        }
    }
}
