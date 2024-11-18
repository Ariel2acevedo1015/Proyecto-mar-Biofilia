using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaPath : MonoBehaviour
{
    [SerializeField] private string pathname;
    [SerializeField] private float time;
    void Start()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(pathname), "easetype", iTween.EaseType.easeInOutSine, "time", time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
