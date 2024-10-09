using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ManglarTypesInteractions : MonoBehaviour
{
    
    [SerializeField] private GameObject[] manglares; // Leñoso, Arbustivo, Denso, Colorado
    [SerializeField] AudioManager audioInstance;
    [SerializeField] InputActionProperty rightGripAction;

    public bool hasGrabbed = false;
    private bool isPlaying = false;
    private float timer = 0f;
  


    // Parámetros de tiempo para iluminar manglares
    private float lightUpTime1 = 18.476f;  // Leñoso
    private float lightUpTime2 = 21.914f;  // arbustivo
    private float lightUpTime3 = 23.835f;  // denso
    private float lightUpTime4 = 26.347f;  // colorado

  

    //private EventInstance manglart;
    // Inicialización
    void Start()
    {
        
        isPlaying = false;
        //hasGrabbed = true;
        timer = 0f;
    }

    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;


            if (timer >= lightUpTime2 && timer < lightUpTime3)
            {
                LightUpManglar(manglares[1], "Arbustivo", true);
                LightUpManglar(manglares[0], "Leñoso", false);
                
            }
            else if (timer >= lightUpTime3 && timer < lightUpTime4)
            {
                LightUpManglar(manglares[2], "Denso", true);
                LightUpManglar(manglares[1], "arbustivo", false);
              
            }
            else if (timer >= lightUpTime4)
            {
                LightUpManglar(manglares[3], "Colorado", true);
                LightUpManglar(manglares[2], "denso", false);
            }

            
            if (timer >= lightUpTime3 + 9f)  // Ejemplo: 10 segundos después del último evento
            {
                //StopAudioAndReset();
                LightUpManglar(manglares[3], "Colorado", false);
            }
        }

        else
        {
            timer += Time.deltaTime;
            //float currentTime = audioInstance.GetTimelinePosition() / 1000f;  // Convertir a segundos
            print(timer);

            // Lógica de iluminación en función del tiempo
            if (timer >= lightUpTime1 )
            {
                //audioInstance.CreateInstance(FmodEvents.instance.Manglar2);

                LightUpManglar(manglares[0], "Leñoso", true);
                manglares[0].GetComponent<XRSimpleInteractable>().enabled = true;
                //CheckGripButton();
                // manlgart.setParameterByName("TypesManglar", 19.476f);
                // Solo interactuar si el GripButton es presionado y aún no ha interactuado
                if (hasGrabbed)
                {
                    audioInstance.SetGlobalParameter(FmodEvents.instance.TypesManglar.Name, 1);
                   // FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FmodEvents.instance.TypesManglar.Name, 1);
                    //audioInstance.InitializeVoice(FmodEvents.instance.Manglar2, this.transform.position);
                    timer = 19.5f;
                    isPlaying = true;
                    manglares[0].GetComponent<XRSimpleInteractable>().enabled = false;
                }
            }
        }

    }
    // Verifica si el GripButton fue presionado
    public void CheckGripButton()
    {
        //if (rightGripAction.action.ReadValue<float>() > 0.1f) 
            hasGrabbed = true;

    }

    private void LightUpManglar(GameObject manglar, string type, bool active)
    {
        Outline manglarOutline = manglar.GetComponent<Outline>();
        if (manglarOutline != null && active)
        {
            manglarOutline.enabled = true;
           // Debug.Log("Iluminando manglar: " + type);
        }
        else if (!active)
        {
            manglarOutline.enabled = false;
        }
    }

    private void StopAudioAndReset()
    {
        audioInstance.StopAudio();
        //audioInstance.release();
       // isPlaying = false;
    }
}
