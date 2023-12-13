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
        GameManager._instance.OpenChest(chestName);
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
