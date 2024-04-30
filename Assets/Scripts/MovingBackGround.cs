using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBackGround : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private float xMovement;
    [SerializeField] private float yMovement;

    private void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(xMovement, yMovement) * Time.deltaTime, backgroundImage.uvRect.size);
    }
}
