using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSreen : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _scalingFactor;
    [SerializeField] float _scalingTimer;
    [SerializeField] int _timesToScale;
    [SerializeField] float _destroyDelay;


    CapsuleCollider2D _trigger;
    bool _trigged;
    float _timer;
    float _delayTimer;
    int _timesScaled;
    private void Start()
    {
        //_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _trigger = GetComponent<CapsuleCollider2D>();
        //_timesToScale--;
        _delayTimer = _destroyDelay;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(gameObject.transform.position, _trigger.size.magnitude);
        
    }
    private void Update()
    {
        if (_trigged && _timesScaled < _timesToScale)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                gameObject.transform.localScale = gameObject.transform.localScale * _scalingFactor;
                _timesScaled++;
                _timer = _scalingTimer;
                //Debug.Log(_timesScaled);
            }
        }
        else if (_trigged && _timesScaled == _timesToScale)
        {
            _delayTimer -= Time.deltaTime;
            if (_delayTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            if (!_trigged)
            {
                StartScaling();
            }
            
        }
    }

    private void StartScaling()
    {
        //Debug.Log("Scaling Started");
        _spriteRenderer.gameObject.SetActive(true);
        _timer = _scalingTimer;
        _trigged = true;
    }
}
