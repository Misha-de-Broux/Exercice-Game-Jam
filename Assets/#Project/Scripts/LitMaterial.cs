using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitMaterial : MonoBehaviour
{
    [SerializeField] Material dark, lit;
    [SerializeField] float distanceToBright = 3;
    Func<bool> isLit;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isLit = () => (player.GetComponent<Illuminate>().CastLight(transform));
    }

    // Update is called once per frame
    void Update()
    {
        if(IsBright()) {
            this.GetComponent<Renderer>().material = lit;
        } else {
            this.GetComponent<Renderer>().material = dark;
        }
    }

    bool IsBright() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, distanceToBright)) {
            if(hit.collider.transform == player.transform) {
                return true;
            }
        }
        return isLit();
    }
}
