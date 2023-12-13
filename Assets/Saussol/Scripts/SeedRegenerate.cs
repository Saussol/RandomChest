using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedRegenerate : MonoBehaviour
{
    public int maSeed; // Remplace ceci par ta seed sp�cifique

    void Start()
    {
        RegenerateSequence(maSeed);
    }

    void RegenerateSequence(int seed)
    {
        // Utilise la seed sp�cifique pour r�g�n�rer la s�quence al�atoire
        Random.InitState(seed);

        // G�n�re des nombres al�atoires avec la m�me seed
        for (int i = 0; i < 10; i++)
        {
            float randomValue = Random.value;
            //Debug.Log("Valeur al�atoire g�n�r�e : " + randomValue);
        }
    }
}
