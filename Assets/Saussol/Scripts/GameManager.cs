using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


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

    public GameObject[] spawnChest;

    public string[] cahstNam;

    [SerializeField]
    int seed; // Variable pour stocker la seed
    [SerializeField]
    List<string> seedTab;

    public ChestSo chestSo;

    public bool[] validate;

    void Start()
    {
        int randomSeed = Random.Range(0, 13);
        ReadSeedValues(chestSo.seedList[0]); 
    }

    void ReadSeedValues(string seedLine)
    {
        // Convertit la chaîne de caractères en une liste d'entiers
        string[] numbersAsString = seedLine.Split(',');
        List<int> seedNumbers = new List<int>();

        foreach (string num in numbersAsString)
        {
            if (int.TryParse(num, out int result))
            {
                seedNumbers.Add(result);
            }
            else
            {
                Debug.LogWarning("La conversion de la chaîne en entier a échoué pour : " + num);
            }
        }

        // Traite les 12 nombres de la seed
        for (int i = 0; i < seedNumbers.Count; i++)
        {
            Debug.Log("Nombre " + (i + 1) + " de la seed : " + seedNumbers[i]);
            // Tu peux faire des opérations avec chaque nombre ici

            spawnChest[i].GetComponent<Pointer>().textMeshPro.text = cahstNam[seedNumbers[i]];
            spawnChest[i].GetComponent<Pointer>().chestName = seedNumbers[i];
        }
    }

    void CreateSeed()
    {
        List<int> numbers = GenerateUniqueNumbers(0, 11, 12); // Génère une liste de 12 entiers uniques de 1 à 11
        int[] seedArray = numbers.ToArray(); // Convertit la liste en tableau d'entiers

        string seedString = string.Join(",", seedArray);
        chestSo.seedList.Add(seedString);

        // Utilise la seed générée
        for (int i = 0; i < seedArray.Length; i++)
        {
            Debug.Log("Valeur aléatoire générée : " + seedArray[i]);
            
        }
    }

    List<int> GenerateUniqueNumbers(int minValue, int maxValue, int count)
    {
        if (count > (maxValue - minValue + 1) || count < 0)
        {
            //throw new ArgumentException("Impossible de générer autant de nombres uniques dans la plage spécifiée.");
        }

        List<int> numbers = new List<int>();
        for (int i = minValue; i <= maxValue; i++)
        {
            numbers.Add(i);
        }

        List<int> uniqueNumbers = new List<int>();
        System.Random random = new System.Random();

        while (uniqueNumbers.Count < count)
        {
            int index = random.Next(numbers.Count);
            uniqueNumbers.Add(numbers[index]);
            numbers.RemoveAt(index);
        }

        return uniqueNumbers;
    }

    public void OpenChest(int chestName)
    {
        switch (chestName)
        {
            case 0:// A
                validate[0] = true; 
                break;
            case 1:// B
                if (validate[0])
                {
                    validate[1] = true;
                }
                break;
            case 2:// C
                if (validate[0])
                {
                    validate[2] = true;
                }
                break;
            case 3:// D
                if (validate[1] && validate[2])
                {
                    validate[3] = true;
                }
                break;
            case 4:// E
                if (validate[1] && validate[2])
                {
                    validate[4] = true;
                }
                break;
            case 5:// F
                if (validate[0] && validate[2])
                {
                    validate[5] = true;
                }
                break;
            case 6:// G
                if (validate[5] && validate[3])
                {
                    validate[6] = true;
                }
                break;
            case 7:// H
                if (validate[0] && validate[1])
                {
                    validate[7] = true;
                }
                break;
            case 8:// I
                if (validate[0] && validate[1] && validate[7])
                {
                    validate[8] = true;
                }
                break;
            case 9:// J
                if (validate[8] && validate[7] && validate[6])
                {
                    validate[9] = true;
                }
                break;
            case 10:// K
                if (validate[0] && validate[1] && validate[7] && validate[6])
                {
                    validate[10] = true;
                }
                break;
            case 11:// L
                if (validate[10] && validate[9])
                {
                    validate[11] = true;
                    Debug.Log("you win");
                }
                break;

        }
    }

    /*private void StartRandomLoot(int chestLvl, int seed)
    {

        int firstTwoDigits = seed / 1000; // pour les 2 premier 
        firstTwoDigits %= 100;

        switch (chestLvl)
        {
            case 1:
                if (firstTwoDigits <= 40)//item
                {
                    GetItem(1, seed) ;
                }
                else if (firstTwoDigits >= 41 && firstTwoDigits <= 80)//gold
                {
                    GetGold(1);
                }
                else if (firstTwoDigits >= 81)//chest + key
                {
                    GetChest(1);
                }
                break;
            case 2:
                if (greenBook >= 1)
                {
                    greenBook--;
                    greenBookText.text = greenBook + " Book";

                    if (firstTwoDigits <= 35)//item
                    {
                        GetItem(2, seed);
                    }
                    else if (firstTwoDigits >= 36 && firstTwoDigits <= 70)//gold
                    {
                        GetGold(2);
                    }
                    else if (firstTwoDigits >= 71)//chest + key
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

                    if (firstTwoDigits <= 30)//item
                    {
                        GetItem(3, seed);
                    }
                    else if (firstTwoDigits >= 31 && firstTwoDigits <= 60)//gold
                    {
                        GetGold(3);
                    }
                    else if (firstTwoDigits >= 61)//chest + key
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

                    if (firstTwoDigits <= 25)//item
                    {
                        GetItem(4, seed);
                    }
                    else if (firstTwoDigits >= 26 && firstTwoDigits <= 50)//gold
                    {
                        GetGold(4);
                    }
                    else if (firstTwoDigits >= 51)//chest + key
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

    private void GetItem(int ChestLvl, int seed)
    {

        int thirdAndFourthDigits = (seed / 100) % 100; // Divise par 100 pour décaler les chiffres, puis utilise le modulo 100 pour obtenir le troisième et le quatrième chiffre
        //Debug.Log("Le troisième et le quatrième chiffre : " + thirdAndFourthDigits);

        switch (ChestLvl)// 10 item a 10% par item 
        {
            case 1://80% blue 20% green
                if (thirdAndFourthDigits <= 80)
                {
                    Debug.Log("blue item");
                }
                else
                {
                    Debug.Log("green item");
                }
                break;
            case 2://60% blue 30% green 10% violet
                if (thirdAndFourthDigits <= 60)
                {
                    Debug.Log("blue item");
                }
                else if (thirdAndFourthDigits <= 90)
                {
                    Debug.Log("green item");
                }
                else
                {
                    Debug.Log("violet item");
                }
                break;
            case 3://40% blue 40% green 20% violet
                if (thirdAndFourthDigits <= 40)
                {
                    Debug.Log("blue item");
                }
                else if (thirdAndFourthDigits <= 80)
                {
                    Debug.Log("green item");
                }
                else
                {
                    Debug.Log("violet item");
                }
                break;
            case 4://10% blue 50% green 25% violet 15% red
                if (thirdAndFourthDigits <= 10)
                {
                    Debug.Log("blue item");
                }
                else if (thirdAndFourthDigits <= 60)
                {
                    Debug.Log("green item");
                }
                else if (thirdAndFourthDigits <= 85)
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
        int thirdAndFourthDigits = (seed / 100) % 100; 
        //Debug.Log("Le troisième et le quatrième chiffre : " + thirdAndFourthDigits);

        int addgold = (Mathf.RoundToInt(thirdAndFourthDigits) * ChestLvl) * 25;
        Debug.Log("Add " + addgold + " Gold + " + thirdAndFourthDigits);

        ChangeGold(addgold); // part 2 si c'est seed gold

        string texteListe = string.Join(", ", SeedList);
        //Debug.Log(" Seed is " + texteListe);
    }

    private void GetChest(int ChestLvl)// Get chest or key 
    {
        int thirdAndFourthDigits = (seed / 100) % 100;
        //Debug.Log("Le troisième et le quatrième chiffre : " + thirdAndFourthDigits);

        int fifthAndSixthDigits = seed % 100; 
        //Debug.Log("Le cinquième et le sixième chiffre : " + fifthAndSixthDigits);

        

        switch (ChestLvl)// 20% chest 80% clé
        {
            case 1:
                if (thirdAndFourthDigits <= 20)// 75% green 25% violet
                {
                    if (fifthAndSixthDigits <= 75)
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
                    if (fifthAndSixthDigits >= 75)
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
                if (thirdAndFourthDigits <= 20)// 65% green 30% violet 5% red
                {
                    if (fifthAndSixthDigits <= 65)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else if (thirdAndFourthDigits <= 95)
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
                    if (fifthAndSixthDigits <= 65)
                    {
                        AddBook(1);
                    }
                    else if (thirdAndFourthDigits <= 95)
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
                if (thirdAndFourthDigits <= 20)// 48% green 40% violet 12%  red
                {
                    if (fifthAndSixthDigits <= 48)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else if (thirdAndFourthDigits <= 88)
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
                    if (fifthAndSixthDigits >= 48)
                    {
                        AddBook(1);
                    }
                    else if (thirdAndFourthDigits <= 88)
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
                if (thirdAndFourthDigits <= 20)// 30% green 50% violet 20% red
                {
                    if (fifthAndSixthDigits <= 30)
                    {
                        spawnChest.SwpawnerNewChest(2);// 2 = green
                    }
                    else if (thirdAndFourthDigits <= 80)
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
                    if (fifthAndSixthDigits <= 30)
                    {
                        AddBook(1);
                    }
                    else if (thirdAndFourthDigits <= 80)
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
    */

    //TODO generé un seed 
}

