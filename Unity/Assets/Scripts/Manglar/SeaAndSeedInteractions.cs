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

    private float activationTime = 8f;
    private void Start()
    {
        Invoke("InitialiesVoice", 6f);
    }
    void Update()
    {
        HandleSeaRise();
        //CheckSeedActivation();
    }

    private void HandleSeaRise()
    {
        float currentTime = audioInstance.GetTimelinePosition() / 1000f;
        if (currentTime >= activationTime && currentTime < 14f)        {
            Vector3 newPosition = seaRise.transform.localPosition;
            newPosition.y = Mathf.Lerp(seaRise.transform.localPosition.y, 0.01f, Time.deltaTime/15);
            seaRise.transform.localPosition = newPosition;
        }
        else if(currentTime>=activationTime && currentTime ==15.2f)audioInstance.CleanUp();
        else if (currentTime >= activationTime && currentTime >= 15.3f)
        {
            CheckSeedActivation();
        }
    }
    private void InitialiesVoice()
    {
        audioInstance.InitializeVoice(FmodEvents.instance.Manglar3, this.transform.position);
    }

    private void CheckSeedActivation()
    {
        //float currentTime = audioInstance.GetTimelinePosition() / 1000f;
        //if (currentTime >= 20.559f && currentTime <= 24.1f)
       // {
            seedGrowth.StartSeeds();  // Activa las semillas cuando el tiempo es adecuado
            //audioInstance.InitializeVoice(FmodEvents.instance.Manglar31, this.transform.position);
        // }
    }
}

