using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    void Start()
    {
        float multi = Random.Range(0.7f, 1.3f);
        transform.localScale = new Vector3(multi, multi, 1);
    }
    
}
