using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Flag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalSize;

    public string[] objectNames; // Lista de nombres de objetos\

    [SerializeField] private UnityEvent OnClick;

    private void Start()
    {
        originalSize = transform.localScale;
    }

    private void Test(string msg)
    {
        Debug.Log(msg);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalSize * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalSize;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Obtener el nombre del objeto que fue presionado
        string clickedObjectName = gameObject.name;

        // Verificar si el nombre del objeto está en la lista de nombres
        bool found = false;
        foreach (string name in objectNames)
        {
            if (clickedObjectName == name)
            {
                found = true;
                break;
            }
        }

        // Si el nombre del objeto está en la lista, cargar la escena correspondiente
        if (found)
        {
            switch (clickedObjectName)
            {
                case "Flag":
                    Loader.Load(Loader.Scene.FirstLevel);
                    break;
                case "Flag2":
                    Loader.Load(Loader.Scene.SecondLevel);
                    break;
                default:
                    Debug.LogWarning("Objeto desconocido: " + clickedObjectName);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Nombre del objeto no encontrado en la lista: " + clickedObjectName);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Test("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Test("OnPointerUp");
    }
}
