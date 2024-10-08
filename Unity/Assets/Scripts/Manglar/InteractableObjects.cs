using UnityEngine;
using UnityEngine.XR;

public class InteractableObject : MonoBehaviour
{
    public bool isInteractable = true;  // Indica si el objeto puede ser interactuado
    //public float interactionRange = 2f; // Rango dentro del cual el objeto puede ser interactuado

    protected XRNode controllerNode = XRNode.RightHand; // O bien XRNode.LeftHand para el controlador izquierdo

    void Update()
    {
        if (isInteractable)
        {
            if (IsInteractButtonPressed())
            {
                OnInteract();
            }
        }
    }

    // Método para verificar si el usuario está en rango para interactuar con el objeto
    //protected bool CheckInteractionRange()
    //{
    //    Transform playerTransform = Camera.main.transform; // Tomar la posición de la cámara como referencia

    //    float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        //return distanceToPlayer <= interactionRange;
   // }

    // Detectar si el botón de interacción está siendo presionado
    protected bool IsInteractButtonPressed()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool triggerValue;
        if (device.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
        {
            return true;
        }
        return false;
    }

    // Este método puede ser sobrescrito por los objetos específicos
    protected virtual void OnInteract()
    {
        Debug.Log("Interacted with: " + gameObject.name);
    }
}

