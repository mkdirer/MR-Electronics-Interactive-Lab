using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrackingObject : MonoBehaviour
{
    [SerializeField]  public GameObject objectToTrack;
    [SerializeField]  public GameObject targetObject;

    void Start()
    {
        if (objectToTrack == null || targetObject == null)
        {
            Debug.LogError("objectToTrack or targetObject is not assigned.");
            return;
        }
    }

    public void OnObjectTracked()
    {
        targetObject.transform.position = objectToTrack.transform.position;
        targetObject.transform.rotation = objectToTrack.transform.rotation;
        Invoke("SetPositionTracked", 0.3f);
    }

    void SetPositionTracked()
    {
        Debug.Log("SetNewPositionForTrackingObject!");
        targetObject.transform.position = objectToTrack.transform.position;
    }


}
