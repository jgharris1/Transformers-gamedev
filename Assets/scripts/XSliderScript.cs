using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XSliderScript : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    [SerializeField] PlayerCam playercam = null;
    // Start is called before the first frame update
    void Start()
    {
        
        SetSlider(PlayerPrefs.GetFloat("SavedSensX", 4f));
        _sliderText.text = playercam.getSensX();
        _slider.SetValueWithoutNotify(playercam.getFloatSensX());
        _slider.onValueChanged.AddListener((v) =>{
            _sliderText.text = v.ToString("0");
            //playercam.setSensX(v);
        });
    }

    public void SetSlider(float _value)
    {
        if(_value < 1){
            _value = 1f;
        }
        RefreshSlider(_value);

        PlayerPrefs.SetFloat("SavedSensX", _value);
        playercam.setSensX(_value);
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
