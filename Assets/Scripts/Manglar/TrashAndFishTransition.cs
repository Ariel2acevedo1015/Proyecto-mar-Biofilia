using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAndFishTransition : MonoBehaviour
{
    [SerializeField] private Transform objectToMoveUp;     // Objeto que subir�
    [SerializeField] private Transform objectToMoveDown;   // Objeto que bajar�
    [SerializeField] private Transform upTargetTransform;  // Transform objetivo hacia arriba
    [SerializeField] private Transform downTargetTransform;// Transform objetivo hacia abajo
    [SerializeField] private float transitionDuration = 2f;  // Duraci�n de la transici�n en segundos
    [SerializeField] private bool loop = true;             // Si queremos que la transici�n sea c�clica

    private Vector3 initialUpPosition;   // Posici�n inicial del objeto que subir�
    private Vector3 initialDownPosition; // Posici�n inicial del objeto que bajar�
    private float elapsedTime = 0f;      // Tiempo transcurrido para la interpolaci�n
    public bool isMovingUp = true;      // Controla si los objetos est�n en la fase de subida/bajada

    void Start()
    {
        // Guardar las posiciones iniciales
        initialUpPosition = objectToMoveUp.position;
        initialDownPosition = objectToMoveDown.position;
    }

    void Update()
    {
        // Incrementar el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        // Calcular el porcentaje completado de la transici�n
        float t = Mathf.Clamp01(elapsedTime / transitionDuration);

        // Mover los objetos suavemente entre sus posiciones iniciales y finales
        if (isMovingUp)
        {
            // Objeto que sube
            objectToMoveUp.position = Vector3.Lerp(initialUpPosition, upTargetTransform.position, t);
            // Objeto que baja
            objectToMoveDown.position = Vector3.Lerp(initialDownPosition, downTargetTransform.position, t);
        }
        else
        {
            // Objeto que sube ahora baja, y el que bajaba ahora sube
            objectToMoveUp.position = Vector3.Lerp(upTargetTransform.position, initialUpPosition, t);
            objectToMoveDown.position = Vector3.Lerp(downTargetTransform.position, initialDownPosition, t);
        }

        // Si la transici�n ha finalizado
        if (t >= 1f)
        {
            // Reiniciar el tiempo transcurrido
            //elapsedTime = 0f;

            // Si el loop est� activo, alternar la direcci�n
            if (loop)
            {
                isMovingUp = !isMovingUp;
            }
        }
    }
}
