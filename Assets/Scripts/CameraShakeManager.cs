using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static Transform Camera;
    void Start()
    {
        Camera = transform;
    }

    
    [Header("Shake Parameteres 1")]
    public static float Duration1 = 0.1f;
    public static float strenght1 = 0.05f;
    public static int Vibrato1 = 2;
    public static bool Snap1;
    public static bool FadeOut1 =true;
    [Header("Shake Parameteres 2")]
    public static  float Duration2 = 0.2f;
    public static  float strenght2 = 0.2f;
    public static  int Vibrato2 = 10;
    public static  bool Snap2;
    public static  bool FadeOut2 = true;
    
    

    [ContextMenu("DoShake")]
    public static void DoShake(int shake)
    {
        if (shake==1) Camera.DOShakePosition(Duration1 , Vector3.one*strenght1 , Vibrato1,90f ,Snap1 , FadeOut1 );
        else if (shake==2)Camera.DOShakePosition(Duration2 , Vector3.one*strenght2 , Vibrato2,90f ,Snap2 , FadeOut2 ); 
    }
    
}
