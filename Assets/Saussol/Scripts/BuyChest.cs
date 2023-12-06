using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyChest : MonoBehaviour
{
    public int price;

    public void Onclick(int idChest)
    {
        if (GameManager._instance.gold >= price)
        {
            GameManager._instance.ChangeGold(-price);
            GameManager._instance.spawnChest.SwpawnerNewChest(idChest);
        }
        else
        {
            Debug.Log("ta pas de thune");
        }
    }
}
