using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

/*
{
    private TextMeshPro debugText;

    private void Start()
    {
        debugText = GetComponent<TextMeshPro>();
        if (debugText == null)
        {
            Debug.LogError("TextMeshPro component not found!");
        }
    }

    public void Log(string message)
    {
        if (debugText != null)
        {
            debugText.text += message + "\n";
        }
    }
}
*/

public class DebugLogger : MonoBehaviour
{
    [SerializeField] int maxLines = 50;
    [HideInInspector] public TextMeshPro debugLogText;
    //[SerializeField] TextMeshPro debugLogText;

    Queue<string> queue = new Queue<string>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        debugLogText = GetComponent<TextMeshPro>();

        // Delete oldest message
        if (queue.Count >= maxLines) queue.Dequeue();

        queue.Enqueue(logString);

        var builder = new StringBuilder();
        foreach (string st in queue)
        {
            builder.Append(st).Append("\n");
        }
        debugLogText.SetText(builder.ToString());
        //debugLogText.text = builder.ToString();
    }
}