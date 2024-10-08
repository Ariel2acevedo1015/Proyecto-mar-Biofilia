using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeedGrowthManager : MonoBehaviour
{
    [SerializeField] private GameObject[] seeds;  
    [SerializeField] private GameObject tinyMangrove;  // El manglar en su fase pequeña
    [SerializeField] private GameObject smallMangrove;  // El manglar en su fase pequeña 2
    [SerializeField] private GameObject mediumMangrove;  // El manglar en su fase mediana
    [SerializeField] private GameObject largeMangrove;  // El manglar en su fase grande
    [SerializeField] InputActionProperty rightGripAction;
    [SerializeField] GameObject crab;
    private bool hasSelectedSeed = false;  // Bandera para verificar si ya se ha seleccionado una semilla

    void Start()
    {
        
        tinyMangrove.SetActive(false);
        smallMangrove.SetActive(false);
        mediumMangrove.SetActive(false);
        largeMangrove.SetActive(false);
    }

    void Update()
    {
        
        if (!hasSelectedSeed)
        {
            CheckSeedSelection();
        }
    }

    private void CheckSeedSelection()
    {
        if (rightGripAction.action.ReadValue<float>() > 0.1f) hasSelectedSeed = true;
    }

    public void StartSeeds()
    {
        seeds[0].SetActive(true);
        seeds[1].SetActive(true);
        seeds[2].SetActive(true);
        seeds[3].SetActive(true);
        seeds[4].SetActive(true);

    }
    public void SelectSeed()
    {
      
              //Debug.Log("Semilla seleccionada: " + selectedSeed.name);

            
            foreach (GameObject seed in seeds)
            {
                seed.SetActive(false);
            }

           
            StartGrowthSequence();
            hasSelectedSeed = true;  
    }

    private void StartGrowthSequence()
    {
        
        tinyMangrove.SetActive(true);

        
        StartCoroutine(GrowthCoroutine());
    }

    private IEnumerator GrowthCoroutine()
    {
        
        yield return new WaitForSeconds(2f); 

        
        
        tinyMangrove.SetActive(false);
        smallMangrove.SetActive(true);

        yield return new WaitForSeconds(2f);
        smallMangrove.SetActive(false);
        mediumMangrove.SetActive(true);

        yield return new WaitForSeconds(3f); 

        mediumMangrove.SetActive(false);
        largeMangrove.SetActive(true);
        yield return new WaitForSeconds(5f);
        crab.SetActive(true);
        largeMangrove.GetComponent<Outline>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
