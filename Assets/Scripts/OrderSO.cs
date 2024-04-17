using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Order Item")]
public class OrderSO : ScriptableObject
{
    [Header("Visual")]
    public Sprite orderSprite;

    public string itemName;
}
