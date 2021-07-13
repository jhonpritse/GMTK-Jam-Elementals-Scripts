using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaAttack : MonoBehaviour
{
    private float _maxSize = 3f;
    private const float LifeTime = 1.5f;
    private bool _canGrow;
    private PlayerData_Object _playerDataObject;

    private void Start()
    {
        var angle = Mathf.Atan2(0f, 1f) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _playerDataObject = GameObject.Find("Fire Player").GetComponent<PlayerMovement>().playerData;
    }
    
    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play("Attack");
    }
    void Update()
    {
    
        if ( transform.localScale.x < _maxSize && !_canGrow)
        {
            var step = transform.localScale.x + 0.5f * Time.deltaTime; 
            transform.localScale = new Vector3(step, step);
        }
        else
        {
            _canGrow = true;
           Invoke(nameof(ShrinkLava), LifeTime);
        }

    }

    void ShrinkLava()
    {
        var step = transform.localScale.x - 0.65f * Time.deltaTime; 
        transform.localScale = new Vector3(step, step);
        if (transform.localScale.x <= .2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Earth/Magma"))
        {
            if (other.CompareTag("Water/Attack") ||other.CompareTag("Water/Defence")  )
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

            if (other.CompareTag("Enemy"))
            {
                GameObject.Find("Game Manager").GetComponent<ScoreManager>().score += 75;
               Destroy(other.gameObject); 
               Instantiate(_playerDataObject.VoidKillParticlePrefab, transform.position, Quaternion.identity);
            }
        }

   
        if (gameObject.CompareTag("Earth/Mud"))
        {
            if (other.CompareTag("Air/Attack") )
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            
            if (other.CompareTag("Enemy"))
            {
                GameObject.Find("Game Manager").GetComponent<ScoreManager>().score += 75;
                Destroy(other.gameObject); 
                Instantiate(_playerDataObject.VoidKillParticlePrefab, transform.position, Quaternion.identity);
            }
        }
       
    }
}
