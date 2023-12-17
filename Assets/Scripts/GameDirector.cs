using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    private int _score = 0;
    private GoalController _goalController;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        _goalController = GameObject.Find("Goal").GetComponent<GoalController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = _goalController.score.ToString()+"‰~";
    }
}
