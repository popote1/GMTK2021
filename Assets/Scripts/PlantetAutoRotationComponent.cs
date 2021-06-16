using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlantetAutoRotationComponent : MonoBehaviour
{
    public float MaxSpeed=40;
    public float MinSpeed=10;
    public bool IsUsingRandomRotation = true;
    public float MaxSize = 0.8f;
    public float MinSize = 1.2f;

    public Vector3 RotationAxis = Vector3.up;
    
    public float RotationSpeed=1;
    
    
    void Start()
    {
        transform.localScale = Vector3.one*Random.Range(MinSize , MaxSize);
        RotationSpeed = Random.Range(MinSpeed, MaxSpeed);
        if (IsUsingRandomRotation) {
            transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            switch (Random.Range((int) 0, (int) 3)) {
                case 0:
                    RotationAxis = Vector3.left;
                    break;
                case 1:
                    RotationAxis = Vector3.up;
                    break;
                case 2:
                    RotationAxis = Vector3.forward;
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationAxis , RotationSpeed*Time.deltaTime);
    }
}
