using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(Light))]
public class Guard : MonoBehaviour {

    

    public GuardStateMachine StateMachine {  get; private set; }
    public Transform[] waypoints;
    [SerializeField] public Transform target;
    public float timeToLoseTarget = 5f;
    [SerializeField] float distanceView = 20f;
    [SerializeField] float angleVision = 90f;
    [SerializeField] float lightIntensity = 50f ;
    public Light Torch {  get; private set; }
    public NavMeshAgent Agent { get; private set; }
    // Start is called before the first frame update
    void Start() {
        Agent = GetComponent<NavMeshAgent>();
        Torch = GetComponent<Light>();
        Torch.type = LightType.Spot;
        Torch.color = Color.red;
        Torch.intensity = lightIntensity;
        Torch.range = distanceView;
        Torch.spotAngle = angleVision;
        Torch.innerSpotAngle = angleVision;
        Torch.enabled = false;
        StateMachine = new GuardStateMachine(this);
        StateMachine.Init(StateMachine.patrolState);
    }

    // Update is called once per frame
    void Update() {
        StateMachine.Perform();
    }
    public bool SeeTarget {
        get {
            Vector3 direction = (target.position - transform.position).normalized;
            RaycastHit hit;
            if (Vector3.Angle(transform.forward, direction) < angleVision / 2 && Physics.Raycast(transform.position, direction, out hit, distanceView)) {
                if (hit.collider.transform == target) {
                    return true;
                }
            }
            return false;
        }
    }
}