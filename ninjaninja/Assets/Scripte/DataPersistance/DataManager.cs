using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataManager : MonoBehaviour
{

    //[SerializeField] TextMeshProUGUI _bestScoreText;

    public string _bestName = "";
    public int _bestScore;

    public static DataManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameInfo();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetBestScore(int score , string _name)
    {
        if (score > _bestScore)
        {
            _bestScore = score;
            _bestName = _name;
            SaveGameInfo();
            //BestScoreScript.Instance.bestScoreText.text = "Best Score : " + _bestName + " : " + _bestScore;
        }
        Debug.Log("Score:" + score + "Player:" + _name);
    }

    [System.Serializable]
    class SaveData
    {
        public string _name;
        public int _bestScore;
    }

    public void SaveGameInfo()
    {
        SaveData _data = new SaveData();
        _data._name = _bestName;
        _data._bestScore = _bestScore;

        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(Application.persistentDataPath + "/Save.game", json);
    }

    public void LoadGameInfo()
    {
        string path = Application.persistentDataPath + "/Save.game";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData _data = JsonUtility.FromJson<SaveData>(json);

            _bestName = _data._name;
            _bestScore = _data._bestScore;
        }
    }
}
