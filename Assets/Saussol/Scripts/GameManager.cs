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
        // V�rifie s'il y a d�j� une instance du GameManager
        if (_instance != null && _instance != this)
        {
            // Si une instance existe d�j� et ce n'est pas celle-ci, d�truit cet objet
            Destroy(gameObject);
            return;
        }
        else
        {
            // Si ce GameManager est la premi�re instance ou la seule, le d�finit comme instance unique
            _instance = this;
        }

        // Assure que cet objet ne sera pas d�truit lors du chargement des nouvelles sc�nes
        DontDestroyOnLoad(gameObject);

        // Initialisez ici vos variables ou param�tres du GameManager si n�cessaire
    }

    public GameObject[] spawnChest;

    public string[] cahstNam;

    [SerializeField]
    int seed; // Variable pour stocker la seed
    [SerializeField]
    List<int> seedTab;

    public ChestSo chestSo;

    public bool[] validate;

    public TextMeshProUGUI[] listePass;

    public bool isPaire;
    public bool isover35;

    void Start()
    {
        int randomSeed = Random.Range(0, 13);
        ReadSeedValues(chestSo.seedList[0]);

        CreatePass(chestSo.seedList[0]);
    }

    void CreatePass(string seedLine)
    {
        // Convertit la cha�ne de caract�res en une liste d'entiers
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
                Debug.LogWarning("La conversion de la cha�ne en entier a �chou� pour : " + num);
            }
        }
        // Traite les 12 nombres de la seed

        if (seedNumbers[0] % 2 ==0)
        {
            isPaire = true;
        }
        else
        {
            isPaire= false;
        }

        if (seedNumbers[0] * 5 >= 35)
        {
            isover35 = true;
        }
        else
        {
            isover35 = false;
        }

        SelectedRoot();
    }

    void SelectedRoot()
    {
        if (isPaire)
        {
            if (isover35)
            {
                SelectedText();
            }
            else
            {
                SelectedTextV2();
            }
        }
        else
        {
            if (isover35)
            {
                SelectedTextV3();
            }
            else
            {
                SelectedTextV4();
            }
        }
    }

    void ReadSeedValues(string seedLine)
    {
        // Convertit la cha�ne de caract�res en une liste d'entiers
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
                Debug.LogWarning("La conversion de la cha�ne en entier a �chou� pour : " + num);
            }
        }

        // Traite les 12 nombres de la seed
        for (int i = 0; i < seedNumbers.Count; i++)
        {
            // Debug.Log("Nombre " + (i + 1) + " de la seed : " + seedNumbers[i]);
            // Tu peux faire des op�rations avec chaque nombre ici

            spawnChest[i].GetComponent<Pointer>().textMeshPro.text = cahstNam[seedNumbers[i]];
            //spawnChest[i].GetComponent<Pointer>().chestName = seedNumbers[i];
        }
    }

    void CreateSeed()
    {
        List<int> numbers = GenerateUniqueNumbers(0, 11, 12); // G�n�re une liste de 12 entiers uniques de 1 � 11
        int[] seedArray = numbers.ToArray(); // Convertit la liste en tableau d'entiers

        string seedString = string.Join(",", seedArray);
        chestSo.seedList.Add(seedString);

        // Utilise la seed g�n�r�e
        for (int i = 0; i < seedArray.Length; i++)
        {
            Debug.Log("Valeur al�atoire g�n�r�e : " + seedArray[i]);
            
        }
    }

    List<int> GenerateUniqueNumbers(int minValue, int maxValue, int count)
    {
        if (count > (maxValue - minValue + 1) || count < 0)
        {
            //throw new ArgumentException("Impossible de g�n�rer autant de nombres uniques dans la plage sp�cifi�e.");
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


    void SelectedText()// text pour solus A 
    {
        listePass[0].text = cahstNam[0] + " ---} " + cahstNam[1];  // A ---} B
        listePass[1].text = cahstNam[0] + " ---} " + cahstNam[2];
        listePass[2].text = cahstNam[0] + cahstNam[2] + " ---} " + cahstNam[5];
        listePass[3].text = cahstNam[0] + cahstNam[1] +" ---} " + cahstNam[7];
        listePass[4].text = cahstNam[0] + cahstNam[1] + cahstNam[7] +" ---} " + cahstNam[8];
        listePass[5].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + cahstNam[6] + " ---} " + cahstNam[10];

        listePass[6].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[3];
        listePass[7].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[4];
        listePass[8].text = cahstNam[5] + cahstNam[3] + " ---} " + cahstNam[6];
        listePass[9].text = cahstNam[8] + cahstNam[7] + cahstNam[9] + " ---} " + cahstNam[10];
        listePass[10].text = cahstNam[10] + cahstNam[9] + " ---} " + cahstNam[11];
    }

    void SelectedTextV2()// text pour solus A 
    {
        listePass[0].text = cahstNam[0] + " ---} " + cahstNam[1];  // A ---} B
        listePass[1].text = cahstNam[0] + " ---} " + cahstNam[2];
        listePass[2].text = cahstNam[0] + cahstNam[2] + " ---} " + cahstNam[5];
        listePass[3].text = cahstNam[0] + cahstNam[1] + " ---} " + cahstNam[7];
        listePass[4].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + " ---} " + cahstNam[8];
        listePass[5].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + cahstNam[6] + " ---} " + cahstNam[10];

        listePass[6].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[3];
        listePass[7].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[4];
        listePass[8].text = cahstNam[5] + cahstNam[3] + " ---} " + cahstNam[6];
        listePass[9].text = cahstNam[8] + cahstNam[7] + cahstNam[9] + " ---} " + cahstNam[10];
        listePass[10].text = cahstNam[10] + cahstNam[9] + " ---} " + cahstNam[11];
    }
    void SelectedTextV3()// text pour solus A 
    {
        listePass[0].text = cahstNam[0] + " ---} " + cahstNam[1];  // A ---} B
        listePass[1].text = cahstNam[0] + " ---} " + cahstNam[2];
        listePass[2].text = cahstNam[0] + cahstNam[2] + " ---} " + cahstNam[5];
        listePass[3].text = cahstNam[0] + cahstNam[1] + " ---} " + cahstNam[7];
        listePass[4].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + " ---} " + cahstNam[8];
        listePass[5].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + cahstNam[6] + " ---} " + cahstNam[10];

        listePass[6].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[3];
        listePass[7].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[4];
        listePass[8].text = cahstNam[5] + cahstNam[3] + " ---} " + cahstNam[6];
        listePass[9].text = cahstNam[8] + cahstNam[7] + cahstNam[9] + " ---} " + cahstNam[10];
        listePass[10].text = cahstNam[10] + cahstNam[9] + " ---} " + cahstNam[11];
    }
    void SelectedTextV4()// text pour solus A 
    {
        listePass[0].text = cahstNam[0] + " ---} " + cahstNam[1];  // A ---} B
        listePass[1].text = cahstNam[0] + " ---} " + cahstNam[2];
        listePass[2].text = cahstNam[0] + cahstNam[2] + " ---} " + cahstNam[5];
        listePass[3].text = cahstNam[0] + cahstNam[1] + " ---} " + cahstNam[7];
        listePass[4].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + " ---} " + cahstNam[8];
        listePass[5].text = cahstNam[0] + cahstNam[1] + cahstNam[7] + cahstNam[6] + " ---} " + cahstNam[10];

        listePass[6].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[3];
        listePass[7].text = cahstNam[1] + cahstNam[2] + " ---} " + cahstNam[4];
        listePass[8].text = cahstNam[5] + cahstNam[3] + " ---} " + cahstNam[6];
        listePass[9].text = cahstNam[8] + cahstNam[7] + cahstNam[9] + " ---} " + cahstNam[10];
        listePass[10].text = cahstNam[10] + cahstNam[9] + " ---} " + cahstNam[11];
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
    public void OpenChestV2(int chestName)
    {
        

        if (seedTab[0] == chestName)
        {
            validate[0] = true;
        }

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
    public void OpenChestV3(int chestName)
    {


        if (seedTab[0] == chestName)
        {
            validate[0] = true;
        }

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
    public void OpenChestV4(int chestName)
    {


        if (seedTab[0] == chestName)
        {
            validate[0] = true;
        }

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
}

