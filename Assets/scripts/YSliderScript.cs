using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YSliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    [SerializeField] private PlayerCam playercam = null;
    // Start is called before the first frame update
    // void Start()
    // {
    //     _sliderText.text = playercam.getSensY();
    //     _slider.SetValueWithoutNotify(playercam.getFloatSensY());
    //     _slider.onValueChanged.AddListener((v) =>{
    //         _sliderText.text = v.ToString();
    //         playercam.setSensY(v);
    //     });
    // }

    void Start()
    {
        
        SetSlider(PlayerPrefs.GetFloat("SavedSensY", 4f));
        _sliderText.text = playercam.getSensY();
        _slider.SetValueWithoutNotify(playercam.getFloatSensY());
        _slider.onValueChanged.AddListener((v) =>{
            _sliderText.text = v.ToString("0");
            //playercam.setSensY(v);
        });
    }

    public void SetSlider(float _value)
    {
        if(_value < 1){
            _value = 1f;
        }
        RefreshSlider(_value);

        PlayerPrefs.SetFloat("SavedSensY", _value);
        playercam.setSensY(_value);
        _slider.value = _value;
    }

    public void SetSliderFromSlider(float _value)
    {
        SetSlider(_slider.value);
    }

    public void RefreshSlider(float _value)
    {
        _slider.value = _value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
