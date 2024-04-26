using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] string nextLevel = "End";

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (other.GetComponent<PlayerObjectives>().ObjectiveComplete) {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
