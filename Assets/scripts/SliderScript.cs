using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    [SerializeField] private PlayerCam playercam = null;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>{
            _sliderText.text = v.ToString();
            playercam.setSensX(v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
