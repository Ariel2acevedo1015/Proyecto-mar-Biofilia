using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CrabInteractions : MonoBehaviour
{

    [SerializeField] InputActionProperty rightGripAction;
    [SerializeField] GameObject crab;
    [SerializeField] AudioManager audioInstance;
    [SerializeField] GameObject fish;
    [SerializeField] GameObject trash;
    [SerializeField] GameObject pescador;
    [SerializeField] GameObject boat;


    private bool hasGrabbed;
    private bool audioPlayed = false;
    private float activationtime = 24.181f;
    private Outline outline;
    void Start()
    {
       // crab = GetComponentInChildren<GameObject>();
       
        audioInstance.CreateInstance(FmodEvents.instance.Manglar3);
     
        outline = crab.GetComponent<Outline>();
        hasGrabbed = false;
        pescador.SetActive(false);
        fish.SetActive(false);
        trash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetTrigger("Walk");
        
        //CheckForGripAction();
        
            //crab.SetActive(true);
        if (hasGrabbed && !audioPlayed)
        {
               // crab.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
          audioInstance.InitializeVoice(FmodEvents.instance.Manglar3, crab.transform.position);
          audioPlayed = true;
        }
        if (hasGrabbed && audioPlayed)
        {
            float currentTime = audioInstance.GetTimelinePosition() / 1000f;
           // print(currentTime);
            if(currentTime >= 12.121f)
            {
                pescador.SetActive(true);
            }

            if (currentTime >= activationtime)
            {
                fish.SetActive(true);
                boat.GetComponent<Outline>().enabled = false;   
            }
            if(currentTime>=61.21f)
            {
                trash.SetActive(true);
                print("Activado");
            }
            if (currentTime >= 73.20f)
            {
                trash.GetComponent<Trashinteraction>().audioPlayed = false;
            }
        }    
        
    }

    public void CheckForGripAction()
    {
        //if (rightGripAction.action.ReadValue<float>() > 0.1f) 
        hasGrabbed = true;
        outline.enabled = false;
        crab.GetComponent<XRSimpleInteractable>().enabled = false;
    }
}
