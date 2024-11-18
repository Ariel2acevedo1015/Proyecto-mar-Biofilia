using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine;
using FMODUnity;
using static UnityEngine.XR.OpenXR.Features.Interactions.HandInteractionProfile;

public class MangroveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] manglares; // Leñoso, Arbustivo, Denso, Colorado
    [SerializeField] private GameObject[] uiMangles;
    [SerializeField] private GameObject growthSeed;
    [SerializeField] private GameObject seed;
    [SerializeField] private ParticleSystem spiralLeaves;
    [SerializeField] private AudioManager audioInstance;
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al controlador de parámetros

    public bool startInteraction = false;
    private bool  spiralend = false, interactionEnd = false, interactionCompleted = false;
    private float timer = 0f, time;
    private int currentManglarIndex = 0;
    private float[] lightUpTimes = { 18.876f, 21.914f, 23.835f, 26.347f };

    void Start()
    {

        foreach (GameObject manglar in manglares)
        {
            XRSimpleInteractable interactable = manglar.GetComponent<XRSimpleInteractable>();

            if (interactable != null)
            {
                interactionHandler.AddInteractable(interactable);
            }
            else
            {
                Debug.LogWarning($"El objeto {manglar.name} no tiene un componente XRSimpleInteractable.");
            }
        }
        interactionHandler.OnInteractionStarted += OnMangroveInteractionStarted;
    }

    void Update()
    {
        if (!interactionCompleted && startInteraction)
        {
            if (!spiralend) Invoke("SpiralParticules", 11f);
            timer += Time.deltaTime;

            if (currentManglarIndex == 0 && timer >= lightUpTimes[0])
            {
                LightUpManglar(manglares[0], true);
                EnableManglarInteraction(manglares[0]);
                interactionCompleted = true;
            }
        }

        if (currentManglarIndex == 4)
        {
            print(currentManglarIndex);

            time += Time.deltaTime;
            //print(time);
            if (time > 5f) interactionEnd = true;
        }

        if (interactionEnd)
        {
            foreach (GameObject ui in uiMangles) { ui.SetActive(false); }

            Destroy(this);
        }
        

    }
    public void MangroveIndex(int index)
    {
        if (index == currentManglarIndex)
        {
            StartInteractionSequence();
        }
    }
    public void OnMangroveInteractionStarted(XRSimpleInteractable interactable)
    {
        MangroveIndex(currentManglarIndex);
    }

    private void StartInteractionSequence()
    {
        //if (currentManglarIndex == 0) uiStart = true;
        timer = lightUpTimes[currentManglarIndex];

        switch (currentManglarIndex)
        {
            case 0: audioInstance.InitializeVoice(FmodEvents.instance.Manglar21, this.transform.position); break;
            case 1: audioInstance.InitializeVoice(FmodEvents.instance.Manglar22, this.transform.position); break;
            case 2: audioInstance.InitializeVoice(FmodEvents.instance.Manglar23, this.transform.position); break;
            case 3: audioInstance.InitializeVoice(FmodEvents.instance.Manglar24, this.transform.position); break;
        }


        DisableManglarInteraction(manglares[currentManglarIndex]);

        currentManglarIndex++;

        if (currentManglarIndex < manglares.Length)
        {
            LightUpManglar(manglares[currentManglarIndex], true);
            EnableManglarInteraction(manglares[currentManglarIndex]);
        }
        else
        {
            growthSeed.SetActive(true);
            seed.SetActive(true);
        }
    }
    private void SpiralParticules()
    {
        spiralLeaves.Stop();
        spiralend = true;
    }
    private void EnableManglarInteraction(GameObject manglar)
    {
        //if (uiStart) uiMangles[currentManglarIndex].SetActive(true);
        manglar.GetComponent<XRSimpleInteractable>().enabled = true;
        manglar.GetComponent<Collider>().enabled = true;
        //manglar.GetComponent<StudioGlobalParameterTrigger>().enabled = true;
    }

    private void DisableManglarInteraction(GameObject manglar)
    {

       // if (uiStart) uiMangles[currentManglarIndex].SetActive(false);
        manglar.GetComponent<XRSimpleInteractable>().enabled = false;
        manglar.GetComponent<Collider>().enabled = false;
        //manglar.GetComponent<StudioGlobalParameterTrigger>().enabled = false;
        LightUpManglar(manglar, false);
    }

    private void LightUpManglar(GameObject manglar, bool active)
    {
        Outline manglarOutline = manglar.GetComponent<Outline>();
        if (manglarOutline != null)
        {
            manglarOutline.enabled = active;
        }
    }

    void OnDestroy()
    {
        audioInstance.CleanUp();
       
        // Desuscribirse para evitar errores cuando se destruya el objeto
        if (interactionHandler != null)
        {
            interactionHandler.OnInteractionStarted -= OnMangroveInteractionStarted;

        }
    }
}