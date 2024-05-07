using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _titleButton;
    [SerializeField] private GameStateController stateController;
    void Start()
    {
        _continueButton.onClick.AddListener(() => stateController.ChangeState(stateController.playState));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
