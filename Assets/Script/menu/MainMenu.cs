using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void Campaign()
    {
        SceneManager.LoadScene("Test2");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

//    private void Update()
//    {
//        if (Input.GetKeyDown("f1"))
//        {
//            Cursor.lockState = CursorLockMode.Locked;
//            Cursor.visible = false;
//        }

//        if (Input.GetKeyDown("f2"))
//        {
//                Cursor.lockState = CursorLockMode.None;
//                Cursor.visible = true;
//        }
//    }
}
