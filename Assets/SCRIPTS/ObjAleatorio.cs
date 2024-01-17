using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAleatorio : MonoBehaviour
{
    public GameObject[] objetosAEncontrar;
    private GameObject objetoActual;
    private ParticleSystem efectoParticulas;
    private int puntos = 0;

    public float tiempoDestacado = 2f;
    private float tiempoRestanteDestacado = 0f;
    private bool objetoDestacado = false;

    void Start()
    {
        SeleccionarNuevoObjeto();
        efectoParticulas = objetoActual.GetComponentInChildren<ParticleSystem>();
        if (efectoParticulas == null)
        {
            Debug.LogError("El objeto actual no tiene un sistema de partículas.");
        }
    }

    void Update()
    {
        if (objetoDestacado)
        {
            tiempoRestanteDestacado -= Time.deltaTime;
            if (tiempoRestanteDestacado <= 0f)
            {
                RestaurarObjeto();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objetoActual)
        {
            SumarPuntos();
            SeleccionarNuevoObjeto();
        }
    }

    void SeleccionarNuevoObjeto()
    {
        objetoActual = objetosAEncontrar[Random.Range(0, objetosAEncontrar.Length)];
        Debug.Log("Encuentra: " + objetoActual.name);
        efectoParticulas = objetoActual.GetComponentInChildren<ParticleSystem>();
        if (efectoParticulas == null)
        {
            Debug.LogError("El objeto actual no tiene un sistema de partículas.");
        }
        DestacarObjeto();
    }

    void DestacarObjeto()
    {
        if (efectoParticulas != null)
        {
            efectoParticulas.Play();
        }
        objetoDestacado = true;
        tiempoRestanteDestacado = tiempoDestacado;
    }

    void RestaurarObjeto()
    {
        if (efectoParticulas != null)
        {
            efectoParticulas.Stop();
            efectoParticulas.Clear();
        }
        objetoDestacado = false;
        SeleccionarNuevoObjeto();
    }

    void SumarPuntos()
    {
        puntos++;
        Debug.Log("Puntos: " + puntos);
    }

    // Nueva función para obtener el puntaje actual
    public int ObtenerPuntos()
    {
        return puntos;
    }
}
