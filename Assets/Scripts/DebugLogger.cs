using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] int maxLines = 50;
    [HideInInspector] public TextMeshPro debugLogText;

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

        if (queue.Count >= maxLines) queue.Dequeue();

        queue.Enqueue(logString);

        var builder = new StringBuilder();
        foreach (string st in queue)
        {
            builder.Append(st).Append("\n");
        }
        debugLogText.SetText(builder.ToString());
    }
}