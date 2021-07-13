using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject damageCrystalParticle;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float patrolStopTime;
    private float _patrolTimer;
    private Transform _startPoint;
    private bool _isMoving;
    private Transform _waypoint;
    private Animator _animator;
    private TextMeshProUGUI _healthText;
    
    void Start()
    {
        _animator = GameObject.Find("Main Camera").GetComponent<Animator>();
        _healthText = GameObject.Find("CrystalHealthText").GetComponent<TextMeshProUGUI>();
        
        var rand = Random.Range(0, patrolPoints.Length);
        _startPoint = patrolPoints[rand];
        _patrolTimer = patrolStopTime;
        
    }

    void Update()
    {
        if(health <= 0) GameOver();
        
        ScoreToText();
        
        if (!_isMoving)
        {
            patrolStopTime -= Time.deltaTime;
        }
        if (patrolStopTime <= 0)
        {
            _isMoving = true;
            var rand = Random.Range(0, patrolPoints.Length);
            _waypoint = patrolPoints[rand];
        
            patrolStopTime = _patrolTimer;
        }
        
        StartPatrol();

    }

    void ScoreToText()
    {
        _healthText.text = health.ToString();
    }
    void StartPatrol()
    {
        
        if (_isMoving && (Vector2) transform.position != (Vector2)_waypoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _waypoint.position, patrolSpeed * Time.deltaTime);
        }
        else
        { 
         _isMoving = false;
        }
    }
    void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var damage = other.GetComponent<VoidSmall>().damage;
            health -= damage;
            Destroy(other.gameObject);
            Instantiate(damageCrystalParticle, transform.position, Quaternion.identity);
            _animator.SetTrigger("Shake");
            FindObjectOfType<AudioManager>().Play("CrystalDamage");
        }
        if (
            other.CompareTag("Fire/Attack") ||
            other.CompareTag("Earth/Attack") ||
            other.CompareTag("Earth/Defence") || 
            other.CompareTag("Water/Attack") ||
            other.CompareTag("Water/Defence") ||
            other.CompareTag("Air/Attack"))
        {
            health -= 1;
            Destroy(other.gameObject);
            Instantiate(damageCrystalParticle, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("CrystalDamage");
            _animator.SetTrigger("Shake");
        }
        
    }
}
