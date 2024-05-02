using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private UnityEvent _scoreEvent = new UnityEvent();
    [SerializeField] private TextAnimator _textAnime;
    private void Start()
    {
        _scoreEvent.AddListener(() => _textAnime.setMessage(ScoreDirector.score.ToString()));
    }
    private void Update()
    {
        _scoreEvent.Invoke();
        
    }
}
