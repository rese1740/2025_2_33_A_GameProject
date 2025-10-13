using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [Header("기본 세팅")]
    public string itemName;
    public Sprite itemIcon;
    public int maxStack = 99;
}
