using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DebugTool : MonoBehaviour
{
    public string label;
    public string description;
    public string toolName;

    public virtual void Execute()
    {

    }
}
