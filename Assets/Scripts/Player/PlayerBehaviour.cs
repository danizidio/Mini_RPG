using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action OnActing;
    public event Action OnPausing;

    [SerializeField] PlayerAttributes _playerAttribute;

    public PlayerAttributes PlayerAttribute { get { return _playerAttribute; } }

    [SerializeField] EquipmentAttributes[] _equipAttribute;

    [SerializeField] GameObject[] _femaleWeapons, _maleWeapons;
    public EquipmentAttributes[] EquipAttribute { get { return _equipAttribute; } }

    [SerializeField] SpriteRenderer[] _femaleLightArmorPieces, _maleLightArmorPieces, _femaleHeavyArmorPieces, _maleHeavyArmorPieces;

    int _currentLife;
    public int CurrentLife { get { return _currentLife; } set { _currentLife = value; } }

    Rigidbody2D rb;

    int _atkModifier, _defModifier;
    float _moveSpeed;
    float _moveX;
    float _moveY;

    bool _isRunning;

    [SerializeField] bool _canMove;
    public bool canMove { get { return _canMove; } }

    SaveLoad s;

    Animator _anim;

    [SerializeField] GameObject _txtAction;

    LifeBar _lifeBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CharCreationBehaviour.instance.LoadCustomCharacter();

        EquipsToUse();

        _canMove = true;

        _currentLife = PlayerAttribute.HealthPoints;

        _anim = GetComponentInChildren<Animator>();

        try
        {
            _lifeBar = GameObject.FindGameObjectWithTag("LifeBar").GetComponent<LifeBar>();
        }
        catch
        {

        }
    }

    void LateUpdate()
    {
        if (OnActing == null)
        {
            OnActing = Attacking;
        }

        if (!_isRunning)
        {
            _isRunning = false;
            _moveSpeed = PlayerAttribute.MoveSpeed;
        }

        if (_moveX == 0 && _moveY == 0)
        {
            _anim.SetBool("WALK", false);
        }
        else
        {
            _anim.SetBool("WALK", true);
        }

        if (_canMove == false)
        {
            _moveX = 0;
            _moveY = 0;
        }

        rb.velocity = new Vector2(_moveX * _moveSpeed, _moveY * _moveSpeed);
    }

    #region - InputManager Buttons
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_moveX == 1)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        if (_moveX == -1)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        if (_canMove)
        {
            _moveX = context.ReadValue<Vector2>().x;
            _moveY = context.ReadValue<Vector2>().y;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();

        if (_isRunning)
        {
            _moveSpeed = PlayerAttribute.MoveSpeed * PlayerAttribute.MoveSpeed;
        }
        else
        {
            _moveSpeed = PlayerAttribute.MoveSpeed;
        }
    }

    public void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnActing?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnPausing?.Invoke();
        }
    }
    #endregion

    public void Attacking()
    {
        _anim.SetTrigger("ATTACK");
    }

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public void EquipsToUse()
    {
        if (PlayerPrefs.HasKey(SaveStrings.ARMOR.ToString()))
        {
            int n = PlayerPrefs.GetInt(SaveStrings.ARMOR.ToString());

            switch (n)
            {
                case 0:
                    {
                        foreach (var item in _femaleLightArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }
                        foreach (var item in _femaleHeavyArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }

                        foreach (var item in _maleLightArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }
                        foreach (var item in _maleHeavyArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }

                        break;
                    }
                case 1:
                    {
                        foreach (var item in _femaleLightArmorPieces)
                        {
                            item.gameObject.SetActive(true);
                        }
                        foreach (var item in _femaleHeavyArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }

                        foreach (var item in _maleLightArmorPieces)
                        {
                            item.gameObject.SetActive(true);
                        }
                        foreach (var item in _maleHeavyArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }

                        _defModifier = PlayerAttribute.DefenseArmor + EquipAttribute[0].EquipStrength;

                        break;
                    }
                case 2:
                    {
                        foreach (var item in _femaleLightArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }
                        foreach (var item in _femaleHeavyArmorPieces)
                        {
                            item.gameObject.SetActive(true);
                        }

                        foreach (var item in _maleLightArmorPieces)
                        {
                            item.gameObject.SetActive(false);
                        }
                        foreach (var item in _maleHeavyArmorPieces)
                        {
                            item.gameObject.SetActive(true);
                        }

                        _defModifier = PlayerAttribute.DefenseArmor + EquipAttribute[1].EquipStrength;

                        break;
                    }
            }
        }
        else
        {
            s = new SaveLoad();

            s.PlayerSaveInt(SaveStrings.ARMOR.ToString(), 0);

            foreach (var item in _femaleLightArmorPieces)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in _femaleHeavyArmorPieces)
            {
                item.gameObject.SetActive(false);
            }

            foreach (var item in _maleLightArmorPieces)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in _maleHeavyArmorPieces)
            {
                item.gameObject.SetActive(false);
            }

            _defModifier = PlayerAttribute.DefenseArmor;
        }

        if (PlayerPrefs.HasKey(SaveStrings.WEAPON.ToString()))
        {
            int n = PlayerPrefs.GetInt(SaveStrings.WEAPON.ToString());

            switch (n)
            {
                case 0:
                    {
                        foreach (var item in _femaleWeapons)
                        {
                            item.gameObject.SetActive(false);
                        }
                        foreach (var item in _maleWeapons)
                        {
                            item.gameObject.SetActive(false);
                        }

                        _atkModifier = PlayerAttribute.AtkStrength;

                        break;
                    }
                case 1:
                    {
                        for (int i = 0; i < _femaleWeapons.Length; i++)
                        {
                            if (i == 0)
                            {
                                _femaleWeapons[i].gameObject.SetActive(true);

                                _atkModifier = PlayerAttribute.AtkStrength + _femaleWeapons[i].GetComponent<EquipmentPerks>().EquipAttributes.EquipStrength;
                            }
                            else
                            {
                                _femaleWeapons[i].gameObject.SetActive(false);
                            }
                        }
                        for (int i = 0; i < _maleWeapons.Length; i++)
                        {
                            if (i == 0)
                            {
                                _maleWeapons[i].gameObject.SetActive(true);

                                _atkModifier = PlayerAttribute.AtkStrength + _maleWeapons[i].GetComponent<EquipmentPerks>().EquipAttributes.EquipStrength;
                            }
                            else
                            {
                                _maleWeapons[i].gameObject.SetActive(false);
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < _femaleWeapons.Length; i++)
                        {
                            if (i == 1)
                            {
                                _femaleWeapons[i].gameObject.SetActive(true);

                                _atkModifier = PlayerAttribute.AtkStrength + _femaleWeapons[i].GetComponent<EquipmentPerks>().EquipAttributes.EquipStrength;
                            }
                            else
                            {
                                _femaleWeapons[i].gameObject.SetActive(false);
                            }
                        }
                        for (int i = 0; i < _maleWeapons.Length; i++)
                        {
                            if (i == 1)
                            {
                                _maleWeapons[i].gameObject.SetActive(true);

                                _atkModifier = PlayerAttribute.AtkStrength + _maleWeapons[i].GetComponent<EquipmentPerks>().EquipAttributes.EquipStrength;
                            }
                            else
                            {
                                _maleWeapons[i].gameObject.SetActive(false);
                            }
                        }

                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < _femaleWeapons.Length; i++)
                        {
                            if (i == 2)
                            {
                                _femaleWeapons[i].gameObject.SetActive(true);

                                _atkModifier = PlayerAttribute.AtkStrength + _femaleWeapons[i].GetComponent<EquipmentPerks>().EquipAttributes.EquipStrength;
                            }
                            else
                            {
                                _femaleWeapons[i].gameObject.SetActive(false);
                            }
                        }
                        for (int i = 0; i < _maleWeapons.Length; i++)
                        {
                            if (i == 2)
                            {
                                _maleWeapons[i].gameObject.SetActive(true);

                                _atkModifier = PlayerAttribute.AtkStrength + _maleWeapons[i].GetComponent<EquipmentPerks>().EquipAttributes.EquipStrength;
                            }
                            else
                            {
                                _maleWeapons[i].gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
            }
        }
        else
        {
            s = new SaveLoad();

            s.PlayerSaveInt(SaveStrings.WEAPON.ToString(), 0);

            foreach (var item in _femaleWeapons)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in _maleWeapons)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        OnActing = null;
    }

    public void ShowInfoUI(bool show, string txt)
    {
        _txtAction.SetActive(show);
        _txtAction.GetComponentInChildren<TMPro.TMP_Text>().text = txt;
    }

    public void ReceiveDamage(int atk)
    {
        int damage = atk - _defModifier;

        CurrentLife -= damage;

        _lifeBar.onUpdateLifeBar(CurrentLife, PlayerAttribute.HealthPoints);

        if(CurrentLife <= 0)
        {
            GameBehaviour.OnNextGameState(GamePlayStates.GAMEOVER);
        }

        if(transform.localScale.x == 1)
        {
            rb.AddForce(new Vector2(-100 ,0), ForceMode2D.Impulse);
        }

        if (transform.localScale.x == -1)
        {
            rb.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
        }

        _anim.SetTrigger("DAMAGED");

        //ShowInfoUI(true, damage.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehaviour e = collision.gameObject.GetComponent<EnemyBehaviour>();

        if(e != null)
        {
            e.ReceiveDamage(_atkModifier);
        }
    }
}
