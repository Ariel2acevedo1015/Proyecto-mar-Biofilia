using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MangroveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] manglares; // Le�oso, Arbustivo, Denso, Colorado
    [SerializeField] private VRInteractionHandler interactionHandler; // Nuevo handler de interacciones
    [SerializeField] private AudioManager audioInstance;
    [SerializeField] private GameObject growthSeed;
    [SerializeField] private GameObject seed;

    private bool isPlaying = false;
    private bool interactionCompleted = false; // Control de interacci�n
    private float timer = 0f;

    // Par�metros de tiempo para iluminar manglares
    private float lightUpTime1 = 18.476f;  // Le�oso
    private float lightUpTime2 = 21.914f;  // Arbustivo
    private float lightUpTime3 = 23.835f;  // Denso
    private float lightUpTime4 = 26.347f;  // Colorado

    private FMOD.Studio.PARAMETER_ID mangroveParameterID;

    void Start()
    {
        mangroveParameterID = new FMOD.Studio.PARAMETER_ID
        {
            data1 = 0xadcd059d,
            data2 = 0x2b84f46b
        };

        // Configurar las interacciones para cada manglar usando el VRInteractionHandler
        foreach (var manglar in manglares)
        {
            
        }
        interactionHandler.AddInteractable(manglares[0].GetComponent<XRSimpleInteractable>());
        // Suscribirse al evento de interacci�n
        interactionHandler.OnInteractionStarted += OnMangroveInteractionStarted;
    }

    void Update()
    {
        if (!interactionCompleted)
        {
            timer += Time.deltaTime;

            if (timer >= lightUpTime1)
            {
                LightUpManglar(manglares[0], "Le�oso", true);
                manglares[0].GetComponent<XRSimpleInteractable>().enabled = true;
                manglares[0].GetComponent<Collider>().enabled = true;
                interactionCompleted = true; // Evitar que se repita
            }
        }

        if (isPlaying)
        {
            PlayInteractionSequence();
        }
    }

    // M�todo invocado cuando una interacci�n comienza
    private void OnMangroveInteractionStarted(XRSimpleInteractable interactable)
    {
        // Verificar qu� manglar ha sido interactuado y empezar la secuencia
        if (manglares[0].GetComponent<XRSimpleInteractable>() == interactable)
        {
            StartInteractionSequence();
        }
    }

    // M�todo para iniciar la secuencia de interacci�n
    private void StartInteractionSequence()
    {
        // Reseteamos el timer e iniciamos la secuencia
        timer = 19.5f;
        isPlaying = true;

        // Iniciar el audio asociado al segundo manglar
        audioInstance.InitializeVoice(FmodEvents.instance.Manglar2, this.transform.position);

        // Desactivar el primer manglar una vez interactuado
        manglares[0].GetComponent<XRSimpleInteractable>().enabled = false;
        manglares[0].GetComponent<Collider>().enabled = false;

        // Remover la interacci�n del primer manglar
        interactionHandler.RemoveInteractable(manglares[0].GetComponent<XRSimpleInteractable>());

        Debug.Log("Interacci�n activada");
    }

    // M�todo que maneja la secuencia de interacci�n de manglares
    private void PlayInteractionSequence()
    {
        timer += Time.deltaTime;

        // Cambia el manglar iluminado seg�n el tiempo
        if (timer >= lightUpTime2 && timer < lightUpTime3)
        {
            LightUpManglar(manglares[1], "Arbustivo", true);
            LightUpManglar(manglares[0], "Le�oso", false);
        }
        else if (timer >= lightUpTime3 && timer < lightUpTime4)
        {
            LightUpManglar(manglares[2], "Denso", true);
            LightUpManglar(manglares[1], "Arbustivo", false);
        }
        else if (timer >= lightUpTime4)
        {
            LightUpManglar(manglares[3], "Colorado", true);
            LightUpManglar(manglares[2], "Denso", false);
            growthSeed.SetActive(true);
        }

        // Finalizar la interacci�n y activar las semillas
        if (timer >= lightUpTime3 + 9f)
        {
            LightUpManglar(manglares[3], "Colorado", false);
            interactionHandler.RemoveInteractable(manglares[0].GetComponent<XRSimpleInteractable>()); // Remover la interacci�n
            seed.SetActive(true);
            Destroy(this); // Termina la secuencia de interacci�n
        }
    }

    // M�todo auxiliar para iluminar los manglares
    private void LightUpManglar(GameObject manglar, string type, bool active)
    {
        Outline manglarOutline = manglar.GetComponent<Outline>();
        if (manglarOutline != null && active)
        {
            manglarOutline.enabled = true;
        }
        else if (!active)
        {
            manglarOutline.enabled = false;
        }
    }
}

