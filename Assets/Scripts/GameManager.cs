using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public float CostPlantationValue;
    public float CostPlantatipnCarbon;


    public float CostTreeValue;
    public float TreeCarbonValue;


    public event Action<int,bool> OnManageSoilwhithId;
    public event Action<int> OnChangeCamera;

    public enum GameStatus
    {
        selectTerrain, ManageTerrain, GameOver, Win
    }

    public GameStatus status;

    public List<GameObject> myTerrains; //lista que guarda seus terrenos
    public GameObject TempTerrain; // terreno temposrario q vai ser usado no raycast

    RaycastHit TempHit;

    public static GameManager Instance;
    // Start is called before the first frame update


    public bool isMenuActive = false; //esconder o canvas

    public float startMoney;
    public float currentMoney;
    public float MaxMoney;

    public float startCarbon;
    public float currentCarbon;
    public float maxCarbon;

    ////////////////////////////////////VAI VIRAR UI manager////////////////////////////
    public Canvas menuCanvas;

    public GameObject MainMenu;

    public GameObject ConfirmationMenu;

    public GameObject InfoMenu;
    Vector2 InfoMenScreenPoin;
    public Text InfoText;


    SoilBehavior soil;

    public Text Soil_ID;
    

    public Text OperationTxt;

    public Text MoneyTxt;
    public Text CarbonTxt;

    ///////////////////////////////////////////////////////////////////////////////////

    int MainMenuOption;


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
        currentMoney = startMoney;
        currentCarbon = startCarbon;

        status = GameStatus.selectTerrain;
        ConfirmationMenu.SetActive(false);
        InfoMenu.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        menuCanvas.enabled = isMenuActive;

        Clique();

        //Debug.Log(MainMenuOption);
        UpdateCreditsValues();

        LimitCarbonControll();
        LimitMoneyControll();
    }

    private void LimitCarbonControll()
    {
        if (currentCarbon <= -100)
            currentCarbon = -100;
        if (currentCarbon >= 100)
            currentCarbon = 100;
    }

    private void LimitMoneyControll()
    {
        if (currentMoney <= 0)
            currentMoney = 0;
    }


    private void UpdateCreditsValues()
    {
        CarbonTxt.text = currentCarbon.ToString();
        MoneyTxt.text = currentMoney.ToString();
    }

    private void Clique()
    {
  
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !isMenuActive) //verifica se clicou com o mouse e nao esta em cima de um GameObject
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            ////posicao menu de acordo com o clique do mouse
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f; //distance of the plane from the camera
            MainMenu.GetComponent<RectTransform>().position = screenPoint;
            InfoMenScreenPoin = screenPoint;
            /////

            InfoMenu.SetActive(false);/// desabilita infoBox

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("soil"))
                {
                    isMenuActive = true;
                    hit.transform.GetComponent<MeshRenderer>().enabled = true;
                    TempHit = hit;// para ser usado em cancelOperation

                    soil = hit.collider.gameObject.GetComponent<SoilBehavior>(); //pega o objeto Soil via raycast

                    if (soil != null) Soil_ID.text = soil.TerrenoId.ToString();// atribui a UI o valor do texto///////////////////////////////////////////////UI

                    TempTerrain = hit.collider.gameObject;

                }

            }

        }
    }

    public void cancelSelectionOperation()//////////////////////////////////////////////////////////////////////////////////////////////////////////////////UI
    {
        isMenuActive = false;

        TempHit.transform.GetComponent<MeshRenderer>().enabled = false;
    }

    public void cancelInfo()//////////////////////////////////////////////////////////////////////////////////////////////////////////////////UI
    {
        InfoMenu.SetActive(false);
    }


    public void BuyTerrain(int _option)
    {
        OperationTxt.text = "compra";
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
    }

    public void SellTerrain(int _option)
    {
        OperationTxt.text = "venda";
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
       
    }

    public void ManageTerrain(int _option)
    {
        MainMenuOption = _option;///verificar se usa

        if (myTerrains.Count==0)
        {
            ShowInfo("Voce nao possui terrenos");
        }
        //verificar se o terreno e seu (se esta na lista)
        foreach (var item in myTerrains)
        {
            //Debug.Log("entrou no loop");
            SoilBehavior itemLista = item.GetComponent<SoilBehavior>();// terreno q esta na lista ou nao
            SoilBehavior temp = TempTerrain.GetComponent<SoilBehavior>();// terreno temporario q veio via raycast qndo clicou
            
            if(itemLista!=null)// tem terreno na lista e voce clicou em um terreno
            {
                if ((itemLista.TerrenoId == temp.TerrenoId) )//terreno e seu
                {
                    status = GameStatus.ManageTerrain;
                    //Debug.Log("gerenciou");/////////////////////////

                    
                    ////passar como parametro de um evento o id do terreno q se esta gerenciando
                    if (OnChangeCamera != null) //// CHAMDA DO ENVENTO
                        OnChangeCamera(itemLista.TerrenoId);//passa como parametro o id para trocar a camera
                    cancelSelectionOperation();// desabilita main menu
                    break;

                }
                else
                {
                    ShowInfo("esse terreno nao e seu");
                    //Debug.Log("esse terreno nao e seu else 1");
                }
            }
        }
    
    }

    ///////////////////////////////////////////
    public void ConfirmOperation()// Compra e Venda
    {


        if (MainMenuOption == 1) {//COMPRA    verificar se tem grana e se esta disponivel
            if (soil.isAvaiable && currentMoney>=soil.price)
            {
                GameObject terrain = new GameObject();// cria um novo gameObject
                terrain = TempTerrain;// pega o GameObject q veio do raycast
                myTerrains.Add(terrain);//adiciona o terreno criado na lista de terrenos

                TempTerrain = null;
                //evento para mudar o valor de disponibilidade depois da compra
                if (OnManageSoilwhithId != null) //// CHAMDA DO ENVENTO
                    OnManageSoilwhithId(soil.TerrenoId,false);//passa como parametro o id para soilBehavior para deixar indisponivel

                currentMoney -= soil.price;//retira o valor do terreno do current money



                ConfirmationMenu.SetActive(false);
                cancelSelectionOperation();// desabilita main menu
            }
            else
            {
                if(currentMoney < soil.price)
                {
                    ShowInfo("Voce nao tem dinheiro");
                }
                else
                {
                    ShowInfo("Terreno indisponivel");
                }
                
                //Debug.Log("Indisponivel");
                return;

            }


        }
        if (MainMenuOption == 2)// venda ////
        {
            foreach (var item in myTerrains)
            {
                SoilBehavior tempSoil = item.GetComponent<SoilBehavior>();
                if (tempSoil != null)
                {
                    if(tempSoil.TerrenoId == soil.TerrenoId && !soil.isAvaiable)
                    {
                        Debug.Log("Vendeu");
                        currentMoney += soil.price;
                        //evento para mudar o valor de disponibilidade depois da venda
                        if (OnManageSoilwhithId != null) //// CHAMDA DO ENVENTO
                            OnManageSoilwhithId(soil.TerrenoId, true);//passa como parametro o id para soilBehavior para deixar disponivel

                        ShowInfo("Venda Realizada com sucesso!!");
                        myTerrains.Remove(item);// remove o terreno da lista

                        ConfirmationMenu.SetActive(false);
                        cancelSelectionOperation();// desabilita main menu
                        return;
                    }

                    
                }
                
            }
            ShowInfo("Esse terreno nao e seu!!"); //varreu toda a lista e nao encontrou
            return;
        }

    }

    public void ShowInfo(string _info)
    {
        InfoMenu.SetActive(true);
        InfoMenu.GetComponent<RectTransform>().position = InfoMenScreenPoin;
        InfoText.text = _info;
        //isMenuActive = true;
      
    }

    public void NotConfirmOperation()
    {
        ConfirmationMenu.SetActive(false);
    }

    public void addCarbon(float amount)
    {
        currentCarbon += amount;
    }

    public void RemoveCarbon(float amount)
    {
        currentCarbon -= amount;
    }

    public void addMoney(float amount)
    {
        currentMoney += amount;
    }

    public void RemoveMoney(float amount)
    {
        currentMoney -= amount;
    }
}
