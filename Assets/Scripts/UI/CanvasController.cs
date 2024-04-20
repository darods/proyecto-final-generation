using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // Names of the canvases
    public string canvas1Name = "Canvas1";
    public string canvas2Name = "Canvas2";
    GameObject canvas1;
    GameObject canvas2;

    void Start()
    {
        // Find canvases by their names
        canvas1 = GameObject.Find(canvas1Name);
        canvas2 = GameObject.Find(canvas2Name);

        // Ensure both canvases start deactivated
        if (canvas1 != null)
            canvas1.SetActive(false);
        else
            Debug.LogError("Canvas1 not found!");

        if (canvas2 != null)
            canvas2.SetActive(false);
        else
            Debug.LogError("Canvas2 not found!");
        
        canvas1.SetActive(true);
    }
    

    // This function can be called to switch from canvas1 to canvas2
    public void SwitchCanvas()
    {
        // Find canvases by their names

        if (canvas1 != null)
        {
            if(canvas2 != null)
            {
                // Deactivate canvas1
                canvas1.SetActive(false);

                // Activate canvas2
                canvas2.SetActive(true);
            }
            else
            {
                Debug.LogError("Canvas2 not found!");
            }
           
        }
        else
        {
            Debug.LogError("Canvas1 not found!");
        }
    }
}
