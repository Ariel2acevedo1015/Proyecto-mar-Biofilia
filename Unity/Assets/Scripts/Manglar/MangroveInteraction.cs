using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MangroveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] manglares; // Leñoso, Arbustivo, Denso, Colorado
    [SerializeField] private VRInteractionHandler interactionHandler; // Nuevo handler de interacciones
    [SerializeField] private AudioManager audioInstance;
    [SerializeField] private GameObject growthSeed;
    [SerializeField] private GameObject seed;

    private bool isPlaying = false;
    private bool interactionCompleted = false; // Control de interacción
    private float timer = 0f;

    // Parámetros de tiempo para iluminar manglares
    private float lightUpTime1 = 18.476f;  // Leñoso
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
        // Suscribirse al evento de interacción
        interactionHandler.OnInteractionStarted += OnMangroveInteractionStarted;
    }

    void Update()
    {
        if (!interactionCompleted)
        {
            timer += Time.deltaTime;

            if (timer >= lightUpTime1)
            {
                LightUpManglar(manglares[0], "Leñoso", true);
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

    // Método invocado cuando una interacción comienza
    private void OnMangroveInteractionStarted(XRSimpleInteractable interactable)
    {
        // Verificar qué manglar ha sido interactuado y empezar la secuencia
        if (manglares[0].GetComponent<XRSimpleInteractable>() == interactable)
        {
            StartInteractionSequence();
        }
    }

    // Método para iniciar la secuencia de interacción
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

        // Remover la interacción del primer manglar
        interactionHandler.RemoveInteractable(manglares[0].GetComponent<XRSimpleInteractable>());

        Debug.Log("Interacción activada");
    }

    // Método que maneja la secuencia de interacción de manglares
    private void PlayInteractionSequence()
    {
        timer += Time.deltaTime;

        // Cambia el manglar iluminado según el tiempo
        if (timer >= lightUpTime2 && timer < lightUpTime3)
        {
            LightUpManglar(manglares[1], "Arbustivo", true);
            LightUpManglar(manglares[0], "Leñoso", false);
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

        // Finalizar la interacción y activar las semillas
        if (timer >= lightUpTime3 + 9f)
        {
            LightUpManglar(manglares[3], "Colorado", false);
            interactionHandler.RemoveInteractable(manglares[0].GetComponent<XRSimpleInteractable>()); // Remover la interacción
            seed.SetActive(true);
            Destroy(this); // Termina la secuencia de interacción
        }
    }

    // Método auxiliar para iluminar los manglares
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

