using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class SeaAndSeedInteractions : MonoBehaviour
{

    [SerializeField] private Transform seaRise;
    [SerializeField] private AudioManager audioInstance;
    [SerializeField] private SeedGrowthManager seedGrowth;

    private float activationTime = 17.781f;

    void Update()
    {
        HandleSeaRise();
        CheckSeedActivation();
    }

    private void HandleSeaRise()
    {
        float currentTime = audioInstance.GetTimelinePosition() / 1000f;
        if (currentTime >= activationTime)
        {
            Vector3 newPosition = seaRise.transform.localPosition;
            newPosition.y = Mathf.Lerp(seaRise.transform.localPosition.y, 0.01f, Time.deltaTime/4);
            seaRise.transform.localPosition = newPosition;
        }
    }

    private void CheckSeedActivation()
    {
        float currentTime = audioInstance.GetTimelinePosition() / 1000f;
        if (currentTime >= 23.859f && currentTime <= 24.1f)
        {
            seedGrowth.StartSeeds();  // Activa las semillas cuando el tiempo es adecuado
        }
    }
}

