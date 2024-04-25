using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Order Item")]
public class OrderSO : ScriptableObject
{
    [Header("Visual")]
    public Texture orderSprite;
    public GameObject prefab;
    
    public GameObject Prefab {get{ return prefab ; } }
    public Texture OrderSprite {get{ return orderSprite ; } }
}