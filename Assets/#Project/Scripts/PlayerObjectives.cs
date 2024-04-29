using TMPro;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour {

    [SerializeField] Material readyToGo;
    private int mcGuffinAmount;
    int mcGuffinToGet;
    [SerializeField] TextMeshProUGUI display;
    void Start() {
        mcGuffinAmount = 0;
        mcGuffinToGet = GameObject.FindGameObjectsWithTag("McGuffin").Length;
        display.text = ($"{mcGuffinAmount} / {mcGuffinToGet}");
    }

    public bool ObjectiveComplete { get { return mcGuffinAmount == mcGuffinToGet; } }

    public int AddMcGuffin() {
        mcGuffinAmount++;
        if (ObjectiveComplete) {
            this.GetComponent<Renderer>().material = readyToGo;
        }
        display.text = ($"{mcGuffinAmount} / {mcGuffinToGet}");
        return mcGuffinAmount - 1;
    }
}
