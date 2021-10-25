using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomizeGain : MonoBehaviour
{   
    [HideInInspector]
    public List<Tuple<float,float>> Gain = new List<Tuple<float,float>>();

    // Tuple<int, float[,]>[] Gain;

    float[] first = {/*0.000001f, -1, 1, -3, 3, -5, 5, 7, -7, 9, */-9};

    float[] second = {0.1f, 0.15f, 0.2f};
    
    [HideInInspector]
    public int trialcnt;
    

    void Start()
    {
        trialcnt = 1;
        // for (int i = 0; i < 8; i++)
        // {
        for (int j = 0; j < first.Length; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Gain.Add(new Tuple<float, float>(first[j], second[k]));
                }
            }
        // }
        GainShuffle();
        // Debug.Log(Gain.Count);
        for (int i = 0; i < Gain.Count; i++)
        {
            // Debug.Log(Gain[i]);
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {

    }

    public void GainShuffle() {
        for (int i = 0; i < Gain.Count; i++)
        {
            Tuple<float, float> tmp = Gain[i];
            int randomIndex = UnityEngine.Random.Range(i, Gain.Count);
            Gain[i] = Gain[randomIndex];
            Gain[randomIndex] = tmp;
        }
    }
}
