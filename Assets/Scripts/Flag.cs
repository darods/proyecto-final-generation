using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Flag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalSize;

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
        OnClick.Invoke();
        Test("OnPointerClick");
        Loader.Load(Loader.Scene.FirstLevel);

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
