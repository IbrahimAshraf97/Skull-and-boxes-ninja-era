using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public List<GameObject> _targets;

    private float _spwnRate = 1.0f;

    public int score;

    public TextMeshProUGUI _scoreText;

    public string sceneToLoad = "highscore";

    public bool _isActive;

    public GameObject _titleScreen;

    public TextMeshProUGUI _livesText;

    private int _lives;

    public GameObject _pauseScreen;

    private bool _paused;

    public static GameManager Instance;

    private string _name;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        print(Screen.currentResolution);

        _name = MainMenuUI.Instance._name;

        InvokeRepeating("PlusLive", 30.0f, 45.0f);
    }


    IEnumerator SpawnTarget()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(_spwnRate);

            int randomIndex = Random.Range(0,_targets.Count);

            Instantiate(_targets[randomIndex]);

        }
    }
    public void UpdateScore(int addScore)
    {
        score += addScore;
        _scoreText.text = "Score : " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        _lives += livesToChange;
        _livesText.text = "Lives : " + _lives;
        if (_lives <= 0)
        {
            GameOverFn();
        }
    }

    private void PlusLive() {
        UpdateLives(1);
    }


    public void GameOverFn()
    {
        DataManager.Instance.SetBestScore(score,_name);
        _isActive = false;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void StartGame(float _defficulty)
    {
        _isActive = true;

        _scoreText.gameObject.SetActive(true);
        _livesText.gameObject.SetActive(true);

        StartCoroutine(SpawnTarget());

        UpdateScore(0);
        UpdateLives(3);

        _titleScreen.gameObject.SetActive(false);

        _spwnRate /= _defficulty;
    }
    void ChangePause()
    {
        if (!_paused)
        {
            _paused = true;
            _pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _paused = false;
            _pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
// Update is called once per frame
    void Update()
    {
      
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
    }
    public void QuitTheApp()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
