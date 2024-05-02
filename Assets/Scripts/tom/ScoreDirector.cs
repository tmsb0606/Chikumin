using System.Collections.Generic;
using System;

public static class ScoreDirector
{
    public static int score;
    public static  Dictionary<Item.Type, int> itemDic = new Dictionary<Item.Type, int>();

    public static void Initialization()
    {
        score = 0;
        itemDic = new Dictionary<Item.Type, int>();
        //�擾�A�C�e�����X�g���X�V
        foreach (Item.Type Value in Enum.GetValues(typeof(Item.Type)))
        {
            itemDic.Add(Value, 0);
        }
    }

}
