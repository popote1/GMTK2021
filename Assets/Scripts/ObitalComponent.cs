using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObitalComponent : MonoBehaviour
{

    public List<Orbite> Orbits;
    
    [Header("Orbit render Parameters")]
    public float theta_scale = 0.01f;        //Set lower to add more points
    public int size;
    public Material OrbitMat;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Orbite orbit in Orbits)
        {
            orbit.OrbiteTransform.Rotate(Vector3.up, orbit.RotationFactor*Time.deltaTime);    
        }
    }

    
}

[Serializable]
public class Orbite
{
    public Transform OrbiteTransform;
    public float RotationFactor;
}
