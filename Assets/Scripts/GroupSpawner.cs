using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 xBounds;
    
    [SerializeField]
    private Vector2 zBounds;

    [SerializeField]
    private GameObject[] toSpawn;

    [SerializeField]
    private Vector2[] minMaxItemCount;
    

    void Start()
    {
        for(int index = 0; index < toSpawn.Length; index++)
        {
            int numToSpawn = Mathf.RoundToInt(Random.Range(minMaxItemCount[index].x, minMaxItemCount[index].y));
            while(numToSpawn > 0)
            {
                numToSpawn--;
                var xPos = Random.Range(xBounds.x, xBounds.y);
                var zPos = Random.Range(zBounds.x, zBounds.y);
                if(Physics.Raycast(new Vector3(xPos, 20f, zPos), Vector3.down * 50f, out RaycastHit hit))
                {
                    var position = new Vector3(xPos, hit.point.y + 1f, zPos);
                    Instantiate(toSpawn[index], position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Failed to find ground position for spawned item");
                }
            }
        }
    }
}
