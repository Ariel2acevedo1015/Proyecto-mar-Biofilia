using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FishManager : MonoBehaviour
{
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al VRInteractionHandler
    [SerializeField] private GameObject infoUI; // Ventana de información que se mostrará al interactuar
    [SerializeField] private GameObject[] fishObjects; // Array de peces con los que se puede interactuar
    [SerializeField] private int maxInteractions = 2; // Número máximo de veces que se puede interactuar con cada pez
    private bool isInfoMenuActive = false;
    private int interactionCount = 0; // Contador de interacciones

    void Start()
    {
        // Desactivar la UI inicialmente
        infoUI.SetActive(false);

        // Iterar sobre cada pez y agregarlo al VRInteractionHandler
        foreach (GameObject fish in fishObjects)
        {
            XRSimpleInteractable interactable = fish.GetComponent<XRSimpleInteractable>();

            if (interactable != null)
            {
                interactionHandler.AddInteractable(interactable);
            }
            else
            {
                Debug.LogWarning($"El objeto {fish.name} no tiene un componente XRSimpleInteractable.");
            }
        }

        // Suscribirse al evento de interacción
        interactionHandler.OnInteractionStarted += HandleInteraction;
    }

    private void HandleInteraction(XRSimpleInteractable interactable)
    {
        // Verificar si el interactable es uno de los peces, para evitar que la interacción de basura se active aquí
        if (!IsFishInteractable(interactable)) return;

        if (isInfoMenuActive || interactionCount >= maxInteractions) return;

        // Incrementar el contador de interacciones
        interactionCount++;

        // Activar la ventana de información y establecer la bandera como true
        infoUI.SetActive(true);
        isInfoMenuActive = true;

        // Ocultar la ventana después de 10 segundos
        Invoke("HideInfoUI", 10f);

        // Si ya se alcanzó el límite de interacciones, deshabilitar la interacción en todos los peces
        if (interactionCount >= maxInteractions)
        {
            DisableAllFishInteractions();
        }
    }

    private bool IsFishInteractable(XRSimpleInteractable interactable)
    {
        // Verificar si el interactable es uno de los peces
        foreach (GameObject fish in fishObjects)
        {
            if (fish.GetComponent<XRSimpleInteractable>() == interactable)
            {
                return true; // Es un pez
            }
        }
        return false; // No es un pez
    }

    private void HideInfoUI()
    {
        infoUI.SetActive(false);
        isInfoMenuActive = false;
    }

    private void DisableAllFishInteractions()
    {
        // Desactivar la interacción en todos los peces
        foreach (GameObject fish in fishObjects)
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
    }

    private void OnDestroy()
    {
        // Limpiar la suscripción al destruir el objeto
        interactionHandler.OnInteractionStarted -= HandleInteraction;
    }
}
