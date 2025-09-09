using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController Dylan;
    bool DylanActive = true;
    [SerializeField] private GameObject DylanWeapon;

    public PlayerController Darragh;
    bool DarraghActive = false;
    [SerializeField] private GameObject DarraghWeapon;
    private bool canPlay = false;
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
            DarraghWeapon.SetActive(true);
            DylanWeapon.SetActive(false);

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
            DarraghWeapon.SetActive(false);
            DylanWeapon.SetActive(true);

            DarraghActive = false;
            Darragh.Deactivate();
        }

    }

    public void Downed()
    {

        if (Darragh.isDowned && Dylan.isDowned)
        {
            //gameOver
            Debug.Log("Game Over");
        }
        else if (Dylan.isDowned && !Darragh.isDowned)
        {
            Darragh.Activate();
            DarraghActive = true;
            Vector3 changedPos = new Vector3(Darragh.transform.position.x, Darragh.transform.position.y + 0.8f, Darragh.transform.position.z);
            UIcam.transform.SetPositionAndRotation(changedPos, Darragh.transform.rotation);
            UIcam.transform.SetParent(Darragh.transform);
            DarraghWeapon.SetActive(true);
            DylanWeapon.SetActive(false);

            DylanActive = false;
            Dylan.Deactivate();

            
            canSwitch = false;
        }
        else if(Darragh.isDowned && !Dylan.isDowned)
        {
            Dylan.Activate();
            DylanActive = true;
            Vector3 changedPos = new Vector3(Dylan.transform.position.x, Dylan.transform.position.y + 0.8f, Dylan.transform.position.z);
            UIcam.transform.SetPositionAndRotation(changedPos, Dylan.transform.rotation);
            UIcam.transform.SetParent(Dylan.transform);
            DarraghWeapon.SetActive(false);
            DylanWeapon.SetActive(true);

            DarraghActive = false;
            Darragh.Deactivate();

            canSwitch = false;
        }
    }
}
