using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer),typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _camera;
    private Vector3 _mousePosition;
    private TrailRenderer _trailRenderer;
    private BoxCollider _boxCollider;
    private bool _swiping = false;

    private void Awake()
    {
        _camera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>(); 
        _boxCollider = GetComponent<BoxCollider>(); 
        _trailRenderer.enabled = false;
        _boxCollider.enabled = false;
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    void UpdateMousePosition()
    {
        _mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 12f));
        transform.position = _mousePosition;
    }
    void UpdateComponents()
    {
        _trailRenderer.enabled = _swiping;
        _boxCollider.enabled = _swiping;
    }


    // Update is called once per frame
    void Update()
    {
        if (_gameManager._isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _swiping = false;
                UpdateComponents();
            }
            if (_swiping)
            {
                UpdateMousePosition();
            }

            
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
