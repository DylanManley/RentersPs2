using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void StartDemo()
    {
        SceneManager.LoadScene("House");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
