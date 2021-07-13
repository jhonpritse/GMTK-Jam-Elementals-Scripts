using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDefence : MonoBehaviour
{
    private const float LifeTime = 5;
    private const float Speed = 9;
    private  float _defenceDistance = .35f;
    private PlayerData_Object _playerDataObject;
    private Vector2 _lastClickPos;
    private Vector2 _holdPos;
    void Start()
    {
        Destroy(gameObject, LifeTime );
        _lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        if ((Vector2) transform.position != _lastClickPos)
        {
            if (_defenceDistance > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _lastClickPos, Speed * Time.deltaTime);
                _defenceDistance -= Time.deltaTime;
                _holdPos = transform.position;
            }
            else
            {
                // transform.position = _holdPos;
            }
        }
    }
    private void OnDestroy()
    {
        Instantiate(_playerDataObject.attackDisableParticlePrefab, transform.position, Quaternion.identity);
    }
}
