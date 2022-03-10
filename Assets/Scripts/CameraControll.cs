using UnityEngine;

/// <summary>
/// creditos https://satellasoft.com/artigo/unity-3d/camera-rts-com-a-unity-3d
/// newking777 
/// Responsavel por movimentar a camera e aplicar o zoom
/// </summary>

public class CameraControll : MonoBehaviour
{


    public RectTransform boxVisual;

    Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;


    public LayerMask clicavel;
    public LayerMask solo;


    public Camera[] MyCams;
    public int currentCamera = 0;

    public float tamanhoBorda = 25.0f;
    public float velocidade = 10.0f;

    private float screenX;
    private float screenY;

    [Header("Limites da camera no modo de selecao de terreno")]
    public float limHorizEsqSelecao;
    public float limHorizDirSelecao;
    public float limVertSupSelecao;
    public float limVertInfSelecao;

    [Header("Limites da camera no modo de gerencia de terreno")]
    public Vector3 limEsquerdo;
    public Vector3 limDireito;
    public Vector3 limSuperior;
    public Vector3 limInferior;




    void Start()
    {
        DrawVisual();
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;


        GameManager.Instance.OnChangeCamera += changeCam;

        MyCams[0].gameObject.SetActive(true);

        screenX = Screen.width;
        screenY = Screen.height;

        DisableCams(currentCamera);//desabilita todas as cameras exceto a primeira
    }



    void Update()
    {
        if (!GameManager.Instance.isMenuActive)
        {

        }

        MoveCam();
        zoomScroll();

        UnitClick();

        UnitDrag();

        if (GameManager.Instance.status == GameManager.GameStatus.ManageTerrain)
        {
            velocidade = 0.2f;
        }
        if (GameManager.Instance.status == GameManager.GameStatus.selectTerrain || currentCamera == 0)
        {
            velocidade = 2f;
        }
    }

    private void UnitDrag()
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

    private static void zoomScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 1)
            {
                Camera.main.fieldOfView--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView < 100)
            {
                Camera.main.fieldOfView++;
            }
        }
    }

    private void MoveCam()
    {
        Vector3 posicao = Vector3.zero;
        Debug.Log("x = "+MyCams[currentCamera].transform.position.x+"  z = "+ MyCams[currentCamera].transform.position.z);


        if (GameManager.Instance.status == GameManager.GameStatus.selectTerrain)//verifica se esta na tela de selecao de terreno//////////////////////////////////////
        {
            /////////movimento horizontal
            if (Input.mousePosition.x < tamanhoBorda && MyCams[currentCamera].transform.position.z >= limHorizEsqSelecao)
            {
                posicao.z = -1;
            }
            if (Input.mousePosition.x > (screenX - tamanhoBorda) && MyCams[currentCamera].transform.position.z <= limHorizDirSelecao)
            {
                posicao.z = 1;
            }
            /////////movimento vertical
            if (Input.mousePosition.y < 25 && MyCams[currentCamera].transform.position.x <= limVertInfSelecao)
            {
                posicao.x = 1;
            }

            if (Input.mousePosition.y > (screenY - tamanhoBorda) && MyCams[currentCamera].transform.position.x >= limVertSupSelecao)
            {
                posicao.x = -1;
            }
        }

        if (GameManager.Instance.status == GameManager.GameStatus.ManageTerrain)//verifica se esta na tela de gerenciamento//////////////////////////////////////////////////
        {
            /////////movimento horizontal
            if (Input.mousePosition.x < tamanhoBorda && MyCams[currentCamera].transform.position.x >= limEsquerdo.x && MyCams[currentCamera].transform.position.z >= limEsquerdo.z)
            {
                posicao.x = 1;
                posicao.z = -1;
            }
            if (Input.mousePosition.x > (screenX - tamanhoBorda) && MyCams[currentCamera].transform.position.x <= limDireito.x && MyCams[currentCamera].transform.position.z <= limDireito.z)
            {
                posicao.x = -1;
                posicao.z = 1;
            }
            /////////movimento vertical
            if (Input.mousePosition.y > (screenY - tamanhoBorda) && MyCams[currentCamera].transform.position.x >= limSuperior.x && MyCams[currentCamera].transform.position.z >= limSuperior.z)
            {
                posicao.x = -1;
                posicao.z = -1;
            }
            if (Input.mousePosition.y < 25 && MyCams[currentCamera].transform.position.x <= limInferior.x && MyCams[currentCamera].transform.position.z <= limInferior.z)
            {
                posicao.x = 1;
                posicao.z = 1;
            }

        }


        if (posicao == Vector3.zero)
            posicao = GetKeyPosition();

        MyCams[currentCamera].transform.localPosition +=  (posicao * velocidade * Time.deltaTime);


    }

    private Vector3 GetKeyPosition()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }



    private void DisableCams(int expetion)
    {
        for (int i = 0; i < MyCams.Length; i++)
        {
            if (i != expetion)
            {
                MyCams[i].gameObject.SetActive(false);
            }
            
        }
    }

    public void changeCam(int camTerrainID)
    {
        if (camTerrainID == 0)// se estiver na camera principal
        {
            GameManager.Instance.status = GameManager.GameStatus.selectTerrain;// garante q qndo apertar o botao voltar ele mude o status para selecao de terreno
        }
        DisableCams(camTerrainID);
        MyCams[camTerrainID].gameObject.SetActive(true);
        currentCamera = camTerrainID;

    }

    private void UnitClick()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = MyCams[currentCamera].ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clicavel))
            {
                //se podemos clicar no objeto

                //normal click e shif click
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                }

            }
            else
            {
                //se nao podemos e nao estamos com o shift apertado
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }

            }
        }
    }


    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //calculos x
        if (Input.mousePosition.x < startPosition.x)//arrastando esquerda
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
            if (selectionBox.Contains(MyCams[currentCamera].WorldToScreenPoint(unit.transform.position)))
            {
                //se as unidades estiverem no retangulo adicione as na lista
                UnitSelection.Instance.DragSelect(unit);
            }
        }
    }
}

