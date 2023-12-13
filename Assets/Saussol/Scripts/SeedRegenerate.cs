using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedRegenerate : MonoBehaviour
{
    public int maSeed; // Remplace ceci par ta seed spécifique

    void Start()
    {
        RegenerateSequence(maSeed);
    }

    void RegenerateSequence(int seed)
    {
        // Utilise la seed spécifique pour régénérer la séquence aléatoire
        Random.InitState(seed);

        // Génère des nombres aléatoires avec la même seed
        for (int i = 0; i < 10; i++)
        {
            float randomValue = Random.value;
            //Debug.Log("Valeur aléatoire générée : " + randomValue);
        }
    }
}
