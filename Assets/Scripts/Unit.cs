using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSelection.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }
}
