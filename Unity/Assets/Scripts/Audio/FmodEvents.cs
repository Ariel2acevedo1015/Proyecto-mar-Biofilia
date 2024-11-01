using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FmodEvents : MonoBehaviour
{
    public static FmodEvents instance { get; private set; }


    [field:Header("MenuVoice")]
    [field: SerializeField] public EventReference MenuVoice {  get; private set; }

    [field: Header("ButtonSFX")]
    [field: SerializeField] public EventReference buttonselected { get;private set; }
    
    [field: Header("Manglar")]
    [field: SerializeField] public EventReference Manglar { get; private set; }
    
    [field: Header("Manglar2")]
    [field: SerializeField] public ParamRef TypesManglar { get; private set; }
    [field: SerializeField] public EventReference Manglar2 { get; private set; }
    [field: SerializeField] public EventReference Manglar21 { get; private set; }
    [field: SerializeField] public EventReference Manglar22 { get; private set; }
    [field: SerializeField] public EventReference Manglar23 { get; private set; }
    [field: SerializeField] public EventReference Manglar24 { get; private set; }
    
    [field: Header("Manglar3")]
    [field: SerializeField] public EventReference Manglar3 { get; private set; }
    [field: SerializeField] public EventReference Manglar31 { get; private set; }
    [field: Header("Manglar4")]
    [field: SerializeField] public EventReference Manglar4 { get; private set; }
    [field: SerializeField] public EventReference Manglar41 { get; private set; }

    [field: Header("Manglar5")]
    [field: SerializeField] public EventReference Manglar5 { get; private set; }





    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance in the escene");
        }

        instance = this;     
    }
}
