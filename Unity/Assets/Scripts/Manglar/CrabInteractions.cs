using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CrabInteractions : MonoBehaviour
{
    [SerializeField] private GameObject crab; // El cangrejo
    [SerializeField] private Outline crabOutline; // Outline del cangrejo
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al VRInteractionHandler
    [SerializeField] private AudioManager audioInstance; // Audio Manager para reproducir los sonidos
    [SerializeField] private GameObject fisherman; // Pescador que aparece
    [SerializeField] private GameObject fish; // Peces que aparecer�n
    [SerializeField] private GameObject trash; // Basura que aparecer�
    [SerializeField] private GameObject boat; // Bote con outline

    private bool hasGrabbed = false;
    private bool audioPlayed = false;
   // private bool isPlaying = false;
    private float activationtime = 24.181f; // Tiempo para activar el pescador

    private XRSimpleInteractable crabInteractable;

    void Start()
    {
        // Obtener el interactuable del cangrejo
        crabInteractable = crab.GetComponent<XRSimpleInteractable>();

        if (crabInteractable != null && interactionHandler != null)
        {
            // A�adir la interacci�n con el cangrejo al VRInteractionHandler
            interactionHandler.AddInteractable(crabInteractable);

            // Suscribirse al evento de interacci�n
            interactionHandler.OnInteractionStarted += OnCrabInteractionStarted;
        }
        else
        {
            Debug.LogError("Error: Falta alguna referencia en CrabInteractions.");
        }
    }

    void Update()
    {
        HandleTimedActions();
    }

    // M�todo invocado cuando se interact�a con el cangrejo
    private void OnCrabInteractionStarted(XRSimpleInteractable interactable)
    {
        if (interactable == crabInteractable && !hasGrabbed)
        {
            StartCrabInteraction();
        }
    }

    private void StartCrabInteraction()
    {
        // Iniciar el audio cuando se interact�a con el cangrejo
        audioInstance.InitializeVoice(FmodEvents.instance.Manglar4, crab.transform.position);
        //audioInstance.PlayOneShot(FmodEvents.instance.Manglar3, crab.transform.position);
        crabOutline.enabled = false; // Desactivar el outline del cangrejo
        crab.GetComponent<XRSimpleInteractable>().enabled = false;
        crab.GetComponent<Collider>().enabled = false;
        // Remover la capacidad de interactuar con el cangrejo
        interactionHandler.RemoveInteractable(crabInteractable);
        hasGrabbed = true; // Marcar que la interacci�n fue completada
        audioPlayed = true; // Marcar que el audio est� en reproducci�n
    }

    private void HandleTimedActions()
    {
        if (hasGrabbed && audioPlayed)
        {
            float currentTime = audioInstance.GetTimelinePosition() / 1000f;
            print(currentTime);

            // Usamos if-else para manejar los eventos en funci�n del tiempo
            if (currentTime >= 10.951f && currentTime < activationtime)
            {
                // Activar el pescador si no se ha activado antes
                ActivateFisherman();
            }
            else if (currentTime >= activationtime && currentTime < 61.21f)
            {
                // Activar los peces en el momento de la interacci�n
                ActivateFish();
            }
            else if (currentTime >= 61.21f && currentTime < 73.20f)
            {
                // Activar la basura
                ActivateTrash();
            }
            else if (currentTime >= 73.20f)
            {
               // audioInstance.CreateInstance(FmodEvents.instance.Manglar4);
                audioInstance.InitializeVoice(FmodEvents.instance.Manglar5, this.transform.position);
                EndTrashInteraction();
            }
        }
    }

    // Funciones para gestionar cada acci�n
    private void ActivateFisherman()
    {
        if (!fisherman.activeInHierarchy)
        {
            fisherman.SetActive(true);
        }
    }

    private void ActivateFish()
    {
        if (!fish.activeInHierarchy)
        {
            fish.SetActive(true);
            boat.GetComponent<Outline>().enabled = false;
        }
    }

    private void ActivateTrash()
    {
        if (!trash.activeInHierarchy)
        {
            trash.SetActive(true);
            print("Basura activada");
        }
    }

    private void EndTrashInteraction()
    {
       // trash.GetComponent<Trashinteraction>().audioPlayed = false;
    }

    public void FisherManVoice()
    {
        audioInstance.InitializeVoice(FmodEvents.instance.Manglar5, this.transform.position);
    }

    void OnDestroy()
    {
        // Desuscribirse para evitar errores cuando se destruya el objeto
        if (interactionHandler != null)
        {
            interactionHandler.OnInteractionStarted -= OnCrabInteractionStarted;
        }
    }
}