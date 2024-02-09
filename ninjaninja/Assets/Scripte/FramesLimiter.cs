using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesLimiter : MonoBehaviour
{
    public int _targetFPS = 60;
    public static FramesLimiter Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFPS;
    }
}
