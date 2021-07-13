using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject voidMonster;
    [SerializeField] private float spawnRate;
    private float _spawnTimer;
    void Start()
    {
        _spawnTimer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate -= Time.deltaTime;
        if (spawnRate <= 0)
        {
            Instantiate(voidMonster, transform.position, Quaternion.identity);
            spawnRate = _spawnTimer;
        }
    }
}
