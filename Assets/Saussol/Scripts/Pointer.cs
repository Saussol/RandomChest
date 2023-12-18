using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public int chestName;

    public TextMeshProUGUI textMeshPro;

    public void OnClickOpenChest()
    {
        if (GameManager._instance.isPaire)
        {
            if ((GameManager._instance.isover35))
            {
                GameManager._instance.OpenChest(chestName);
            }
            else
            {
                GameManager._instance.OpenChestV2(chestName);
            }
        }
        else
        {
            if (GameManager._instance.isover35)
            {
                GameManager._instance.OpenChestV3(chestName);
            }
            else
            {
                GameManager._instance.OpenChestV4(chestName);
            }
        }
        
    }

    public void LateUpdate()
    {

        if (GameManager._instance.validate[chestName])
        {
            Destroy(gameObject);
        }
    }
    private void Good()
    {       
        Destroy(gameObject);
    }
}
