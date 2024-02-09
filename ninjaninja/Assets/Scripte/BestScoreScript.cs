using UnityEngine;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BestScoreScript : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {
    public TextMeshProUGUI bestScoreText;

    public string sceneToLoad = "Game";
    public string _ndSceneToLoad = "menu";

    [SerializeField] Button _resumButton;

    private void Start()
    {
        DataManager.Instance.LoadGameInfo();

        bestScoreText.text = "Best Score " +"\n" + "\n"+ DataManager.Instance._bestName+"\n"+ "\n"+ DataManager.Instance._bestScore;
    }

    public void TryResumeGame() {
        ShowAd();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadAd() {
        Advertisement.Load(AdManager.Instance._rewardedVideoPlacementId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId) {Debug.Log("Loading Ad: " + placementId);}

    public void ShowAd() {
        LoadAd();
        Advertisement.Show(AdManager.Instance._rewardedVideoPlacementId, this);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_ndSceneToLoad);
    }


    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) {

        switch (showCompletionState) {
            case UnityAdsShowCompletionState.UNKNOWN:
                MainMenu();
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                SceneManager.LoadScene(sceneToLoad);
                break;
            default:
                break;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) {
        Debug.Log(message);
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) {
        Debug.Log(message);
    }
    public void OnUnityAdsShowStart(string placementId) {}

    public void OnUnityAdsShowClick(string placementId) {}

    void OnDestroy() {
        // Clean up the button listeners:
        _resumButton.onClick.RemoveAllListeners();
    }
}
