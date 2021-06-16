using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RockMoveCompoenent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakePosition(60f, Vector3.one*0.005f, 1);
    }

    
}
