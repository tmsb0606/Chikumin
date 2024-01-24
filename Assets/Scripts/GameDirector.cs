using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Play,
        Pause,
        End,
        Result,
    }
    // Start is called before the first frame update
    private int _score = 0;
    private float _timeLimit = 100f;
    private GoalController _goalController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameState gameState = GameState.Start;

    public PlayableDirector EndPlayableDirector;
    void Start()
    {
        _goalController = GameObject.Find("Goal").GetComponent<GoalController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                break;
            case GameState.Play:
                scoreText.text = _goalController.score.ToString() + "Y";
                _timeLimit -= Time.deltaTime;
                timeText.text = ((int)_timeLimit).ToString();
                if (_timeLimit <= 0)
                {
                    gameState = GameState.End;
                }
                break;
            case GameState.End:
                EndPlayableDirector.Play();
                break;
            case GameState.Result:
                SceneManager.LoadScene("ResultScene");
                break;

        }




    }
    public void ChangeState(int state)
    {
         gameState = EasyParse.Enumelate(state, GameState.Start);
    }


}
