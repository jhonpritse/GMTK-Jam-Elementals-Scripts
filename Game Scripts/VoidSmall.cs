using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Pathfinding;
using UnityEngine;

public class VoidSmall : MonoBehaviour
{
    public int damage;
    [SerializeField] private float speed;
    [SerializeField] private Transform voidBody;
    private Seeker _seeker;
    private AstarPath _astar;
    private Transform _target;
    private float _nextWayPointDistance = 3f;
    private Path _path;
    private int _currentWaypoint;
    private Rigidbody2D _rigidbody;
    private Vector2 _force;
    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("LifeCrystal").GetComponent<Transform>();
        _astar = GameObject.Find("Path").GetComponent<AstarPath>();
        _seeker = GetComponent<Seeker>();

       InvokeRepeating(nameof(StartPathFind), 0f, .5f);
    }
    
    public void StartPathFind()
    {
        _astar.Scan();
        if (_seeker.IsDone()) 
            _seeker.StartPath(_rigidbody.position, _target.position, OnPathComplete);
    }
    void OnPathComplete(Path _p)
    {
        if (!_p.error)
        {
            _path = _p;
            _currentWaypoint = 0;
        }
    }
    
    void FixedUpdate()
    {
        if (_path == null) return;
  
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            return;
        }
        
        StartMovement();
        
    }
    
    void StartMovement()
    {
        var position = _rigidbody.position;
        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - position).normalized;
        _force = direction * (speed * Time.deltaTime); 
        _rigidbody.AddForce(_force);
     
        
        float distance = Vector2.Distance(position, _path.vectorPath[_currentWaypoint]);
     
        if (distance < _nextWayPointDistance)
        {
            _currentWaypoint++;
        }
    }
    
    private void Update()
    {
        LookAtDir(_target);
    }

    void LookAtDir(Transform target)
    {
        var lookDir = target.position - voidBody.position;
        lookDir.y = 0f;
        
        if (lookDir.x <= -0.1f)
        {
            voidBody.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    private void OnDestroy()
    {
        FindObjectOfType<AudioManager>().Play("VoidDeath");
    }
}
