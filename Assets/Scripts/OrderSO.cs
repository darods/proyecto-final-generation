using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Order Item")]
public class OrderSO : ScriptableObject
{

    
    [Header("Visual")]
    public Texture orderSprite;
    public Texture OrderSprite {get{ return orderSprite ; } }

    
}