using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileColorizer : MonoBehaviour
{
    public float range;
    public Tilemap map;


    public bool doColorizeWater;

    private void LateUpdate()
    {
        if (doColorizeWater)
        {
            doColorizeWater = false;
            
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
