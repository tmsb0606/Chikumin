using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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
    private float _timeLimit = 5f;
    private GoalController _goalController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameState gameState = GameState.Start;

    public GameObject ResultPanel;
    public GameObject UIPanel;
    public TextMeshProUGUI resultScore;

    //��ŕ����͎��������ɂ���
    public TextMeshProUGUI wedScore;
    public TextMeshProUGUI wedNum;
    public TextMeshProUGUI jewelryScore;
    public TextMeshProUGUI jewelryNum;

    public PlayableDirector EndPlayableDirector;
    public PlayableDirector ResultPlayableDirector;

    /// <summary>
    /// �|�[�Y�p�̃X�N���v�g������B
    /// </summary>
    [SerializeField]public UnityEvent pauseEvent = new UnityEvent();
    void Start()
    {
        _goalController = GameObject.Find("Goal").GetComponent<GoalController>();
        timeText.text = ((int)_timeLimit).ToString();
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
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                    gameState = GameState.Pause;
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
                //���U���g�p�l���͌�ŏ���������炵���B
                //wedNum.text = _goalController.itemNum[0].ToString();
                //wedScore.text = (_goalController.itemNum[0] * 1000000).ToString();
                //jewelryNum.text = _goalController.itemNum[1].ToString();
                //jewelryScore.text = (_goalController.itemNum[1] * 1000000).ToString();
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
            case GameState.Pause:
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Play;
                }
                break;

        }




    }
    public void ChangeState(int state)
    {
         gameState = EasyParse.Enumelate(state, GameState.Start);
    }

    public void Pause()
    {
        pauseEvent.Invoke();


    }


}
