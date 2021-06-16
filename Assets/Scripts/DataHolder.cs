using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public List<LevelPanel> LevelPanels = new List<LevelPanel>();
    public int LevelIndex =-1;
    void Awake() {
        if (GameObject.Find(transform.name) != null) {
            if (GameObject.Find(transform.name)!=gameObject)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
