using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    RaycastHit TempHit;

    public static GameManager Instance;
    // Start is called before the first frame update
    public GameObject menu;

    public Canvas menuCanvas;
    public Dropdown menuCascata;

    public bool isMenuActive = false; //esconder o canvas



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
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !isMenuActive) //verifica se clicou com o mouse e nao esta em cima de um GameObject
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            ////posicao menu de acordo com o clique do mouse
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f; //distance of the plane from the camera
            menuCascata.GetComponent<RectTransform>().position = screenPoint;
            /////



            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("soil"))
                {
                    isMenuActive = true;
                    hit.transform.GetComponent<MeshRenderer>().enabled = true;
                    TempHit = hit;
                    /// TODO solo

                }

            }

        }
    }

    public void cancelSelectionOperation()
    {
        isMenuActive = false;
        TempHit.transform.GetComponent<MeshRenderer>().enabled = false;
    }

    
}
