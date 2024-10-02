using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashinteraction : MonoBehaviour
{

    [SerializeField] AudioManager audioInstance;
    [SerializeField] TrashAndFishTransition revert;
    

    private bool grabbed = false;
    public bool audioPlayed = true;
    // Start is called before the first frame update
    void Start()
    {
        audioInstance.CreateInstance(FmodEvents.instance.Manglar4);
        audioPlayed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (grabbed)
        //{
        
            if (!audioPlayed)
            {
               // audioInstance.CreateInstance(FmodEvents.instance.Manglar4);
                audioInstance.InitializeVoice(FmodEvents.instance.Manglar4, this.transform.position);
                audioPlayed = true;
                //
            }
            
        //}
    }

    public void TrashSelected()
    {
        revert.GetComponent<TrashAndFishTransition>().isMovingUp = false;
    }
}
