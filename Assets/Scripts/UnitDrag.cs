using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    public RectTransform boxVisual;

    Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        DrawVisual();
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //quando clica
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
            
        }
        //quando arrasta
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
        //quando solta
        if (Input.GetMouseButtonUp(0))
        {
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
            SelectUnits();
        }

    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x) , Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {   
        //calculos x
        if(Input.mousePosition.x < startPosition.x)//arrastando esquerda
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;

        }
        else//arrastando p direita
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        //calculos y
        if (Input.mousePosition.y < startPosition.y)//arrastando baxio
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else//arrastando p cima
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        //loop em toda unidades
        foreach (var unit in UnitSelection.Instance.unitList)
        {
            //se as unidades estiverem dentro do retangulo
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                //se as unidades estiverem no retangulo adicione as na lista
                UnitSelection.Instance.DragSelect(unit);
            }
        }
    }
}
