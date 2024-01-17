using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntUI : MonoBehaviour
{
    public TMP_Text puntos;
    public TMP_Text vidas;
    public TMP_Text velocidad;
    public TMP_Text tiempo;

    private float tiempoAux = 180;

    // Referencia al GameManager
    private ObjAleatorio gameManager;

    private void Start()
    {
        // Busca el GameManager en la escena (asegúrate de que el GameManager esté en algún objeto)
        gameManager = FindObjectOfType<ObjAleatorio>();

        if (gameManager == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
    }

    private void Update()
    {
        tiempoAux -= Time.deltaTime;

        // Actualiza el texto del tiempo
        string formatoTemp = "Tiempo: " + (int)tiempoAux;
        tiempo.text = formatoTemp;

        // Actualiza el texto del puntaje
        if (gameManager != null)
        {
            puntos.text = "Puntaje: " + gameManager.ObtenerPuntos();
        }
    }
}
