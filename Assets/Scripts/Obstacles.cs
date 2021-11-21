using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject obstacle;

    float obstacleMaxMinY = 3;
    float obstacleTopBottomMaxRotation = 90;
    float obstacleMinMaxRotation = 10;
    float obstacleSpawnTime = 2;
    float obstacleTimeToDestroy = 8;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            StartCoroutine(SpawnAndMoveSingleObstacle());
            yield return new WaitForSeconds(obstacleSpawnTime);
        }
    }

    IEnumerator SpawnAndMoveSingleObstacle()
    {
        yield return new WaitForFixedUpdate();

        var obstacleStartPosition = new Vector3(12, Random.Range(-obstacleMaxMinY, obstacleMaxMinY), 0);
        var obstacleObj = Instantiate(obstacle, obstacleStartPosition, Quaternion.Euler(0, 0, Random.Range(-obstacleMinMaxRotation, obstacleMinMaxRotation)));
        var obstacleScript = obstacleObj.GetComponent<ObstacleClass>();

        var randRotation = Random.Range(0, obstacleTopBottomMaxRotation);
        Debug.Log(randRotation);
        obstacleScript.obstacleTop.transform.localEulerAngles = new Vector3(0, randRotation, -90);
        obstacleScript.obstacleBottom.transform.localEulerAngles = new Vector3(0, randRotation, -90);

        var obstacleRB = obstacleObj.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = new Vector2(-3, obstacleRB.velocity.y);

        StartCoroutine(DestroyObstacle(obstacleObj));
    }

    IEnumerator DestroyObstacle(GameObject obstacleObj)
    {
        yield return new WaitForSeconds(obstacleTimeToDestroy);
        Destroy(obstacleObj);
    }
}
