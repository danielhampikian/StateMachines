using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats 
{
    private float heatlh;
    private float speed;
    private float projectileSpeed;
    private float projectileDiameter;
    private float detectionRange;
    private float height;
    private float rotationalSpeed;
    private float width;
    

    public Stats()
    {
        float []statStack = new float[7];
        int i = 0;
        foreach (float stat in statStack)
        {
            float luck = 0;
            if (Random.Range(0,2)<1)
            {
                 luck = -(stat / 10);
            }
            else
            {
                luck = stat / 10;
            }
            statStack[i] += luck;
            i++;
        }
    }
    
}





