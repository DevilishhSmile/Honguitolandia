using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAleatorio : MonoBehaviour
{
    public GameObject[] objetosAEncontrar;
    private GameObject objetoActual;
    private ParticleSystem efectoParticulas;
    private int puntos = 0;

    public float tiempoDestacado = 2f; // Duración en segundos que el objeto estará destacado
    private float tiempoRestanteDestacado = 0f;
    private bool objetoDestacado = false;

    void Start()
    {
        // Inicializa el juego seleccionando un objeto al azar
        SeleccionarNuevoObjeto();

        // Obtén el sistema de partículas del objeto actual
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
                // Restaura el objeto a su estado original después del tiempo destacado
                RestaurarObjeto();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objetoActual)
        {
            // El jugador ha colisionado físicamente con el objeto correcto
            SumarPuntos();
            SeleccionarNuevoObjeto();
        }
    }

    void SeleccionarNuevoObjeto()
    {
        // Selecciona un objeto al azar de la lista
        objetoActual = objetosAEncontrar[Random.Range(0, objetosAEncontrar.Length)];
        Debug.Log("Encuentra: " + objetoActual.name);

        // Obtén el nuevo sistema de partículas del objeto actual
        efectoParticulas = objetoActual.GetComponentInChildren<ParticleSystem>();
        if (efectoParticulas == null)
        {
            Debug.LogError("El objeto actual no tiene un sistema de partículas.");
        }

        // Destaca el objeto visualmente
        DestacarObjeto();
    }

    void DestacarObjeto()
    {
        // Activa el sistema de partículas para reproducir el efecto
        if (efectoParticulas != null)
        {
            efectoParticulas.Play();
        }

        // Activa la bandera de objeto destacado
        objetoDestacado = true;

        // Establece el tiempo de duración del objeto destacado
        tiempoRestanteDestacado = tiempoDestacado;
    }

    void RestaurarObjeto()
    {
        // Detiene y reinicia el sistema de partículas
        if (efectoParticulas != null)
        {
            efectoParticulas.Stop();
            efectoParticulas.Clear();
        }

        // Desactiva la bandera de objeto destacado
        objetoDestacado = false;

        // Selecciona un nuevo objeto después de restaurar el actual
        SeleccionarNuevoObjeto();
    }

    void SumarPuntos()
    {
        puntos++;
        Debug.Log("Puntos: " + puntos);
    }
}
