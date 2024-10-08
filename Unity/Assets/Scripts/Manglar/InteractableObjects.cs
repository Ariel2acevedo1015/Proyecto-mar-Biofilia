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

    // M�todo para verificar si el usuario est� en rango para interactuar con el objeto
    //protected bool CheckInteractionRange()
    //{
    //    Transform playerTransform = Camera.main.transform; // Tomar la posici�n de la c�mara como referencia

    //    float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        //return distanceToPlayer <= interactionRange;
   // }

    // Detectar si el bot�n de interacci�n est� siendo presionado
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

    // Este m�todo puede ser sobrescrito por los objetos espec�ficos
    protected virtual void OnInteract()
    {
        Debug.Log("Interacted with: " + gameObject.name);
    }
}

