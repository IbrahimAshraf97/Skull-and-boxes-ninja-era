using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static readonly string BackGroundPref = "BackGroundPref";
    private float _bkGroundSliderValue;
    public AudioSource _bkGroundAudioSource;

    void Awake()
    {
        ContinueSetting();
    }
    public void ContinueSetting()
    {
        _bkGroundSliderValue = PlayerPrefs.GetFloat(BackGroundPref);

        _bkGroundAudioSource.volume = _bkGroundSliderValue;
    }
}
