using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectTImeExtension : ItemEffectBase
{
    private GameStateController stateController;
    private float plusTime = 10f;
    public override void Effect()
    {
        stateController.timeLimit += plusTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        stateController = GameObject.Find("GameStateDirector").GetComponent<GameStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
