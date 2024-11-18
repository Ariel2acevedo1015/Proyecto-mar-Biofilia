using System.Collections;
using UnityEngine;

public class GuiaPath : MonoBehaviour
{
    [SerializeField] private string pathname; // Nombre del path actual
    [SerializeField] private float time = 8f; // Duraci�n del movimiento en el path
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutSine; // Tipo de transici�n
    private bool isMoving = false; // Controla si el objeto est� en movimiento

    private void Start()
    {
        //StartPath(pathname, time);
    }

    // M�todo para iniciar el movimiento en un path
    public void StartPath(string newPathname)
    {
        if (isMoving)
        {
            StopPath(); // Detener cualquier movimiento previo antes de iniciar uno nuevo
        }

        pathname = newPathname;
       // time = newTime;

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "path", iTweenPath.GetPath(pathname),
            "easetype", easeType,
            "time", time,
            "oncomplete", "OnPathComplete"
        ));

        isMoving = true;
    }

    // M�todo para detener el movimiento actual
    public void StopPath()
    {
        iTween.Stop(this.gameObject); // Detiene cualquier animaci�n activa
        isMoving = false;
    }

    // Callback que se ejecuta cuando el movimiento en el path se completa
    private void OnPathComplete()
    {
        isMoving = false;
        Debug.Log($"El movimiento en el path '{pathname}' se complet�.");
    }

    // M�todo p�blico para cambiar el path din�micamente desde otro script o UI
    public void ChangePath(string newPathname)
    {
        StartPath(newPathname);
    }
}
