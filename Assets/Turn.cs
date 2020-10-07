using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public List<Transform> bezierPos;

    public float GetDistanceTurn()
    {
        return Vector3.Distance(bezierPos[1].position, bezierPos[2].position);
    }
    public float GetDistance1()
    {
        float dis = 0;
        for (int i = 0; i < bezierPos.Count-1; i++)
        {
            dis += Vector3.Distance(bezierPos[i].position, bezierPos[i + 1].position);
        }
        return dis;
    }
}
