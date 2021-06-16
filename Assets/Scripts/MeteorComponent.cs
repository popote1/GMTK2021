
using DG.Tweening;
using UnityEngine;



public class MeteorComponent : MonoBehaviour
{
    public float Speed;
    public float DistanceToContact = 1;
    public Vector3 target;
    public LineRenderer lineRenderer;
    public Transform GraphiquePiece;
    
    void Update()
    {
        Vector3 dir = target - transform.position;
        transform.position += dir.normalized * Speed * Time.deltaTime;
        if (Vector3.Distance(target, transform.position) <= DistanceToContact)
        {
            Destroy(gameObject);
        }
        GraphiquePiece.forward = dir;
    }
}
 