using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelectorManager : MonoBehaviour
{
    public Camera Camera;

    public GameManager GameManager;

    public StarShipComponent StarShipComponent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.GetComponent<PlanetComponent>() != null) {
                    if (hit.transform.GetComponent<PlanetComponent>().IsTargetable) {
                        GameManager.AddTarget(hit.transform.GetComponent<PlanetComponent>());
                    }
                }

                //SaveTargets.Add(hit.transform);
                //Debug.Log(hit.transform.name+" est ajouter a la liste des targets");
            }
        }
/*
        if (Input.GetKeyDown("p"))
        {
            SaveTargets.Clear();
            Debug.Log("La Liste des Target a été vidé");
        }

        if (Input.GetKeyDown("o"))
        {
            StarShipComponent ship= Instantiate(StarShipComponent);
            ship.Targets = SaveTargets.ToArray();
            SaveTargets.Clear();
            ship.IndexTarget = 0;
            //Lance la navette;
            Debug.Log("LanceMent d'une Navette");
        }*/
    }
}
