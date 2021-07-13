using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAttack : MonoBehaviour
{
    private const float Speed = 5;
    private const float LifeTime = .9f;
    private Vector2 _lastClickPos;
    private PlayerData_Object _playerDataObject;

    void Start()
    {
        _lastClickPos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Destroy(gameObject, LifeTime);
        _playerDataObject = GameObject.Find("Earth Player").GetComponent<PlayerMovement>().playerData;
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
        transform.position = Vector2.MoveTowards(transform.position, _lastClickPos, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject.Find("Game Manager").GetComponent<ScoreManager>().score += 15;
            
            Destroy(other.gameObject); 
            Instantiate(_playerDataObject.VoidKillParticlePrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play("Attack");
    }

    private void OnDestroy()
    {
        Instantiate(_playerDataObject.attackDisableParticlePrefab, transform.position, Quaternion.identity);
    }
}
