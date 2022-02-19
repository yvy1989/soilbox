using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public LayerMask layermask_solo;
    public LayerMask layermask_UI;

    public static GameManager Instance;
    // Start is called before the first frame update
    public GameObject menu;

    public Canvas menuCanvas;

    public bool isMenuActive = false;


    private void Awake()
    {
        menuCanvas.enabled = isMenuActive;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menuCanvas.enabled = isMenuActive;
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //verifica se clicou com o mouse e nao esta em cima de um GameObject
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 400f, layermask_solo))
            {
                if (hit.transform.CompareTag("soil"))
                {
                    isMenuActive = !isMenuActive;

                }

            }

        }
    }

    
}
