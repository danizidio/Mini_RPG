using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] EnemyAttributes _enemyAttributes;

    [SerializeField] int _currentLife;
    public int CurrentLife { get { return _currentLife; } set { _currentLife = value; } }

    float _moveSpeed;
    
    Rigidbody2D rb;

    Animator _anim;

    Transform _player;

    [SerializeField] float _distanceToAttack;
    [SerializeField] float _bufferDistance;

    float _spd;

    [SerializeField] float _chanceToGiveItem;

    [SerializeField] GameObject _item;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _spd = _enemyAttributes.MoveSpeed;

        CurrentLife = _enemyAttributes.HealthPoints;

        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, _player.position) > _distanceToAttack && _player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _spd * Time.deltaTime);
        }
    }


    public void ReceiveDamage(int atk)
    {
        int damage = atk - _enemyAttributes.DefenseArmor;
        CurrentLife -= damage;

        if(CurrentLife <= 0)
        {
            GiveItem();

            _anim.Play("Death");
        }

        if (_player.localScale.x == 1)
        {
            rb.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
        }

        if (_player.localScale.x == -1)
        {
            rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
        }
    }
    public void GiveItem()
    {
        float value = Random.value;

        if (value <= _chanceToGiveItem)
        {
            Instantiate(_item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }

        float value2 = Random.value;

        if(value2 <= _chanceToGiveItem)
        {
            GameBehaviour.instance.PlayerMoney += Random.Range(1, _enemyAttributes.MaxMoney);

            GameBehaviour.instance.OnTakingCoins?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehaviour p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            p.ReceiveDamage(_enemyAttributes.AtkStrength);
        }
    }

}
