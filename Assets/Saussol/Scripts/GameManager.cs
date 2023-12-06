using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private void Awake()
    {
        // Vérifie s'il y a déjà une instance du GameManager
        if (_instance != null && _instance != this)
        {
            // Si une instance existe déjà et ce n'est pas celle-ci, détruit cet objet
            Destroy(gameObject);
            return;
        }
        else
        {
            // Si ce GameManager est la première instance ou la seule, le définit comme instance unique
            _instance = this;
        }

        // Assure que cet objet ne sera pas détruit lors du chargement des nouvelles scènes
        DontDestroyOnLoad(gameObject);

        // Initialisez ici vos variables ou paramètres du GameManager si nécessaire
    }

    public SpawnerChest spawnChest;

    private int[] chestRarety; // 1=normal 2=green 3=violet 4=red 

    public int gold = 0;
    public int greenBook = 0;
    public int violetBook = 0;
    public int redBook = 0;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI greenBookText;
    public TextMeshProUGUI violetBookText;
    public TextMeshProUGUI redBookText;

    private List<int> SeedList = new List<int>();

    public void OpenChest(int chestLvl)
    {
        StartRandomLoot(chestLvl);

        spawnChest.chestNum--;
        spawnChest.textMeshProUGUI.text = "You have " + spawnChest.chestNum + " chest";
    }

    private void StartRandomLoot(int chestLvl) 
    {
        SeedList.Clear();

        float randNum = Random.Range(0f, 101f); // part 1 de la seed
        int randomNum = Mathf.RoundToInt(randNum);

        SeedList.Add(chestLvl);
        SeedList.Add(randomNum);

        switch (chestLvl)
        {
            case 1:
                if (randomNum <= 40)//item
                {
                    GetItem(1);
                }
                else if(randomNum >= 41 && randomNum <= 80)//gold
                {
                    GetGold(1);
                }
                else if(randomNum >= 81)//chest + key
                {
                    GetChest(1);
                }
                break;
            case 2:
                if (greenBook >= 1)
                {
                    greenBook--;
                    greenBookText.text = greenBook + " Book";

                    if (randomNum <= 35)//item
                    {
                        GetItem(2);
                    }
                    else if (randomNum >= 36 && randomNum <= 70)//gold
                    {
                        GetGold(2);
                    }
                    else if (randomNum >= 71)//chest + key
                    {
                        GetChest(2);
                    }
                }
                else
                {
                    Debug.Log("you need green key");
                }
                break;
            case 3:
                if (violetBook >= 1)
                {
                    violetBook--;
                    violetBookText.text = violetBook + " Book";

                    if (randomNum <= 30)//item
                    {
                        GetItem(3);
                    }
                    else if (randomNum >= 31 && randomNum <= 60)//gold
                    {
                        GetGold(3);
                    }
                    else if (randomNum >= 61)//chest + key
                    {
                        GetChest(3);
                    }
                }
                else
                {
                    Debug.Log("You need violet key");
                }

                break;
            case 4:
                if (redBook >= 1)
                {
                    redBook--;
                    redBookText.text = redBook + " Book";

                    if (randomNum <= 25)//item
                    {
                        GetItem(4);
                    }
                    else if (randomNum >= 26 && randomNum <= 50)//gold
                    {
                        GetGold(4);
                    }
                    else if (randomNum >= 51)//chest + key
                    {
                        GetChest(4);
                    }
                }
                else
                {
                    Debug.Log("you need red key");
                }

                break;
        }
    }

    private void GetItem(int ChestLvl)
    {
        float randitemNum = Random.Range(0f, 101f);
        int randitemomNum = Mathf.RoundToInt(randitemNum);// part 2 item seed

        SeedList.Add(ChestLvl);
        SeedList.Add(randitemomNum);

        switch (ChestLvl)// 10 item a 10% par item 
        {
            case 1://80% blue 20% green
                if (randitemomNum <= 80)
                {
                    Debug.Log("blue item");
                }
                else
                {
                    Debug.Log("green item");
                }
                break;
            case 2://60% blue 30% green 10% violet
                if (randitemomNum <= 60)
                {
                    Debug.Log("blue item");
                }
                else if(randitemomNum <= 90)
                {
                    Debug.Log("green item");
                }
                else
                {
                    Debug.Log("violet item");
                }
                break;
            case 3://40% blue 40% green 20% violet
                if (randitemomNum <= 40)
                {
                    Debug.Log("blue item");
                }
                else if (randitemomNum <= 80)
                {
                    Debug.Log("green item");
                }
                else
                {
                    Debug.Log("violet item");
                }
                break;
            case 4://10% blue 50% green 25% violet 15% red
                if (randitemomNum <= 10)
                {
                    Debug.Log("blue item");
                }
                else if (randitemomNum <= 60)
                {
                    Debug.Log("green item");
                }
                else if (randitemomNum <= 85)
                {
                    Debug.Log("violet item");
                }
                else
                {
                    Debug.Log("red item");
                }
                break;
        }

        string texteListe = string.Join(", ", SeedList);
        Debug.Log(" Seed is " + texteListe);
    }

    private void GetGold(int ChestLvl)
    {
        float randNum = Random.Range(10f, 101f);
        int addgold = (Mathf.RoundToInt(randNum) * ChestLvl) * 5;

        SeedList.Add(ChestLvl);
        SeedList.Add(Mathf.RoundToInt(randNum));
        Debug.Log("Add " + addgold + " Gold");

        ChangeGold(addgold); // part 2 si c'est seed gold

        string texteListe = string.Join(", ", SeedList);
        Debug.Log(" Seed is " + texteListe);
    }

    private void GetChest(int ChestLvl)// Get chest or key 
    {
        float randNum = Random.Range(0f, 101f);
        int randomNum = Mathf.RoundToInt(randNum);

        float randChestOrKey = Random.Range(0f, 101f);
        int randChestOrKeyInt = Mathf.RoundToInt(randChestOrKey);

        SeedList.Add(ChestLvl);
        SeedList.Add(randomNum); 
        SeedList.Add(randChestOrKeyInt);

        switch (ChestLvl)// 20% chest 80% clé
        {            
            case 1:
                if (randomNum <= 20)// 75% green 25% violet
                {
                    if (randChestOrKeyInt <= 75)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else
                    {
                        spawnChest.SwpawnerNewChest(3);// 3 = violet
                    }
                }
                else// for key chest lvl 1
                {
                    if (randChestOrKeyInt >= 75)
                    {
                        AddBook(1);
                    }
                    else
                    {
                        AddBook(2);
                    }
                }
                break;
            case 2:
                if (randomNum <= 20)// 65% green 30% violet 5% red
                {
                    if (randChestOrKeyInt <= 65)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else if(randomNum <= 95)
                    {
                        spawnChest.SwpawnerNewChest(3);// 3 = violet
                    }
                    else
                    {
                        spawnChest.SwpawnerNewChest(4);// 4 = red   
                    }
                }
                else
                {
                    if (randChestOrKeyInt <= 65)
                    {
                        AddBook(1);
                    }
                    else if (randomNum <= 95)
                    {
                        AddBook(2);
                    }
                    else
                    {
                        AddBook(3);
                    }
                }
                break;
            case 3:
                if (randomNum <= 20)// 48% green 40% violet 12%  red
                {
                    if (randChestOrKeyInt <= 48)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else if (randomNum <= 88)
                    {
                        spawnChest.SwpawnerNewChest(3);// 3 = violet
                    }
                    else
                    {
                        spawnChest.SwpawnerNewChest(4);// 4 = red   
                    }
                }
                else
                {
                    if (randChestOrKeyInt >= 48)
                    {
                        AddBook(1);
                    }
                    else if (randomNum <= 88)
                    {
                        AddBook(2);
                    }
                    else
                    {
                        AddBook(3);
                    }
                }
                break;
            case 4:
                if (randomNum <= 20)// 30% green 50% violet 20% red
                {
                    if (randChestOrKeyInt <= 30)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else if (randomNum <= 80)
                    {
                        spawnChest.SwpawnerNewChest(3);// 3 = violet
                    }
                    else
                    {
                        spawnChest.SwpawnerNewChest(4);// 4 = red   
                    }
                }
                else
                {
                    if (randChestOrKeyInt <= 30)
                    {
                        AddBook(1);
                    }
                    else if (randomNum <= 80)
                    {
                        AddBook(2);
                    }
                    else
                    {
                        AddBook(3);
                    }
                }
                break;

        }

        string texteListe = string.Join(", ", SeedList);
        Debug.Log(" Seed is " + texteListe);
    }



    public void ChangeGold(int golde)// fonction pour add ou remove le gold 
    {
        if (golde >= 0)
        {
            gold += golde;
            goldText.text = gold + " Gold";
            return;
        }
        else
        {
            gold += golde;
            goldText.text = gold + " Gold";
        }

        
    }
    public void AddBook(int book)
    {
        switch (book)
        {
            case 1:
                greenBook++;
                greenBookText.text = greenBook + " Book";
                break;
            case 2:
                violetBook++;
                violetBookText.text = violetBook + " Book";
                break;
            case 3:
                redBook++;
                redBookText.text = redBook + " Book";
                break;
        }

        Debug.Log("add book");
    }


    //TODO generé un seed 
}
