using UnityEngine;
using UnityEngine.EventSystems;

public class EarthController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Earth Variables")]
    [SerializeField] private float rotationSpeed = 5f;
    private bool isRotating = false;
    private Vector3 startMousePosition;

    [Header("Levels")]
    [SerializeField] private GameObject secondLevel;
    [SerializeField] private GameObject thirdLevel;

    private void Start()
    {
        secondLevel.SetActive(false);
        thirdLevel.SetActive(false);

        if (PlayerPrefs.GetInt("Level1Completed") == 1)
        {
            secondLevel.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level2Completed") == 2)
        {
            thirdLevel.SetActive(true);
        }
    }

    private void Update()
    {
        if (isRotating)
        {
            RotateEarth();
        }
    }

    private void RotateEarth()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseMovement = currentMousePosition - startMousePosition;

        mouseMovement.x /= Screen.width;
        mouseMovement.y /= Screen.height;

        float angleX = mouseMovement.y * rotationSpeed * Time.deltaTime * 360;
        float angleY = -mouseMovement.x * rotationSpeed * Time.deltaTime * 360;

        transform.Rotate(Vector3.up, angleY, Space.World);
        transform.Rotate(Vector3.right, angleX, Space.World);

        startMousePosition = currentMousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isRotating = true;
        startMousePosition = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;
    }

    public void MarkLevelCompleted(int levelNumber)
    {
        PlayerPrefs.SetInt($"Level{levelNumber}Completed", 1);
        PlayerPrefs.Save();
    }
}
