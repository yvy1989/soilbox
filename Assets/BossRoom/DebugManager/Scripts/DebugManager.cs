using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private bool isEnable = false;
    public List<DebugTool> toolList = new List<DebugTool>();
    // Start is called before the first frame update
    void Start()
    {
        isEnable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
            isEnable = !isEnable;
    }

    private void OnGUI()
    {
        if (isEnable)
        {
            GUI.Box(new Rect(10, 10, 400, 200+toolList.Count*20), "Loader Menu");
            int i = 1;
            foreach (var tool in toolList)
            {
                if (GUI.Button(new Rect(100, 20*i, 200, 20), tool.label))
                {
                    tool.Execute();
                }
                i++;
            }
        }
    }
}
