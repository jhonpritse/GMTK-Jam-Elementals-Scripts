using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject spawner;
    [SerializeField] private float spawnRate;
    private float _spawnTimer;
    private int _counterLimit;
    void Start()
    {
        _spawnTimer = spawnRate;
        var rand = Random.Range(0, spawnPoints.Length);
        Instantiate(spawner, spawnPoints[rand].position, Quaternion.identity);
        _counterLimit = spawnPoints.Length;
        FindObjectOfType<AudioManager>().Play("heart");
    }

    
    void Update()
    {
        spawnRate -= Time.deltaTime;
        
        if (spawnRate <= 0 && _counterLimit > 0)
        {
            var rand = Random.Range(0, spawnPoints.Length);
            Instantiate(spawner, spawnPoints[rand].position, Quaternion.identity);
            _counterLimit--;
            spawnRate = _spawnTimer;
        }
    }
}
