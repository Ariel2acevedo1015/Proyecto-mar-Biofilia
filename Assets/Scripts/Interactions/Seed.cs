using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] SeedGrowthManager seedGrowthManager;

    void Start()
    {
        seedGrowthManager = GetComponentInParent<SeedGrowthManager>();  // Referencia al SeedGrowthManager
    }

    public void OnSelect()  // Este método se llamará cuando se seleccione la semilla (por ejemplo, usando raycasting o interacción VR)
    {
        seedGrowthManager.SelectSeed();  // Notificar al SeedGrowthManager cuál semilla ha sido seleccionada
    }
}
