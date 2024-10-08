using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fishObjects; // Los objetos que representan los peces
    [SerializeField] private Transform corner1; // Primer vértice del área
    [SerializeField] private Transform corner2; // Segundo vértice del área
    [SerializeField] private Transform corner3; // Tercer vértice del área
    [SerializeField] private Transform corner4; // Cuarto vértice del área

    [SerializeField] private float moveSpeed = 1.5f; // Velocidad de movimiento de los peces
    [SerializeField] private float rotationSpeed = 2f; // Velocidad de rotación de los peces
    [SerializeField] private float changeDirectionInterval = 3f; // Intervalo para cambiar de dirección

    private Vector3[] targets; // Posiciones objetivo de los peces
    private float timer;

    void Start()
    {
        targets = new Vector3[fishObjects.Length];
        SetRandomTargets();
    }

    void Update()
    {
        SwimFish();
        timer += Time.deltaTime;

        if (timer >= changeDirectionInterval)
        {
            SetRandomTargets();
            timer = 0f;
        }
    }

    void SwimFish()
    {
        for (int i = 0; i < fishObjects.Length; i++)
        {
            // Suaviza la rotación del pez hacia el objetivo, limitando a los ejes X y Z
            Vector3 directionToTarget = (targets[i] - fishObjects[i].transform.position).normalized;

            // Solo rotar en los ejes X y Z (ignorar el eje Y)
            directionToTarget.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            fishObjects[i].transform.rotation = Quaternion.Slerp(fishObjects[i].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Mover el pez hacia la posición objetivo
            fishObjects[i].transform.position = Vector3.MoveTowards(fishObjects[i].transform.position, targets[i], moveSpeed * Time.deltaTime);
        }
    }

    void SetRandomTargets()
    {
        // Generar nuevas posiciones objetivo dentro del área delimitada
        for (int i = 0; i < fishObjects.Length; i++)
        {
            targets[i] = GetRandomPointInArea();
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
}
