using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrackingObject : MonoBehaviour
{
    [SerializeField]  public GameObject objectToTrack;
    [SerializeField]  public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        if (objectToTrack == null || targetObject == null)
        {
            Debug.LogError("objectToTrack or targetObject is not assigned.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObjectTracked()
    {
        // Get the location of the tracked object
        targetObject.transform.position = objectToTrack.transform.position;
        targetObject.transform.rotation = objectToTrack.transform.rotation;
        Invoke("SetPositionTracked", 0.3f);
    }

    void SetPositionTracked()
    {
        Debug.Log("SetNewPositionForTrackingObject!");
        //targetObject.transform.position = objectToTrack.transform.position;
    }


}
