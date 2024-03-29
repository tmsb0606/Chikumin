using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolume : MonoBehaviour
{
    // Start is called before the first frame update

    private SoundDirector _soundDirector;
    [SerializeField] public Slider bgmSlider;
    [SerializeField] public Slider seSlider;

    [SerializeField]private AudioClip _testSE;


    void Start()
    {
        _soundDirector = GameObject.Find("SoundDirector").GetComponent<SoundDirector>();


        bgmSlider.value = _soundDirector.bgmSource.volume;
        seSlider.value = _soundDirector. seSource.volume;

        bgmSlider.onValueChanged.AddListener(delegate { VolumeChange(bgmSlider, _soundDirector.bgmSource); });
        seSlider.onValueChanged.AddListener(delegate { VolumeChange(seSlider, _soundDirector.seSource); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeChange(Slider slider ,AudioSource audioSource)
    {
        audioSource.volume = slider.value;
    }
    public void PlaySE()
    {
        _soundDirector.seSource.PlayOneShot(_testSE);
    }
}
