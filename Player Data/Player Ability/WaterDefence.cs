using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDefence : MonoBehaviour
{
    private const float Speed = 6;
    private const float LifeTime = 1f;
    private Vector2 _lastClickPos;
    private PlayerData_Object _playerDataObject;

    void Start()
    {
        _lastClickPos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Destroy(gameObject, LifeTime);
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
        transform.position = Vector2.MoveTowards(transform.position, _lastClickPos, Speed * Time.deltaTime);
        var localScale = transform.localScale;
        var step =  localScale.x - (.7f * Time.deltaTime);
        localScale = new Vector3(step, localScale.y);
        transform.localScale = localScale;

        if (step <= 0.2f)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play("Attack");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject.Find("Game Manager").GetComponent<ScoreManager>().score += 10;
            Destroy(other.gameObject); 
            Instantiate(_playerDataObject.VoidKillParticlePrefab, transform.position, Quaternion.identity);
        }
    }
    private void OnDestroy()
    {
        Instantiate(_playerDataObject.attackDisableParticlePrefab, transform.position, Quaternion.identity);
    }
}
