using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// creditos https://satellasoft.com/artigo/unity-3d/camera-rts-com-a-unity-3d
/// newking777 
/// Responsavel por movimentar a camera e aplicar o zoom
/// </summary>

public class CameraControll : MonoBehaviour
{
    public float tamanhoBorda = 25.0f;
    public float velocidade = 10.0f;

    private float screenX;
    private float screenY;

    public float limiteHorizontalEsquerdo;
    public float limiteHorizontalDireito;

    public float limiteVerticalSuperior;
    public float limiteVerticalInferior;



    void Start()
    {
        screenX = Screen.width;
        screenY = Screen.height;
    }

    void Update()
    {
        if (!GameManager.Instance.isMenuActive)
        {
            MoveCam();
            zoomScroll();
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

        if (Input.mousePosition.x < tamanhoBorda && transform.position.z >= limiteHorizontalEsquerdo)
        {
            posicao.z = -1;
        }
        if (Input.mousePosition.x > (screenX - tamanhoBorda) && transform.position.z <= limiteHorizontalDireito)
        {
            posicao.z = 1;
        }

        if (Input.mousePosition.y < 25 && transform.position.x <= limiteVerticalInferior)
            posicao.x = 1;

        if (Input.mousePosition.y > (screenY - tamanhoBorda) && transform.position.x >= limiteVerticalSuperior)
            posicao.x = -1;

        if (posicao == Vector3.zero)
            posicao = GetKeyPosition();

        transform.Translate(posicao * velocidade * Time.deltaTime);


    }

    private Vector3 GetKeyPosition()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    
}
