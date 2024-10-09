using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInteractionHandler : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerButtonAction; // Botón trigger en VR
    private XRSimpleInteractable currentInteractable; // Objeto interactuable dinámico

    // Delegado para suscribirse a la interacción
    public delegate void InteractionStartedHandler();
    public event InteractionStartedHandler OnInteractionStarted;

    public void SetInteractable(XRSimpleInteractable interactableObject)
    {
        // Configura el objeto interactuable dinámicamente
        currentInteractable = interactableObject;
        currentInteractable.selectEntered.AddListener(StartInteraction);
        currentInteractable.selectExited.AddListener(EndInteraction);
    }

    void Update()
    {
        if (currentInteractable != null && currentInteractable.isSelected &&
            triggerButtonAction.action.ReadValue<float>() > 0.1f)
        {
            OnInteractionStarted?.Invoke(); // Invoca la interacción cuando es válida
        }
    }

    private void StartInteraction(SelectEnterEventArgs args)
    {
        Debug.Log("Interacción iniciada con: " + currentInteractable.gameObject.name);
    }

    private void EndInteraction(SelectExitEventArgs args)
    {
        Debug.Log("Interacción terminada con: " + currentInteractable.gameObject.name);
    }
}
