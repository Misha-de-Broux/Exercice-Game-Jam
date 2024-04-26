using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour {
    public int mcGuffinAmount;
    int mcGuffinToGet;
    void Start() {
        mcGuffinAmount = 0;
        mcGuffinToGet = GameObject.FindGameObjectsWithTag("McGuffin").Length;
    }

    public bool ObjectiveComplete { get { return mcGuffinAmount == mcGuffinToGet; } }
}
