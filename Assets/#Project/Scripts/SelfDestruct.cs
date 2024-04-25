using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeToLive = 1;
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }
}
