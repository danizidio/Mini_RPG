using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Name", menuName = "Add NPC Attributes", order = 3)]
public class NpcAttributes : ScriptableObject
{
    [Header("CHARACTER BASIC INFO")]

    [Tooltip("NPC NAME")]
    [SerializeField] string _npcName;
    public string NpcName { get { return _npcName; } }

    [Space(5)]

    [Tooltip("NPC FACE SPRITE")]
    [SerializeField] Sprite _npcPortrait;
    public Sprite NpcPortrait { get { return _npcPortrait; } }

    [Space(10)]

    [Header("DIALOGS")]

    [Tooltip("NPC DIALOGS SEQUENCE")]
    [SerializeField] string[] _npcDialogs;
    public string[] NpcDialogs { get { return _npcDialogs; } }

    [Tooltip("EXHAUST DIALOG OPTION")]
    [SerializeField] string[] _exhaustedDialog;
    public string[] ExhaustedDialog { get { return _exhaustedDialog; } }
}
