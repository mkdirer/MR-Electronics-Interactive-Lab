using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Closing the application...");
        Application.Quit();
    }
}