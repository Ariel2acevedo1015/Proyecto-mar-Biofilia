using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowPlayer 
{
    public static void FollowCamera(GameObject uiCanvas,Transform cameraPosition, float spawnDistance)
    {
        uiCanvas.transform.position = cameraPosition.position + new Vector3(cameraPosition.forward.x, 0, cameraPosition.forward.z).normalized * spawnDistance;


        uiCanvas.transform.LookAt(new Vector3(cameraPosition.position.x, cameraPosition.transform.position.y, cameraPosition.position.z));
    }

    public static void FollowPlayerLooK(GameObject uiCanvas, Transform cameraPosition)
    {
        uiCanvas.transform.LookAt(new Vector3(cameraPosition.position.x, uiCanvas.transform.position.y, cameraPosition.position.z));
    }
}
