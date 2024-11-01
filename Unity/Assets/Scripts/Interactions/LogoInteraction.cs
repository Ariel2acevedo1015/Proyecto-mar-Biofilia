using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LogoInteraction : MonoBehaviour
{
    [SerializeField] private GameObject fog;
    [SerializeField] private VRInteractionHandler interactionHandler;
    [SerializeField] private Animator Animator;
    [SerializeField] private MangroveInteraction mangrove;
    [SerializeField] private GameObject spiralLeaves;
    [SerializeField] private string sceneName;
    //[SerializeField] private MangroveInteraction mangrove;
    private bool hasInteracted = false;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        XRSimpleInteractable interactable = GetComponentInChildren<XRSimpleInteractable>();
        interactionHandler.AddInteractable(interactable);
        interactionHandler.OnInteractionStarted += HandleRunaInteraction;
    }

    private void HandleRunaInteraction(XRSimpleInteractable interactable)
    {
        if (hasInteracted) return;
       
        //GetComponentIn<Outline>().enabled = false;
        GetComponent<XRSimpleInteractable>().enabled = false;
        GetComponent<Collider>().enabled = false;
        spiralLeaves.SetActive(true);
        fog.GetComponent<ParticleSystem>().Stop();
        Animator.SetBool("Active", true);
        //fog.SetActive(false);
        mangrove.enabled=true;
        mangrove.startInteraction = true;
        hasInteracted = true;
        AudioManager.instance.PlayZoneAudio(sceneName);
    }


    private void OnDestroy()
    {
        // Limpiar la suscripción al destruir el objeto
        interactionHandler.OnInteractionStarted -= HandleRunaInteraction;
    }
}