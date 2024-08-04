using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Method called when a button is pressed or another action is performed
    public void Quit()
    {
        Debug.Log("Closing the application...");
        Application.Quit();
    }
}