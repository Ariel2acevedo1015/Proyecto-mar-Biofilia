using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SeedInteractionHandler : MonoBehaviour
{
    [SerializeField] private VRInteractionHandler interactionHandler;
    [SerializeField] private SeedGrowthManager seedGrowthManager; // Referencia al SeedGrowthManager

    private XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        if (interactable != null && interactionHandler != null)
        {
            // Configura la interacci�n para esta semilla
            interactionHandler.AddInteractable(interactable);

            // Suscribirse al evento espec�fico para esta semilla
            interactionHandler.OnInteractionStarted += OnSeedInteractionStarted;
        }
        else
        {
            Debug.LogError("Error: Falta alguna referencia en SeedInteractionHandler.");
        }
    }

    // M�todo manejador de la interacci�n con la semilla
    private void OnSeedInteractionStarted(XRSimpleInteractable selectedInteractable)
    {
        // Verificar si el interactuable seleccionado es la semilla
        if (selectedInteractable == interactable)
        {
            HandleInteraction(); // Iniciar la interacci�n
        }
    }

    private void HandleInteraction()
    {
        // Llamamos al m�todo de SeedGrowthManager para iniciar el crecimiento cuando la semilla es seleccionada
        seedGrowthManager.SelectSeed();

        // Remover la interacci�n despu�s de que ocurra
        interactionHandler.RemoveInteractable(interactable);
    }

    void OnDestroy()
    {
        // Desuscribirse para evitar errores cuando se destruya el objeto
        interactionHandler.OnInteractionStarted -= OnSeedInteractionStarted;
    }
}
