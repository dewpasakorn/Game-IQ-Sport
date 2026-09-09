using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    [SerializeField] private GameObject targetWall;
    [SerializeField] private List<GameObject> walls;

    private void OnTriggerEnter(Collider other)
    {
        int randomNum = Random.Range(0, walls.Count);
        Instantiate(walls[randomNum],targetWall.transform.position,Quaternion.identity);
    }


}
