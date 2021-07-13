using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMovement : MonoBehaviour
{

    private GameObject _activePlayer;
    private const int Speed = 25;
    void Start()
    {
        MoveToSelectedPlayer();
    }
    

    private void MoveToSelectedPlayer()
    {
        _activePlayer = GameObject.Find("Game Manager").GetComponent<InputManager>().ActivePlayerPos;
    }

    private void Update()
    {
        if ((Vector2)transform.position != (Vector2)_activePlayer.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _activePlayer.transform.position,  Speed*Time.deltaTime);
        }
        else
        {
            _activePlayer.GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("Game Manager").GetComponent<InputManager>().CanSwitch = true;
            Destroy(gameObject);
        }
       
    }
}
