using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUPUIController : MonoBehaviour
{
    public CharacterStatus status;
    public GoalController goalController;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Lv." + status.level;
        moneyText.text = ((status.level * 0.1f) * 1000000).ToString();
    }
}
