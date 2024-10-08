using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementInArea : MonoBehaviour
{
    [SerializeField] private GameObject[] movingObjects; // Los objetos que se moverán
    [SerializeField] private Transform corner1; // Primer vértice del área
    [SerializeField] private Transform corner2; // Segundo vértice del área
    [SerializeField] private Transform corner3; // Tercer vértice del área
    [SerializeField] private Transform corner4; // Cuarto vértice del área

    [SerializeField] private float moveSpeed = 1.0f; // Velocidad de movimiento de los objetos
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotación
    [SerializeField] private float changeDirectionInterval = 2f; // Tiempo para cambiar de dirección
   
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
        // Cambia la dirección y rotación después de un intervalo
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
            // Mover hacia la posición objetivo
            movingObjects[i].transform.position = Vector3.MoveTowards(movingObjects[i].transform.position, targets[i], moveSpeed * Time.deltaTime);

            // Rotar hacia la rotación objetivo
            movingObjects[i].transform.rotation = Quaternion.RotateTowards(movingObjects[i].transform.rotation, targetRotations[i], rotationSpeed * Time.deltaTime);
        }
    }

    void SetRandomTargetsAndRotations()
    {
        // Generar nuevas posiciones y rotaciones objetivo dentro del área delimitada
        for (int i = 0; i < movingObjects.Length; i++)
        {
            targets[i] = GetRandomPointInArea();
            targetRotations[i] = GetRandomRotation();
        }
    }

    Vector3 GetRandomPointInArea()
    {
        // Encuentra el punto mínimo y máximo en el área delimitada por los cuatro puntos
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

        // Genera un punto aleatorio dentro de estos límites
        return new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            Random.Range(minBounds.z, maxBounds.z)
        );
    }

    Quaternion GetRandomRotation()
    {
        // Genera una rotación aleatoria en el espacio 3D
        return Quaternion.Euler(
            Random.Range(0f, 360f), // Rotación en el eje Y (horizontal)
            Random.Range(0f, 360f), // Rotación en el eje X
            Random.Range(0f, 360f)  // Rotación en el eje Z
        );
    }
}
