using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class InputManager : MonoBehaviour
{
    private int _activePlayer;
    public GameObject spiritPrefab;
    public GameObject ActivePlayerPos { get; private set;}

    private GameObject _firePlayer;
    [SerializeField] private GameObject fireIndicator; 
    private GameObject _earthPlayer;
    [SerializeField] private GameObject earthIndicator; 
    private GameObject _waterPlayer;
    [SerializeField] private GameObject waterIndicator; 
    private GameObject _airPlayer;
    [SerializeField] private GameObject airIndicator; 

    
    
    private bool _isSelect;
    private bool _isQ;
    private bool _isE;
    
    public bool CanSwitch { get; set;}
    private bool _firstSpawn;

    
    private readonly Color _fireNorm = new Color(.75f, 0, 0, .5f);
    private readonly Color _earthNorm = new Color(.75f, .2f, 0f, .5f);
    private readonly Color _waterNorm = new Color(0, 0, .75f, .5f);
    private readonly Color _airNorm = new Color(.6f, 1f, 1f, .5f);


    private void Start()
    {
        _firePlayer = GameObject.Find("Fire Player");
        _earthPlayer = GameObject.Find("Earth Player");
        _waterPlayer = GameObject.Find("Water Player");
        _airPlayer =  GameObject.Find("Air Player");
        
        _activePlayer = Random.Range(1, 4);
        CanSwitch = true;
        _isSelect = true;
        _firstSpawn = true;

        fireIndicator.GetComponent<SpriteRenderer>().color = _fireNorm;
        earthIndicator.GetComponent<SpriteRenderer>().color = _earthNorm;
        waterIndicator.GetComponent<SpriteRenderer>().color = _waterNorm;
        airIndicator.GetComponent<SpriteRenderer>().color = _airNorm;

    }

    
    void SwitchPlayer()
    {
        if (_firstSpawn)
        {
            Instantiate(spiritPrefab, transform.position, Quaternion.identity);
            _firstSpawn = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && CanSwitch)
        {
            DisableMovement();
            _isQ = true;
            _isSelect = true;
            CanSwitch = false;
            if (_activePlayer != 1) _activePlayer--;
            else if (_activePlayer == 1) _activePlayer = 4;
        }
        else if (Input.GetKeyDown(KeyCode.E) && CanSwitch )
        {
            DisableMovement();
            _isE = true;
            _isSelect = true;
            CanSwitch = false;
            if (_activePlayer != 4) _activePlayer++;
            else if (_activePlayer == 4)  _activePlayer = 1;
        }
    }
    void DisableMovement()
    {
        _firePlayer.GetComponent<PlayerMovement>().enabled = false;
        _earthPlayer.GetComponent<PlayerMovement>().enabled = false;
        _waterPlayer.GetComponent<PlayerMovement>().enabled = false;
        _airPlayer.GetComponent<PlayerMovement>().enabled = false;

        fireIndicator.GetComponent<SpriteRenderer>().color = _fireNorm;
        earthIndicator.GetComponent<SpriteRenderer>().color = _earthNorm;
        waterIndicator.GetComponent<SpriteRenderer>().color = _waterNorm;
        airIndicator.GetComponent<SpriteRenderer>().color = _airNorm;
    }
    
    void SelectPlayer()
    {
        switch (_activePlayer)
        {
            case 1:
                FireSelected();
                break;
            case 2:
                EarthSelected();
                break;
            case 3:
                WaterSelected();
                break;
            case 4:
                AirSelected();
                break;
        }
    }
    
    
    void Update()
    {
        SwitchPlayer();
        SelectPlayer();
    }
    
    

    private void FireSelected()
    {
        if (_isSelect)
        {
            fireIndicator.GetComponent<SpriteRenderer>().color = new Color(.75f, 0, 0, 1f);
            earthIndicator.GetComponent<SpriteRenderer>().color = _earthNorm;
            waterIndicator.GetComponent<SpriteRenderer>().color = _waterNorm;
            airIndicator.GetComponent<SpriteRenderer>().color = _airNorm;
            
            ActivePlayerPos = _firePlayer;
            if (_isQ)
            {
                Instantiate(spiritPrefab, _earthPlayer.transform.position, Quaternion.identity);
                _isQ = false;
            }
            if (_isE)
            {
                Instantiate(spiritPrefab, _airPlayer.transform.position, Quaternion.identity);
                _isE = false;
            }

            _isSelect = false;
        }
    }  
    private void EarthSelected()
    {
        if (_isSelect)
        {
           
            fireIndicator.GetComponent<SpriteRenderer>().color = _fireNorm;
            earthIndicator.GetComponent<SpriteRenderer>().color = new Color(.75f, .2f, 0f, 1f);
            waterIndicator.GetComponent<SpriteRenderer>().color = _waterNorm;
            airIndicator.GetComponent<SpriteRenderer>().color = _airNorm;
            ActivePlayerPos = _earthPlayer;
            if (_isQ)
            {
                Instantiate(spiritPrefab, _waterPlayer.transform.position, Quaternion.identity);
                _isQ = false;
            }
            if (_isE)
            {
                Instantiate(spiritPrefab, _firePlayer.transform.position, Quaternion.identity);
                _isE = false;
            }
            _isSelect = false;
        }
    }
    private void WaterSelected()
    {
        if (_isSelect)
        {
            
            fireIndicator.GetComponent<SpriteRenderer>().color = _fireNorm;
            earthIndicator.GetComponent<SpriteRenderer>().color = _earthNorm;
            waterIndicator.GetComponent<SpriteRenderer>().color = new Color(.1f, .1f, 1f, 1f);
            airIndicator.GetComponent<SpriteRenderer>().color = _airNorm;
            
            
            ActivePlayerPos = _waterPlayer;
            if (_isQ)
            {
                Instantiate(spiritPrefab,  _airPlayer.transform.position, Quaternion.identity);
                _isQ = false;
            }
            if (_isE)
            {
                Instantiate(spiritPrefab,  _earthPlayer.transform.position, Quaternion.identity);
                _isE = false;
            }
            _isSelect = false;
        }
      
    }
    private void AirSelected()
    {
        if (_isSelect)
        {

            fireIndicator.GetComponent<SpriteRenderer>().color = _fireNorm;
            earthIndicator.GetComponent<SpriteRenderer>().color = _earthNorm;
            waterIndicator.GetComponent<SpriteRenderer>().color = _waterNorm;
            airIndicator.GetComponent<SpriteRenderer>().color = new Color(.6f, 1f, 1f, 1f);
            
            ActivePlayerPos = _airPlayer;
            if (_isQ)
            {
                Instantiate(spiritPrefab, _firePlayer.transform.position, Quaternion.identity);
                _isQ = false;
            }
            if (_isE)
            {
                Instantiate(spiritPrefab, _waterPlayer.transform.position, Quaternion.identity);
                _isE = false;
            }
          
            _isSelect = false;
        }
      
    }

  
}
