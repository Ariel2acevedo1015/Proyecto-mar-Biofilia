using UnityEngine;

public class SeedFall : MonoBehaviour
{
    [SerializeField] private Transform pointA; // Punto inicial de ca�da
    [SerializeField] private Transform pointB; // Punto final de ca�da
    [SerializeField] private float fallSpeed = 2f; // Velocidad de ca�da

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = pointA.position;
        endPosition = pointB.position;
        transform.position = startPosition; // Inicia en punto A
    }

    void Update()
    {
        // Mueve la semilla hacia abajo desde punto A a punto B
        transform.position = Vector3.MoveTowards(transform.position, endPosition, fallSpeed * Time.deltaTime);

        // Si llega al punto B, regresa instant�neamente a punto A
        if (transform.position == endPosition)
        {
            transform.position = startPosition;
        }
    }
}
