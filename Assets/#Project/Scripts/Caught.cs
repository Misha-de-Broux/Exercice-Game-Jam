using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caught : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ennemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
