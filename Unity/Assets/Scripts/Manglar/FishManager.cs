using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FishManager : MonoBehaviour
{
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al VRInteractionHandler
    [SerializeField] private GameObject[] fishUIs; // Ventanas de informaci�n para cada pez
    [SerializeField] private GameObject[] fishObjects; // Array de peces con los que se puede interactuar
    [SerializeField] private GameObject trash; // Array de peces con los que se puede interactuar
    [SerializeField] private GameObject trashs; // Basura que aparecer�
    [SerializeField] private int maxInteractions = 2; // N�mero m�ximo de interacciones permitidas por pez
    [SerializeField] private AudioManager audioManager;
    private int[] fishInteractionCounts; // Contador por pez para rastrear interacciones
    private bool fishInteractionEnd = false;
    SkyRotation skyRotation;
    void Start()
    {
        trash.SetActive(false);
        // Inicializar contadores y desactivar todas las UIs al inicio
        fishInteractionCounts = new int[fishObjects.Length];
        foreach (var fishUI in fishUIs)
        {
            fishUI.SetActive(false);
        }

        // Iterar sobre cada pez y agregarlo al VRInteractionHandler
        for (int i = 0; i < fishObjects.Length; i++)
        {
            XRSimpleInteractable interactable = fishObjects[i].GetComponent<XRSimpleInteractable>();
            if (interactable != null)
            {
                interactionHandler.AddInteractable(interactable);
            }
            else
            {
                Debug.LogWarning($"El objeto {fishObjects[i].name} no tiene un componente XRSimpleInteractable.");
            }
        }

        // Suscribirse al evento de interacci�n
        interactionHandler.OnInteractionStarted += HandleInteraction;
    }
    private void Update()
    {
        float currentTime = audioManager.GetTimelinePosition() / 1000f;

        if(currentTime==41.953f && fishInteractionEnd) { trash.SetActive(true); }
    }
    private void HandleInteraction(XRSimpleInteractable interactable)
    {
        // Obtener el �ndice del pez interactuado
        int fishIndex = GetFishIndex(interactable);
        if (fishIndex == -1) return; // No es un pez v�lido

        // Verificar si ya alcanz� el l�mite de interacciones
        if (fishInteractionCounts[fishIndex] >= maxInteractions) return;

        // Incrementar el contador de interacciones del pez
        fishInteractionCounts[fishIndex]++;

        // Mostrar la UI asociada al pez
        fishUIs[fishIndex].SetActive(true);

        // Deshabilitar la interacci�n con el pez despu�s de que alcance el m�ximo permitido
        if (fishInteractionCounts[fishIndex] >= maxInteractions)
        {
            DisableFishInteraction(fishObjects[fishIndex]);
        }

        // Ocultar la UI despu�s de un tiempo
        Invoke(nameof(HideInfoUI), 10f);
    }

    private int GetFishIndex(XRSimpleInteractable interactable)
    {
        // Obtener el �ndice del pez interactuado
        for (int i = 0; i < fishObjects.Length; i++)
        {
            if (fishObjects[i].GetComponent<XRSimpleInteractable>() == interactable)
            {
                return i;
            }
        }
        return -1;
    }

    public void HideInfoUI()
    {
        foreach (var fishUI in fishUIs)
        {
            fishUI.SetActive(false);
        }
    }

    public void ActivatedAudio()
    {
        audioManager.ResumeAllAudio();
    }


    public void PauseAudio()
    {
        audioManager.PauseAllAudio();
    }
    public void ActivateTrash()
    {
        if (!trashs.activeInHierarchy)
        {
            skyRotation.SkyChange();
            audioManager.InitializeVoice(FmodEvents.instance.Manglar62, this.transform.position);
            trashs.SetActive(true);
            print("Basura activada");
        }
    }
    public void FinishUIinteraction()
    {
        fishInteractionEnd = true;
    }
    private void DisableFishInteraction(GameObject fish)
    {
        XRSimpleInteractable fishInteractable = fish.GetComponent<XRSimpleInteractable>();
        if (fishInteractable != null)
        {
            fishInteractable.enabled = false;
            fish.GetComponent<Outline>().enabled = false;
            fish.GetComponent<Collider>().enabled = false;
            interactionHandler.RemoveInteractable(fishInteractable);
        }
    }

    private void OnDestroy()
    {
        // Limpiar la suscripci�n al destruir el objeto
        interactionHandler.OnInteractionStarted -= HandleInteraction;
    }
}
