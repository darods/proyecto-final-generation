using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UiExample : MonoBehaviour
{
    public static UiExample instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    [Header("Wake Up Pilot")]
    [SerializeField] Canvas canvas;
    private GameObject buttonMash1;
    private GameObject buttonMash2;
    private GameObject buttonStar1;
    private GameObject buttonStar2;
    private void Start()
    {
        buttonMash1 = canvas.transform.Find("ButtonMash1").gameObject;
        buttonMash2 = canvas.transform.Find("ButtonMash2").gameObject;

        buttonStar1 = canvas.transform.Find("ButtonStar1").gameObject;
        buttonStar2 = canvas.transform.Find("ButtonStar2").gameObject;      
    }

    public void ButtonSmash()
    {
        if (buttonMash1.activeInHierarchy)
        {
            buttonMash1.SetActive(false);
            buttonMash2.SetActive(true);
            return;
        }
        buttonMash1.SetActive(true);
        buttonMash2.SetActive(false);

    }

    public void ButtonStar()
    {
        if (buttonStar1.activeInHierarchy)
        {
            buttonStar1.SetActive(false);
            buttonStar2.SetActive(true);
            return;
        }
        buttonStar1.SetActive(true);
        buttonStar2.SetActive(false);

    }

    public void ResetUISmash (){
        buttonMash1.SetActive(false);
        buttonMash2.SetActive(false);
        buttonStar1.SetActive(false);
        buttonStar2.SetActive(false);
    }
}
