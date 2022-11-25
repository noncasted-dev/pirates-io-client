using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floated : MonoBehaviour
{
    public List<Transform> targets;

   

    private void OnEnable()
    {
        StartCoroutine(Floating());
    }


    IEnumerator Floating()
    { 
        List<Vector3> startPoses = new List<Vector3>();
        foreach (var target in targets)
        {
            startPoses.Add(target.position);
        }

        yield return null;


    }

}
