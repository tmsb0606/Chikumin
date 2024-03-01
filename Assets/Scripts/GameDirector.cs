using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Linq;

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
    private float _timeLimit = 180f;
    private GoalController _goalController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameState gameState = GameState.Start;

    public GameObject ResultPanel;
    public GameObject UIPanel;
    public TextMeshProUGUI resultScore;

    public GameObject ResultContent;
    public GameObject ResultItemLine;

    public ItemDataBase itemDataBase;

    //��ŕ����͎��������ɂ���
    public TextMeshProUGUI wedScore;
    public TextMeshProUGUI wedNum;
    public TextMeshProUGUI jewelryScore;
    public TextMeshProUGUI jewelryNum;

    public PlayableDirector EndPlayableDirector;
    public PlayableDirector ResultPlayableDirector;

    public SceneDirector sceneDirector;

    public CharacterStatus adachikumin;
    public CharacterStatus chiyodakumin;
    public CharacterStatus minatokumin;

    /// <summary>
    /// �|�[�Y�p�̃X�N���v�g������B
    /// </summary>
    [SerializeField]public UnityEvent pauseEvent = new UnityEvent();

    [SerializeField] public  TextAnimator textAnime;
    [SerializeField] public UnityEvent scoreEvent = new UnityEvent();
    void Start()
    {
        _goalController = GameObject.Find("Goal").GetComponent<GoalController>();
        timeText.text = ((int)_timeLimit).ToString();
        //scoreEvent += EvaluateRichText();
        scoreEvent.AddListener(()=> textAnime.setMessage(_goalController.score.ToString()));
        //scoreEvent.AddListener(() => StartCoroutine(textAnime.RunAnimation(0f)));
        //scoreEvent.AddListener(() => textAnime.EvaluateRichText()));
        adachikumin.level = 1;
        chiyodakumin.level = 1;
        minatokumin.level = 1;
        

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                break;
            case GameState.Play:
                //scoreText.text = _goalController.score.ToString();
                scoreEvent.Invoke();
                //StartCoroutine(textAnime.RunAnimation(1f));
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
                if (Input.GetKeyDown("1"))
                {
                    LevelUP(adachikumin);
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
                    //SceneManager.LoadScene("SampleScene");
                    sceneDirector.ChangeScene("SampleScene");
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //SceneManager.LoadScene("TitleScene");
                    sceneDirector.ChangeScene("TitleScene");
                }
                break;
            case GameState.Pause:
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Play;
                    Time.timeScale = 1;
                }
                break;

        }




    }
    public void ChangeState(int state)
    {
         gameState = EasyParse.Enumelate(state, GameState.Start);
    }

    public void LevelUP(CharacterStatus status)
    {
        status.level += 1;
    }

    public void Pause()
    {
        pauseEvent.Invoke();

        Time.timeScale = 0;



    }
    /// <summary>
    /// ���U���g�p�l�����쐬����BResultPlayableDirector������s������\��
    /// </summary>
    public void CreateResultPanel()
    {

        foreach (ItemData item in itemDataBase.itemList)
        {
            

            print(item.itemType);
            if (_goalController.itemDic[item.itemType] != 0)
            {
                GameObject obj = Instantiate(ResultItemLine, ResultContent.transform);
                obj.transform.Find("ItemNumText").GetComponent<TextMeshProUGUI>().text = _goalController.itemDic[item.itemType].ToString();
                obj.transform.Find("ItemMoneyText").GetComponent<TextMeshProUGUI>().text = (_goalController.itemDic[item.itemType] * item.money).ToString();
                obj.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemImage;

            }

        }

    }


}
