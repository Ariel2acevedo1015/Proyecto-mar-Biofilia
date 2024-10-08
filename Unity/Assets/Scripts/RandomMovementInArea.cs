using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementInArea : MonoBehaviour
{
    [SerializeField] private GameObject[] movingObjects; // Los objetos que se mover�n
    [SerializeField] private Transform corner1; // Primer v�rtice del �rea
    [SerializeField] private Transform corner2; // Segundo v�rtice del �rea
    [SerializeField] private Transform corner3; // Tercer v�rtice del �rea
    [SerializeField] private Transform corner4; // Cuarto v�rtice del �rea

    [SerializeField] private float moveSpeed = 1.0f; // Velocidad de movimiento de los objetos
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotaci�n
    [SerializeField] private float changeDirectionInterval = 2f; // Tiempo para cambiar de direcci�n
   
    [SerializeField] GameObject cornerPoints;
    
    private Vector3[] targets; // Posiciones objetivo de los objetos
    private Quaternion[] targetRotations; // Rotaciones objetivo
    private float timer;

    void Start()
    {
        targets = new Vector3[movingObjects.Length];
        targetRotations = new Quaternion[movingObjects.Length];
        SetRandomTargetsAndRotations();
    }

    void Update()
    {
        MoveAndRotateObjects();
        timer += Time.deltaTime;
        cornerPoints.GetComponent<TrashAndFishTransition>().enabled = true;
        // Cambia la direcci�n y rotaci�n despu�s de un intervalo
        if (timer >= changeDirectionInterval)
        {
            SetRandomTargetsAndRotations();
            timer = 0f;
        }
    }

    void MoveAndRotateObjects()
    {
        for (int i = 0; i < movingObjects.Length; i++)
        {
            // Mover hacia la posici�n objetivo
            movingObjects[i].transform.position = Vector3.MoveTowards(movingObjects[i].transform.position, targets[i], moveSpeed * Time.deltaTime);

            // Rotar hacia la rotaci�n objetivo
            movingObjects[i].transform.rotation = Quaternion.RotateTowards(movingObjects[i].transform.rotation, targetRotations[i], rotationSpeed * Time.deltaTime);
        }
    }

    void SetRandomTargetsAndRotations()
    {
        // Generar nuevas posiciones y rotaciones objetivo dentro del �rea delimitada
        for (int i = 0; i < movingObjects.Length; i++)
        {
            targets[i] = GetRandomPointInArea();
            targetRotations[i] = GetRandomRotation();
        }
    }

    Vector3 GetRandomPointInArea()
    {
        // Encuentra el punto m�nimo y m�ximo en el �rea delimitada por los cuatro puntos
        Vector3 minBounds = new Vector3(
            Mathf.Min(corner1.position.x, corner2.position.x, corner3.position.x, corner4.position.x),
            Mathf.Min(corner1.position.y, corner2.position.y, corner3.position.y, corner4.position.y),
            Mathf.Min(corner1.position.z, corner2.position.z, corner3.position.z, corner4.position.z)
        );

        Vector3 maxBounds = new Vector3(
            Mathf.Max(corner1.position.x, corner2.position.x, corner3.position.x, corner4.position.x),
            Mathf.Max(corner1.position.y, corner2.position.y, corner3.position.y, corner4.position.y),
            Mathf.Max(corner1.position.z, corner2.position.z, corner3.position.z, corner4.position.z)
        );

        // Genera un punto aleatorio dentro de estos l�mites
        return new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            Random.Range(minBounds.z, maxBounds.z)
        );
    }

    Quaternion GetRandomRotation()
    {
        // Genera una rotaci�n aleatoria en el espacio 3D
        return Quaternion.Euler(
            Random.Range(0f, 360f), // Rotaci�n en el eje Y (horizontal)
            Random.Range(0f, 360f), // Rotaci�n en el eje X
            Random.Range(0f, 360f)  // Rotaci�n en el eje Z
        );
    }
}
