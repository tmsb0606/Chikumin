using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUPUIController : MonoBehaviour
{
    public CharacterStatus status;
    public GoalController goalController;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyText;
    public Image LevelUPImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Lv." + status.level;
        moneyText.text = ((status.level) * 1000000).ToString();
        if ((status.level*0.2) * 1000000 <= goalController.score)
        {
            print("レベルアップ可能");
            LevelUPImage.color = Color.Lerp(new Color32(255,255,255,0),new Color32(255,255,255,255), Mathf.PingPong(Time.time / 1.0f, 1.0f));
        }
        else
        {
            LevelUPImage.color = new Color32(255, 255, 255, 0);
        }
    }
}
