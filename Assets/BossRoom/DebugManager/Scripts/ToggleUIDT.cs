using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIDT : DebugTool 
{
    [SerializeField]
    [Tooltip("Lista de gameobjects de elementos UI da cena")]
    private List<GameObject> uiEls = new List<GameObject>();
    private void Start()
    {
    }
    public override void Execute()
    {
        base.Execute();
        foreach (GameObject go in uiEls)
        {
            if (go != null)
            {
                go.SetActive(!go.activeSelf);
            }
        }
    }
}
