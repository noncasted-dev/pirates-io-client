using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WawesLookRotrator : MonoBehaviour
{
    private Vector3 lastPos;
    private Vector3 currentPos;
    void Update()
    {
        currentPos = transform.position;
        transform.right = (currentPos - lastPos);
        lastPos = Vector3.Lerp(lastPos, currentPos, 15 * Time.deltaTime);
    }
}
