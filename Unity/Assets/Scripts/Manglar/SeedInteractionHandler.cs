using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SeedInteractionHandler : MonoBehaviour
{
    //[SerializeField] private VRInteractionHandler interactionHandler;
    [SerializeField] private SeedGrowthManager seedGrowthManager; // Referencia al SeedGrowthManager
    [SerializeField] private AudioManager audioInstance;
  //  private XRSimpleInteractable interactable;

    void Start()
    {
       // interactable = GetComponent<XRSimpleInteractable>();

      //  if (interactable != null && interactionHandler != null)
       // {
            // Configura la interacción para esta semilla
          //  interactionHandler.AddInteractable(interactable);

            // Suscribirse al evento específico para esta semilla
          //  interactionHandler.OnInteractionStarted += OnSeedInteractionStarted;
     //   }
      //  else
      //  {
       //     Debug.LogError("Error: Falta alguna referencia en SeedInteractionHandler.");
      //  }
    }

    // Método manejador de la interacción con la semilla
    private void OnSeedInteractionStarted(XRSimpleInteractable selectedInteractable)
    {
        // Verificar si el interactuable seleccionado es la semilla
       // if (selectedInteractable == interactable)
       // {
          //  HandleInteraction(); // Iniciar la interacción
        //}
    }

    public void HandleInteraction()
    {
        // Llamamos al método de SeedGrowthManager para iniciar el crecimiento cuando la semilla es seleccionada
        seedGrowthManager.SelectSeed();
        audioInstance.InitializeVoice(FmodEvents.instance.Manglar31, this.transform.position);
        // Remover la interacción después de que ocurra
       // interactionHandler.RemoveInteractable(interactable);
    }

    void OnDestroy()
    {
        // Desuscribirse para evitar errores cuando se destruya el objeto
        //interactionHandler.OnInteractionStarted -= OnSeedInteractionStarted;
    }
}

