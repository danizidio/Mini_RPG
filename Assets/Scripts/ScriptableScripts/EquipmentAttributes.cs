using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    WEAPON,
    ARMOR
}

[CreateAssetMenu(fileName = "Equipment Attribute", menuName = "Add Equipment Attributes", order = 1)]
public class EquipmentAttributes : ScriptableObject
{
    [Header("EQUIPMENT TYPE")]
    [Tooltip("EQUIP TYPE")]
    [SerializeField] EquipmentType _equipType;
    public EquipmentType EquipType { get { return _equipType; } }

    [Header("EQUIPMENT SPRITE")]
    [Tooltip("EQUIP SPRITE")]
    [SerializeField] Sprite _equipPortrait;
    public Sprite EquipPortrait { get { return _equipPortrait; } }

    [Header("MAIN ATTRIBUTE")]
    [Tooltip("EQUIPMENT STRENGTH - ATTACK MODIFIER FOR WEAPONS, DEFENSE MODIFIER FOR ARMOR")]
    [SerializeField] int _equipStrength;
    public int EquipStrength { get { return _equipStrength; } }
}
