using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Liste de nombres al�atoires pour les portes
    //int[] listeNombres = new int[] { 2, 8, 4, 6, 1, 9, 3, 0, 7, 5, 11, 10 };

    public List<string> root = new List<string>();
    public int[] seedUse;
    public GameObject[] door;

    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI seedNum;

    public ChestSo chestSo;

    public List<bool> isGood;
    public int i = 0;

    public TMP_InputField inputField;

    void Start()
    {
        CreateSeed();

        int randomSeed = UnityEngine.Random.Range(0, chestSo.seedList.Count);
        seedNum.text = "Seed : " + randomSeed;

        ReadSeed(chestSo.seedList[randomSeed]);

        // G�n�ration de la matrice d'adjacence
        int[,] matriceAdjacence = GenererMatriceAdjacence(seedUse/*listeNombres*/ );

        // Utilisation de la matrice pour ouvrir les portes
        OuvrirPortesEnUtilisantMatrice(matriceAdjacence, seedUse /*listeNombres*/ );
    }

    private void FixedUpdate()
    {
        //CreateSeed();
    }

    public void Regenerate()
    {
        int randomSeed = UnityEngine.Random.Range(0, chestSo.seedList.Count);
        seedNum.text = "Seed : " + randomSeed;

        reloadSeed(randomSeed);
    }

    void reloadSeed(int seed)// 1: set bool tab false  2:  set all chest  3: change seed
    {
        for (int i = 0; i < isGood.Count; i++)// 1 
        {
            isGood[i] = false;
            seedUse[i] = 0;
            door[i].SetActive(true);
        }

        i = 0;

        root.Clear();

        ReadSeed(chestSo.seedList[seed]);

        int[,] matriceAdjacence = GenererMatriceAdjacence(seedUse/*listeNombres*/ );

        // Utilisation de la matrice pour ouvrir les portes
        OuvrirPortesEnUtilisantMatrice(matriceAdjacence, seedUse /*listeNombres*/ );

        seedNum.text = "Seed : " + seed;
    }

    public void SeletedSeed() 
    {
        Debug.Log(inputField.text);

        if (int.TryParse(inputField.text, out int nombreEntier))
        {
            //Debug.Log("Nombre entier : " + nombreEntier);
            if (nombreEntier <= chestSo.seedList.Count)
            {
                reloadSeed(nombreEntier);
            }
        }
        else
        {
            // Si la conversion �choue 
            //Debug.LogWarning("La conversion en entier a �chou� !");
        }
    }

    #region GenerateSeed

    void ReadSeed(string list)
    {
        string[] numbersAsString = list.Split(',');
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
            seedUse[i] = seedNumbers[i];// change all tab
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
            //Debug.Log("Valeur al�atoire g�n�r�e : " + seedArray[i]);
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

    #endregion

    // G�n�re la matrice d'adjacence en fonction de la liste de nombres
    int[,] GenererMatriceAdjacence(int[] listeNombres)
    {
        int tailleMatrice = listeNombres.Length;
        int[,] matriceAdjacence = new int[tailleMatrice, tailleMatrice];

        for (int i = 0; i < tailleMatrice; i++)
        {
            for (int j = 0; j < tailleMatrice; j++)
            {
                if (i != j)
                {
                    matriceAdjacence[i, j] = EstConnecte(listeNombres[i], listeNombres[j]) ? 1 : 0;
                }
            }
        }

        return matriceAdjacence;
    }

    // D�termine si deux nombres sont connect�s (adapter selon votre logique)
    bool EstConnecte(int a, int b)
    {
        // Exemple : connectez les portes si leur diff�rence est inf�rieure � 3
        return Math.Abs(a - b) < 3;
    }

    // Ouvre les portes en utilisant la matrice d'adjacence
    void OuvrirPortesEnUtilisantMatrice(int[,] matriceAdjacence, int[] listeNombres)
    {
        // Correspondance entre nombres et lettres pour les portes
        Dictionary<int, char> correspondance = new Dictionary<int, char>()
        {
            { 0, 'a' }, { 1, 'b' }, { 2, 'c' }, { 3, 'd' },
            { 4, 'e' }, { 5, 'f' }, { 6, 'g' }, { 7, 'h' },
            { 8, 'i' }, { 9, 'j' }, { 10, 'k' }, { 11, 'l' }
        };

        List<char> chemin = new List<char>();
        HashSet<char> portesVisitees = new HashSet<char>();

        char porteDeDepart = correspondance[listeNombres[0]];
        DFS(porteDeDepart, chemin, portesVisitees, matriceAdjacence, correspondance);

        OuvrirPortesSelonChemin(chemin);
    }

    // Algorithme DFS pour parcourir les portes et g�n�rer le chemin
    void DFS(char porteActuelle, List<char> chemin, HashSet<char> portesVisitees, int[,] matriceAdjacence, Dictionary<int, char> correspondance)
    {
        chemin.Add(porteActuelle);
        portesVisitees.Add(porteActuelle);

        for (int i = 0; i < correspondance.Count; i++)
        {
            char porteSuivante = correspondance[i];
            if (matriceAdjacence[porteActuelle - 'a', porteSuivante - 'a'] == 1 && !portesVisitees.Contains(porteSuivante))
            {
                DFS(porteSuivante, chemin, portesVisitees, matriceAdjacence, correspondance);
            }
        }
    }

    // M�thode pour ouvrir les portes selon le chemin g�n�r�
    void OuvrirPortesSelonChemin(List<char> chemin)
    {        
        foreach (char porte in chemin)
        {
            //Debug.Log(porte);
            root.Add(porte.ToString());
        }

        string resultat = string.Join(" ", root);

        textMesh.text = resultat;
    }

    // M�thode fictive pour ouvrir une porte (adapter selon votre logique)
    public void OuvrirPorte(string porte)
    {
        if (porte == root[0 + i])
        {
            for (int i = 0; i < door.Length; i++)
            {
                if (door[i].GetComponent<Pointer>().chestName == porte)
                {
                    door[i].SetActive(false);
                }
            }

            isGood[0 + i] = true;

            i++;
            
            Debug.Log("It's a good door");
        }
        else
        {
            Debug.Log("Not good door");
        }
    }
}
