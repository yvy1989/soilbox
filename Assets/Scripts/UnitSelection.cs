using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelection _instance;

    public static UnitSelection Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        
        if(_instance !=null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //seja unico
            _instance = this;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
        }
        else
        {
            unitSelected.Remove(unitToAdd);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {

    }

    public void DeselectAll()
    {
        unitSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {

    }

    public void selectAll(GameObject unitToSelect)
    {

    }
}
