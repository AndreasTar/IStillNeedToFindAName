using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager: MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Debug.Log("pressed");
            SpawnPlant();
        }

    }

    void SpawnPlant()
    {
        PolygonCollider2D groundColl = GameObject.FindGameObjectWithTag("SpawnableGround").GetComponent<PolygonCollider2D>();
        Vector3 pcgroundCenter = groundColl.bounds.center;
        RaycastHit2D ray = Physics2D.Raycast(pcgroundCenter + new Vector3(Random.value*2-1, 10), Vector2.down, 10f);
        GameObject tempobject = new GameObject("temp");
        tempobject.transform.Translate(ray.point);


        string biome = "BiomeBound";
        BoxCollider2D biomebound = GameObject.FindGameObjectWithTag(biome).GetComponent<BoxCollider2D>();
        Vector3 temp = biomebound.bounds.size;
        Debug.Log("Dimensions : x " + temp.x + ", y " + temp.y + ", z " + temp.z);
    }
}
