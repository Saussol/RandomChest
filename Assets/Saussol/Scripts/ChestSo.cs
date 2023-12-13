using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "NouveauScriptableObject", menuName = "Mon Projet/ScriptableObject")]
public class ChestSo : ScriptableObject
{
    public Sprite[] chestColor;
    public Sprite[] Item;// 10 item
    public List<string> seedList;


}
