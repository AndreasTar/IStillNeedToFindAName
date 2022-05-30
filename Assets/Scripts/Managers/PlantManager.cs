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

        // The Biome bound have to be a rectangle for this to work well ((i think) at least for now).
        // we get the box dimensions basically, so if you fetch that and do some math depending on the center
        // of the box, you can get a random position on the ground where the box is above
        // (IDEA : make boxes be dynamic strength, meaning that a plant is more likely to spawn on a box with more strength
        // and the boxes surrounding it will increase in strength as more plants grow, kinda like how forests happen.
        // A tree will grow near already existing trees, due to the seed falling there, and the forest moves out slowly)
        string biome = "BiomeBound";
        Vector3 biomebound = GameObject.FindGameObjectWithTag(biome).transform.localScale;
        Debug.Log("Dimensions : x " + biomebound.x + ", y " + biomebound.y + ", z " + biomebound.z);
    }
}
