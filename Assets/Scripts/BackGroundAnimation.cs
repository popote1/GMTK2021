using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackGroundAnimation : MonoBehaviour
{
    public List<Transform> Plans;

    public float Duration=20;
    public float Strength = 0.02f;
    public int vibrator = 1;
    void Start()
    {
        DoShake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoShake() {
        foreach (var plan in Plans) {
            plan.DOShakePosition(Duration, Vector3.one * Strength, vibrator,90 , false , false);
        }
        Invoke("DoShake",Duration);
    }
}
