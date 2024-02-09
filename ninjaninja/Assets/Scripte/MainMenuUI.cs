using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameField;
    [SerializeField] private TextMeshProUGUI _warnningText;
    public string _name = "";
    public string sceneToLoad = "Game";

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _QuitButton;

    public static MainMenuUI Instance;


    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playButton.onClick.AddListener(() => {
            HanddelInputField();
            SoundSettingManager.Instance.SaveSoundSetting();
        });

        _QuitButton.onClick.AddListener(() => {
            Exit();
        });
    }

    private void HanddelInputField() {
        if (_nameField.text != "") {
            _name = _nameField.text.ToString();
            SceneManager.LoadScene(sceneToLoad);
        } else {
            _warnningText.gameObject.SetActive(true);
            print("Type Ur name");

            Debug.LogWarning("Please enter a name!");
        }
    }
    public void Exit() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
    Debug.Log("Exiting ....");
#endif
    }
}
