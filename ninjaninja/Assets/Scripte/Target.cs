using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager _gameManager;
    private Rigidbody _targetRb;
    public ParticleSystem _explosionParticle;

    private float _minSpeed = 12;
    private float _maxSpeed = 19;

    private float _maxTorque = 10;

    private float _xRang = 4;
    private float _yPosSpwn = -4.5f;

    public int _pointValue;

    public int _boxCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();
        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRang, _xRang), _yPosSpwn);
    }

    public void DestroyTarget()
    {
        if (_gameManager._isActive)
        {
            Destroy(gameObject);
            Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            _gameManager.UpdateScore(_pointValue);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && _gameManager._isActive )
        {
            _gameManager.UpdateLives(-1);
        }
    }
}
