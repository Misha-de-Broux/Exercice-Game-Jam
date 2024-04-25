using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Light))]
public class Illuminate : MonoBehaviour
{
    Light light;

    private void Start() {
        light = GetComponent<Light>();
    }
    public bool CastLight(Transform target) {
        if (light.enabled) {
            Vector3 direction = (target.position - transform.position).normalized;
            RaycastHit hit;
            if (Vector3.Angle(transform.forward, direction) < light.spotAngle / 2 && Physics.Raycast(transform.position, direction, out hit, light.range)) {
                if (hit.collider.transform == target) {
                    return true;
                }
            }
            return false;
        }
        return false ;
    }
}
