using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolume : MonoBehaviour
{
    // Start is called before the first frame update

    private SoundDirector _soundDirector;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _seSlider;


    void Start()
    {
        _soundDirector = GetComponent<SoundDirector>();
        _bgmSlider.value = _soundDirector.bgmSource.volume;
        _seSlider.value = _soundDirector. seSource.volume;

        _bgmSlider.onValueChanged.AddListener(delegate { VolumeChange(_bgmSlider, _soundDirector.bgmSource); });
        _seSlider.onValueChanged.AddListener(delegate { VolumeChange(_seSlider, _soundDirector.seSource); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeChange(Slider slider ,AudioSource audioSource)
    {
        audioSource.volume = slider.value;
    }
}
