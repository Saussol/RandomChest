using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public int chestLvl;

    public Image colorchest;

    public void OnClickOpenChest()
    {     
        switch (chestLvl)
        {
            case 1:
                Good();
                break;
            case 2:
                if (GameManager._instance.greenBook >= 1)
                {
                    Good();
                }
                break;
            case 3:
                if (GameManager._instance.violetBook >= 1)
                {
                    Good();
                }
                break;
            case 4:
                if (GameManager._instance.redBook >= 1)
                {
                    Good();
                }
                break;
        }       
    }

    private void Good()
    {
        GameManager._instance.OpenChest(chestLvl);
        Destroy(gameObject);
    }
}
