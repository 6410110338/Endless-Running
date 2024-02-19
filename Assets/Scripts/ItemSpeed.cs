using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : ItemManager
{
    [SerializeField] private int speedSet;

    protected override void Effectitem()
    {
        player.SetSpeed(speedSet);
    }
}
