using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData_Object playerData;
    private float _speed;
    private Vector2 _moveVelocity;
    private Rigidbody2D _rb;
    private GameObject _attack;
    private GameObject _defence;
    private float _attackCD;
    private float _defenceCD;
    private bool _canAttack;
    private bool _canDefence;

    
    void Start()
    {
       SetPlayerVar();
        _rb = GetComponent<Rigidbody2D>();
    }
    void SetPlayerVar()
    {
        _speed = playerData.speed;
        _attack = playerData.attackPrefab;
        _defence = playerData.defencePrefab;
        _attackCD = playerData.attackCooldown;
        _defenceCD = playerData.defenceCooldown;

    }

    private void OnEnable()
    {
        _canAttack = true;
        _canDefence = true;
        GetComponent<Collider2D>().enabled = true;
    }
    private void OnDisable()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    
    private void ButtonMovement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _moveVelocity = moveInput * (_speed * Time.deltaTime) ;
    }

    private void Attack()
    {
        if (!_canAttack )
        {
            _attackCD -= Time.deltaTime;
            if (_attackCD <= 0)
            {
                _canAttack = true;
                _attackCD = playerData.attackCooldown;
            }
        }
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            Instantiate(_attack, transform.position, Quaternion.identity);
            _canAttack = false;
        }
    }
    private void Defence()
    {
        if (!_canDefence )
        {
            _defenceCD -= Time.deltaTime;
            if (_defenceCD <= 0)
            {
                _canDefence = true;
                _defenceCD = playerData.defenceCooldown;
            }
        }
        if (Input.GetMouseButtonDown(1) && _canDefence)
        {
            Instantiate(_defence, transform.position, Quaternion.identity);
            _canDefence = false;
        }
    }
    
    void Update()
    { 
        ButtonMovement(); 
        Attack(); 
        Defence();
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveVelocity);
    }
}
