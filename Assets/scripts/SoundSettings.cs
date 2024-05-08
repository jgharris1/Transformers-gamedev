using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        _sliderText.text = soundSlider.value.ToString("0.0");
        soundSlider.onValueChanged.AddListener((v) =>{
            _sliderText.text = v.ToString("0.0");
        });
    }

    public void SetVolume(float _value)
    {
        if(_value < 1){
            _value = 0.001f;
        }
        RefreshSlider(_value);

        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value/100)*20f);
        soundSlider.value = _value;
    }

    public void SetVolumeFromSlider(float _value)
    {
        SetVolume(soundSlider.value);
    }
    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
