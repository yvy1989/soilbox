using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{
    public GameObject[] UnitPrefab_blueprint;

    public void spawn_Units_blueprint_ByIndex(int index)
    {
        Instantiate(UnitPrefab_blueprint[index]);// acessado via Button
    }
}
