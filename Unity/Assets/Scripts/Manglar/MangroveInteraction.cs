using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MangroveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] manglares; // Leñoso, Arbustivo, Denso, Colorado
    [SerializeField] private VRInteractionHandler interactionHandler;
    [SerializeField] private AudioManager audioInstance;

    private float timer = 0f;
    private bool isPlaying = false;

    // Parámetros de tiempo para iluminar manglares
    private float lightUpTime1 = 18.476f;  // Leñoso
    private float lightUpTime2 = 21.914f;  // Arbustivo
    private float lightUpTime3 = 23.835f;  // Denso
    private float lightUpTime4 = 26.347f;  // Colorado
   
    void Start()
    {
        // Configurar el manglar inicial como el objeto interactuable
        interactionHandler.SetInteractable(manglares[0].GetComponent<XRSimpleInteractable>());
        interactionHandler.OnInteractionStarted += StartInteractionSequence; // Suscribirse al evento
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lightUpTime1)
        {
            LightUpManglar(manglares[0], "Leñoso", true);
            manglares[0].GetComponent<XRSimpleInteractable>().enabled = true;
            manglares[0].GetComponent<Collider>().enabled = true;
        }

        if (isPlaying)
        {
            PlayInteractionSequence();
        }
    }

    private void StartInteractionSequence()
    {
        // Reseteamos el timer e iniciamos la secuencia
        timer = 19.5f;
        isPlaying = true;
        audioInstance.SetGlobalParameter(FmodEvents.instance.TypesManglar.Name, 1); // Iniciar el audio
        manglares[0].GetComponent<XRSimpleInteractable>().enabled = false;
        manglares[0].GetComponent<Collider>().enabled = false;
        // Ilumina el primer manglar al iniciar la interacción

    }

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
            isPlaying = false; // Termina la secuencia de interacción
        }
    }

    private void LightUpManglar(GameObject manglar, string type, bool active)
    {
        Outline manglarOutline = manglar.GetComponent<Outline>();
        if (manglarOutline != null)
        {
            manglarOutline.enabled = active;
        }
    }
}
