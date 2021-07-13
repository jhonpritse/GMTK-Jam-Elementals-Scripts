using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAttack : MonoBehaviour
{
    private const float Speed = 8;
    private const float LifeTime = 3;
    private  float _defenceDistance = .8f;
    private Vector2 _lastClickPos;
    private bool _isExpland;
    private PlayerData_Object _playerDataObject;
    
    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play("Attack");
    }
    void Start()
    {
        _lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _playerDataObject = GameObject.Find("Water Player").GetComponent<PlayerMovement>().playerData;
        LookAtMouse();
    }
    void LookAtMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Update()
    {
        if ((Vector2) transform.position != _lastClickPos)
        {
            if (_defenceDistance > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _lastClickPos, Speed * Time.deltaTime);
                _defenceDistance -= Time.deltaTime;
            }
            else
            {
                if (!_isExpland)
                {
                    Expand();
                    _isExpland = true;
                }
            }
        }else
        {
            if (!_isExpland)
            {
                Expand();
                _isExpland = true;
            }
        }
    }

    void Expand()
    {
        Destroy(gameObject, LifeTime);
        var step = transform.localScale.x * 2.5f;
        transform.localScale = new Vector3(step, step);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Earth/Defence"))
        {
            Instantiate(_playerDataObject.MixAttackPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Enemy"))
        {
            GameObject.Find("Game Manager").GetComponent<ScoreManager>().score += 7;
            Destroy(other.gameObject); 
            Instantiate(_playerDataObject.VoidKillParticlePrefab, transform.position, Quaternion.identity);
        }
    }
    
    private void OnDestroy()
    {
        Instantiate(_playerDataObject.attackDisableParticlePrefab, transform.position, Quaternion.identity);
    }
}
