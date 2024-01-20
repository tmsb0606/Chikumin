using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    private int _score = 0;
    private float _timeLimit = 100f;
    private GoalController _goalController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    void Start()
    {
        _goalController = GameObject.Find("Goal").GetComponent<GoalController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = _goalController.score.ToString()+"Y";

        _timeLimit -= Time.deltaTime;

        timeText.text = ((int)_timeLimit).ToString();
    }
}
