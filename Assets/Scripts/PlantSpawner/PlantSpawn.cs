using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawn : MonoBehaviour
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
        RaycastHit2D ray = Physics2D.Raycast(pcgroundCenter + (new Vector3(0, 10)), Vector2.down, 10f);
        GameObject temp = new GameObject("temp");
        temp.transform.Translate(ray.point);
    }
}
