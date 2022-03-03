using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// /TESTE DE CLIQUE
/// </summary>

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSelection.Instance.unitList.Add(this.gameObject);// ao iniciar a cena adiciona este objeto na lista do singleton
    }

    void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);// ao ser destruido remova este objeto da lista no singleton
    }
}
