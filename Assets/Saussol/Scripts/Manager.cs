using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Liste de nombres aléatoires pour les portes
    int[] listeNombres = new int[] { 2, 8, 4, 6, 1, 9, 3, 0, 7, 5, 11, 10 };

    void Start()
    {
        // Génération de la matrice d'adjacence
        int[,] matriceAdjacence = GenererMatriceAdjacence(listeNombres);

        // Utilisation de la matrice pour ouvrir les portes
        OuvrirPortesEnUtilisantMatrice(matriceAdjacence, listeNombres);
    }

    // Génère la matrice d'adjacence en fonction de la liste de nombres
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

    // Détermine si deux nombres sont connectés (adapter selon votre logique)
    bool EstConnecte(int a, int b)
    {
        // Exemple : connectez les portes si leur différence est inférieure à 3
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

    // Algorithme DFS pour parcourir les portes et générer le chemin
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

    // Méthode pour ouvrir les portes selon le chemin généré
    void OuvrirPortesSelonChemin(List<char> chemin)
    {
        foreach (char porte in chemin)
        {
            OuvrirPorte(porte);
        }
        
    }

    // Méthode fictive pour ouvrir une porte (adapter selon votre logique)
    void OuvrirPorte(char porte)
    {
        Debug.Log("Ouverture de la porte " + porte);
        // Votre logique pour ouvrir la porte va ici...
    }
}
