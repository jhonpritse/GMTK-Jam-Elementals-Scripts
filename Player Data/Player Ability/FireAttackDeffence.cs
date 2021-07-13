using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackDeffence : MonoBehaviour
{
    private const float Speed = 10;
    private const float LifeTime = 1;
    private Vector2 _lastClickPos;
    private PlayerData_Object _playerDataObject;
    
    void Start()
    {
        _playerDataObject = GameObject.Find("Fire Player").GetComponent<PlayerMovement>().playerData;
        Destroy(gameObject, LifeTime );
        _lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            transform.position = Vector2.MoveTowards(transform.position, _lastClickPos, Speed * Time.deltaTime);
        }else
        {
            Destroy(gameObject);
        }
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
            GameObject.Find("Game Manager").GetComponent<ScoreManager>().score += 5;
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
