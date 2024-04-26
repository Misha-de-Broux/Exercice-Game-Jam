using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McGuffinTrigger : MonoBehaviour
{
    Transform mcGuffin;
    [SerializeField] float timeToSteal = 3;
    float timeIn;
    void Start()
    {
        mcGuffin = transform.GetChild(0);
        timeIn = 0;
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            timeIn += Time.deltaTime;
            if(timeIn > timeToSteal) {
                if(mcGuffin != null) {
                    mcGuffin.SetParent(other.transform);
                    int mcGuffinAmount = other.GetComponent<PlayerObjectives>().mcGuffinAmount++;
                    mcGuffin.transform.localPosition = new Vector3(0.4f + mcGuffinAmount * 0.1f, mcGuffinAmount * 0.4f, -0.4f + mcGuffinAmount * 0.1f);
                    
                    mcGuffin = null;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Exit");
        if (other.CompareTag("Player")) {
            timeIn = 0;
        }
    }
}
