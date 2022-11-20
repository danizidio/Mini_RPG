using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPerks : MonoBehaviour
{
    [SerializeField] EquipmentAttributes _equipAttributes;
    public EquipmentAttributes EquipAttributes { get { return _equipAttributes; } }

    [SerializeField] EquipmentType _type;
    public EquipmentType Type { get { return _type; } }
}
