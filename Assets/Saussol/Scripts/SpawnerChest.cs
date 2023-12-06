using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerChest : MonoBehaviour
{
    public ChestSo scriptableObject; // Référence vers votre ScriptableObject

    public GameObject prefab; // Le prefab de GameObject que vous voulez instancier
    public TextMeshProUGUI textMeshProUGUI;

    public int chestNum;

    void Start()
    {
        if (scriptableObject != null && prefab != null)
        {
            for (int i = 0; i < 10; i++)
            {
                chestNum++;
                GameObject nouvelObjet = Instantiate(prefab, this.transform);              
                nouvelObjet.GetComponent<Pointer>().colorchest.sprite = scriptableObject.chestColor[0];    
                nouvelObjet.GetComponent<Pointer>().chestLvl = 1;

                textMeshProUGUI.text = "You have " + chestNum + " chest";
            }
        }
        else
        {
            Debug.LogWarning("ScriptableObject ou prefab non défini !");
        }
    }

    public void SwpawnerNewChest(int chestLvl)
    {
        chestNum++;

        GameObject nouvelObjet = Instantiate(prefab, this.transform);
        nouvelObjet.GetComponent<Pointer>().colorchest.sprite = scriptableObject.chestColor[chestLvl - 1];
        nouvelObjet.GetComponent<Pointer>().chestLvl = chestLvl;

        textMeshProUGUI.text = "You have " + chestNum + " chest";
    }

    
}
