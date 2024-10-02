using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAndFishTransition : MonoBehaviour
{
    [SerializeField] private Transform objectToMoveUp;     // Objeto que subirá
    [SerializeField] private Transform objectToMoveDown;   // Objeto que bajará
    [SerializeField] private Transform upTargetTransform;  // Transform objetivo hacia arriba
    [SerializeField] private Transform downTargetTransform;// Transform objetivo hacia abajo
    [SerializeField] private float transitionDuration = 2f;  // Duración de la transición en segundos
    [SerializeField] private bool loop = true;             // Si queremos que la transición sea cíclica

    private Vector3 initialUpPosition;   // Posición inicial del objeto que subirá
    private Vector3 initialDownPosition; // Posición inicial del objeto que bajará
    private float elapsedTime = 0f;      // Tiempo transcurrido para la interpolación
    public bool isMovingUp = true;      // Controla si los objetos están en la fase de subida/bajada

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

        // Calcular el porcentaje completado de la transición
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

        // Si la transición ha finalizado
        if (t >= 1f)
        {
            // Reiniciar el tiempo transcurrido
            //elapsedTime = 0f;

            // Si el loop está activo, alternar la dirección
            if (loop)
            {
                isMovingUp = !isMovingUp;
            }
        }
    }
}
