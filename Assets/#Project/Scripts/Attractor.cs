using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    Guard[] guards;
    LayerMask guardMask;
    [SerializeField] float baitDistance = 20;
    // Start is called before the first frame update
    void Start()
    {
        guards = GameObject.FindObjectsOfType<Guard>();
        guardMask = LayerMask.GetMask("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Guard guard in guards) {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,guard.transform.position - transform.position,out hit, baitDistance, guardMask)) {
                if(hit.collider.transform == guard.transform) {
                    Debug.Log($"hit {guard}");
                    guard.BaitedBy = gameObject;
                }
            }
        }
    }
}
