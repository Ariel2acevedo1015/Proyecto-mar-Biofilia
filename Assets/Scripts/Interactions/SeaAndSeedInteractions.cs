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
    [SerializeField] AudioManager audioInstance;
    
    [field: Header("Manglar2t")]
    [field: SerializeField] public EventReference Manglar2t { get; private set; }
    
    //private EventInstance manglart;
    //private float timer = 0f;
    private float activationtime = 17.781f;
    SeedGrowthManager seedGrowth;
    void Start()
    {
         //manglart = audioInstance.CreateInstance(Manglar2t);
        seedGrowth= GetComponent<SeedGrowthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = audioInstance.GetTimelinePosition() / 1000f;
        //print(currentTime);
        //timer += Time.deltaTime;
        if(currentTime >= activationtime )
        {
            Vector3 newPosition = seaRise.transform.localPosition;

            newPosition.y = math.lerp(seaRise.transform.localPosition.y, 0.01f, Time.deltaTime);

            seaRise.transform.localPosition = newPosition;

            if (currentTime >= 23.859f && currentTime<=24.1f)
            {
                seedGrowth.StartSeeds();
            }
            
        }
    }
}
