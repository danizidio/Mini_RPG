using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Attribute", menuName = "Add Enemy Attributes", order = 2)]
public class EnemyAttributes : ScriptableObject
{
    [Header("ENEMY STATS")]

    [Space(5)]

    [Tooltip("PLAYER MAX HEALTH")]
    [SerializeField] int _healthPoints;
    public int HealthPoints { get { return _healthPoints; } }

    [Tooltip("THE QUANTITY OF XP THIS ENEMY WILL GIVE")]
    [SerializeField] int _xpPoints;
    public int XpPoints { get { return _xpPoints; } }

    [Tooltip("HOW MUCH MONEY THIS ENEMY WILL GIVE")]
    [SerializeField] int _maxMoney;
    public int MaxMoney { get { return _maxMoney; } }

    [Tooltip("ATTACK STRENGTH")]
    [SerializeField] int _atkStrength;
    public int AtkStrength { get { return _atkStrength; } }

    [Tooltip("ARMOR TO RESIST ATTACKS")]
    [SerializeField] int _defenseArmor;
    public int DefenseArmor { get { return _defenseArmor; } }

    [Tooltip("MOVE SPEED")]
    [SerializeField] float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
}
