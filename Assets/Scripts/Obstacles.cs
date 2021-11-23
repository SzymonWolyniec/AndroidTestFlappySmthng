using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public List<GameObject> obstaclePrefabsList;
    public Transform obstaclesContainer;

    private float obstacleMaxMinY = 3;
    private float obstacleMinMaxRotation = 15;
    private float obstacleSpawnTime = 2;
    private float obstacleTimeToDestroy = 8;

    [HideInInspector]
    public int gameNumber = 1;

    public IEnumerator SpawnObstacles()
    {
        int currentGameNumber = gameNumber;

        while (currentGameNumber == gameNumber)
        {
            StartCoroutine(SpawnAndMoveSingleObstacle());
            yield return new WaitForSeconds(obstacleSpawnTime);
        }
    }

    IEnumerator SpawnAndMoveSingleObstacle()
    {
        yield return new WaitForFixedUpdate();

        var obstacleStartPosition = new Vector3(12, Random.Range(-obstacleMaxMinY, obstacleMaxMinY), 0);
        var obstacleObj = Instantiate(obstaclePrefabsList[Random.Range(0, obstaclePrefabsList.Count)], obstacleStartPosition, Quaternion.Euler(0, 0, Random.Range(-obstacleMinMaxRotation, obstacleMinMaxRotation)), obstaclesContainer);

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
