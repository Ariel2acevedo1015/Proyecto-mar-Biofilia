using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public List<EventInstance> eventIntances;
    public static AudioManager instance { get; private set; }
    public EventInstance voiceEventInstance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Error, more than one instance in the scene");
        }
        instance = this;
        
        eventIntances = new List<EventInstance>();
       
        
       
        
        
    }

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "Menu":
                InitializeVoice(FmodEvents.instance.MenuVoice, this.transform.position);
                break;

            case "PrototipoManglar":
                InitializeVoice(FmodEvents.instance.Manglar, this.transform.position);
               
                break;

            case "Zone2":
                // InitializeVoice(FmodEvents.instance.Zone2Voice, this.transform.position);
                break;

            // Puedes agregar más zonas aquí según sea necesario
            default:
                Debug.LogWarning("No voice event for this scene.");
                break;
        }
    }

    private void Update()
    {
        
       
    }

    public void PlayOneShot(EventReference sound, Vector3 Worldpos)
    {
        RuntimeManager.PlayOneShot(sound, Worldpos);
    }

    public EventInstance CreateInstance( EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventIntances.Add(eventInstance);
        return eventInstance;
    }

    public void SetParameterByName(EventInstance eventInstance, string parameterName, float value)
    {
        if (eventInstance.isValid())
        {
            eventInstance.setParameterByName(parameterName, value);
        }
        else
        {
            Debug.LogError("El EventInstance no es válido o no se ha inicializado correctamente.");
        }
    }

    public void PauseAllAudio()
    {
        foreach (EventInstance eventInstance in eventIntances)
        {
            eventInstance.setPaused(true); // Pausar todas las instancias de audio
        }
    }

    public void ResumeAllAudio()
    {
        foreach (EventInstance eventInstance in eventIntances)
        {
            eventInstance.setPaused(false); // Reanudar todas las instancias de audio
        }
    }

    public void InitializeVoice( EventReference voiceEventReferent, Vector3 position)
    {
        voiceEventInstance = CreateInstance(voiceEventReferent);

        var attributes = RuntimeUtils.To3DAttributes(position);
        voiceEventInstance.set3DAttributes(attributes);

        voiceEventInstance.start();
    }

    public int GetTimelinePosition()
    {
        voiceEventInstance.getTimelinePosition(out int timelinePosition);
        return timelinePosition;
    }

    // Método para detener el audio cuando sea necesario
    public void StopAudio()
    {
        voiceEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        voiceEventInstance.release();
    }
    private void CleanUp()
    {
        foreach(EventInstance eventInstance in eventIntances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }


    private void OnDestroy()
    {
        CleanUp();
    }
}
