using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject uiPopups;
    [SerializeField] Transform head;
    
    private float spawnDistance = 4f;
    private float elapsedTime = 0f;
    private bool isPositioningActive = true;

    private void Start()
    {
        //UiFollowPlayer.FollowCamera(uiPopups, head, spawnDistance);
        //this.transform.Rotate(0, 180, 0);
        //head = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {


        UiFollowPlayer.FollowCamera(uiPopups, head, spawnDistance);
        if (isPositioningActive)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < 1f)
            {
               
                isPositioningActive = false;
            }
           
        }
    }
}
