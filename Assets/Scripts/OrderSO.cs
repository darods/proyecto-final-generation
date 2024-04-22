using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Order Item")]
public class OrderSO : ScriptableObject
{
    [Header("Visual")]
    public string orderName;

    public Texture orderSprite;
}