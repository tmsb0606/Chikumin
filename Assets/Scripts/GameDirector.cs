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
        ResultProduction,
        Result,
    }
    // Start is called before the first frame update
    private int _score = 0;
    private float _timeLimit = 60f;
    private GoalController _goalController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameState gameState = GameState.Start;

    public GameObject ResultPanel;
    public GameObject UIPanel;
    public TextMeshProUGUI resultScore;

    //å„Ç≈ï∂éöÇÕé©ìÆê∂ê¨Ç…Ç∑ÇÈ
    public TextMeshProUGUI wedScore;
    public TextMeshProUGUI wedNum;
    public TextMeshProUGUI jewelryScore;
    public TextMeshProUGUI jewelryNum;

    public PlayableDirector EndPlayableDirector;
    public PlayableDirector ResultPlayableDirector;
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
                scoreText.text = _goalController.score.ToString();
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
            case GameState.ResultProduction:
                //SceneManager.LoadScene("ResultScene");
                UIPanel.SetActive(false);
                ResultPanel.SetActive(true);
                resultScore.text = _goalController.score.ToString();
                wedNum.text = _goalController.itemNum[0].ToString();
                wedScore.text = (_goalController.itemNum[0] * 1000000).ToString();
                jewelryNum.text = _goalController.itemNum[1].ToString();
                jewelryScore.text = (_goalController.itemNum[1] * 1000000).ToString();
                ResultPlayableDirector.Play();
                break;
            case GameState.Result:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("SampleScene");
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("TitleScene");
                }
                break;

        }




    }
    public void ChangeState(int state)
    {
         gameState = EasyParse.Enumelate(state, GameState.Start);
    }


}
