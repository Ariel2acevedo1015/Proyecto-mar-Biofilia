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

    public void OnSelect()  // Este m�todo se llamar� cuando se seleccione la semilla (por ejemplo, usando raycasting o interacci�n VR)
    {
        seedGrowthManager.SelectSeed();  // Notificar al SeedGrowthManager cu�l semilla ha sido seleccionada
    }
}
