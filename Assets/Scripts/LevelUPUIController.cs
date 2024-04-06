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
    public Image LevelUPNormalImage;
    [SerializeField] private CanvasGroup costGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Lv." + status.level;
        moneyText.text = ((status.level) * 1000000).ToString();
        if ((status.level*0.2) * 1000000 <= ScoreDirector.score)
        {
            print("レベルアップ可能");
            LevelUPImage.color = Color.Lerp(new Color32(255,255,255,0),new Color32(255,255,255,255), Mathf.PingPong(Time.time / 1.0f, 1.0f));
            //moneyText.color = new Color(255,255,255);
            LevelUPNormalImage.color = new Color32(255,255,255,255);
            costGroup.alpha = 1;
        }
        else
        {
            LevelUPNormalImage.color = new Color32(50,50,50,255);
           // moneyText.color = new Color(0, 0, 0);
            LevelUPImage.color = new Color32(255, 255, 255, 0);
            costGroup.alpha = 0.5f;
        }
    }
}
