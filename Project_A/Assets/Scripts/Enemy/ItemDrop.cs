using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class ItemDrop : ScriptableObject
{
    public string itemName; // 아이템 이름
    public ItemType itemType; // 아이템의 유형
    public Sprite itemImage; // 아이템 이미지 (보류)
    public GameObject itemPrefab; // 아이템의 프리팹
    //----------------------------
    public int rewardValue; // 모양은 똑같은데 지급 수량을 다르게 하고 싶을 때

    public enum ItemType
    {
        Ticket
    }
}