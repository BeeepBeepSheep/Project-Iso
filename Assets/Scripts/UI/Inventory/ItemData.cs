using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    None,
    Weapon,
    OffHand,
    Head,
    Neck,
    Shoulders,
    Chest,
    Waist,
    Hands,
    Finger,
    Legs,
    Feet
    
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public EquipmentSlot equipmentSlot;
    public int sizeWidth = 1;
    public int sizeHeight = 1;

    public Sprite icon;
}
