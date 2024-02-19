using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScore : ItemManager
{
    [SerializeField] private int addScore = 10;
    protected override void Effectitem()
    {
        player.GetComponent<UIScore>().AddScore(addScore);
    }
}
