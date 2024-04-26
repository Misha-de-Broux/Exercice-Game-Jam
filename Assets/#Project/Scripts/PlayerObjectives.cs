using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour {

    [SerializeField] Material readyToGo;
    private int mcGuffinAmount;
    int mcGuffinToGet;
    void Start() {
        mcGuffinAmount = 0;
        mcGuffinToGet = GameObject.FindGameObjectsWithTag("McGuffin").Length;
    }

    public bool ObjectiveComplete { get { return mcGuffinAmount == mcGuffinToGet; } }

    public int AddMcGuffin() {
        mcGuffinAmount++;
        if (ObjectiveComplete) {
            this.GetComponent<Renderer>().material = readyToGo;
        }
        return mcGuffinAmount - 1;
    }
}
