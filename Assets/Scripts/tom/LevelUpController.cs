using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] private AudioClip LevelUPSE;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private  List<CharacterStatus> _characterStatuses = new List<CharacterStatus>();

    private void Start()
    {
        audioSource = GameObject.Find("SoundDirector").GetComponent<SoundDirector>().seSource;
        foreach (CharacterStatus characterStatus in _characterStatuses)
        {
            characterStatus.level = 1;
        }
    }
    public void LevelUP(CharacterStatus status)
    {
        int money = (int)((status.level * 0.2f) * 1000000);
        if (ScoreDirector.score >= money)
        {
            print("levelUP");
            ScoreDirector.score -= money;
            status.level += 1;
            audioSource.PlayOneShot(LevelUPSE);
        }

    }

    //ğŒ‚È‚µ‚Å‚·‚×‚Ä‚ÌƒLƒƒƒ‰‚ÌƒŒƒxƒ‹‚ğã‚°‚éB
    public void AllCharacterLevelUP()
    {
        foreach(CharacterStatus characterStatus in _characterStatuses)
        {
            characterStatus.level += 1;
        }
        audioSource.PlayOneShot(LevelUPSE);
    }
}
