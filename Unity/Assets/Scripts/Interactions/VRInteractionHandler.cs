using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInteractionHandler : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerButtonAction; // Bot�n trigger en VR
    private XRSimpleInteractable currentInteractable; // Objeto interactuable din�mico

    // Delegado para suscribirse a la interacci�n
    public delegate void InteractionStartedHandler();
    public event InteractionStartedHandler OnInteractionStarted;

    public void SetInteractable(XRSimpleInteractable interactableObject)
    {
        // Configura el objeto interactuable din�micamente
        currentInteractable = interactableObject;
        currentInteractable.selectEntered.AddListener(StartInteraction);
        currentInteractable.selectExited.AddListener(EndInteraction);
    }

    void Update()
    {
        if (currentInteractable != null && currentInteractable.isSelected &&
            triggerButtonAction.action.ReadValue<float>() > 0.1f)
        {
            OnInteractionStarted?.Invoke(); // Invoca la interacci�n cuando es v�lida
        }
    }

    private void StartInteraction(SelectEnterEventArgs args)
    {
        Debug.Log("Interacci�n iniciada con: " + currentInteractable.gameObject.name);
    }

    private void EndInteraction(SelectExitEventArgs args)
    {
        Debug.Log("Interacci�n terminada con: " + currentInteractable.gameObject.name);
    }
}
