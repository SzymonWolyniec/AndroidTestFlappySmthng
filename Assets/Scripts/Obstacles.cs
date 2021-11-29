using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    private SingleObstacle singleObstacle;

    [SerializeField]
    private Transform obstaclesContainer;

    [SerializeField]
    private List<ObstaclesOptionsClass> ObstaclesOptions = new List<ObstaclesOptionsClass>();

    private float obstacleMaxMinY = 3;
    private float obstacleMinMaxRotation = 15;
    private float obstacleSpawnTime = 2;
    private float obstacleTimeToDestroy = 8;

    [HideInInspector]
    public int gameNumber = 0;

    private void OnEnable()
    {
        EventManager.OnStartClicked += OnStart;
        EventManager.OnDied += OnDied;
        EventManager.OnResetClicked += OnReset;
    }

    private void OnDisable()
    {
        EventManager.OnStartClicked -= OnStart;
        EventManager.OnDied -= OnDied;
        EventManager.OnResetClicked -= OnReset;
    }

    private void OnStart()
    {
        StartCoroutine(SpawnObstacles());
    }

    private void OnDied()
    {
        gameNumber++;
    }
    private void OnReset()
    {
        foreach (Transform singleObstacle in obstaclesContainer)
        {
            singleObstacle.gameObject.SetActive(false);
        }

        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        int currentGameNumber = gameNumber;

        while (currentGameNumber == gameNumber)
        {
            StartCoroutine(SpawnAndMoveSingleObstacle());
            yield return new WaitForSeconds(obstacleSpawnTime);
        }
    }

    private IEnumerator SpawnAndMoveSingleObstacle()
    {
        yield return new WaitForFixedUpdate();

        var obstacleStartPosition = new Vector3(12, Random.Range(-obstacleMaxMinY, obstacleMaxMinY), 0);
        int drawnObstacleOption = Random.Range(0, ObstaclesOptions.Count - 1);

        Vector3 drawnOptionEulerAngles = new Vector3(0, ObstaclesOptions[drawnObstacleOption].obstaclePartsRotationY, -90);
        Vector2 drawnOptionColliderSize = new Vector2(10, ObstaclesOptions[drawnObstacleOption].obstaclePartsBoxColliderSizeY);

        singleObstacle.obstacleTop.localEulerAngles = drawnOptionEulerAngles;
        singleObstacle.obstacleBottom.localEulerAngles = drawnOptionEulerAngles;

        singleObstacle.obstacleTopCollider.size = drawnOptionColliderSize;
        singleObstacle.obstacleBottomCollider.size = drawnOptionColliderSize;

        var obstacleObj = Instantiate(singleObstacle.gameObject, obstacleStartPosition, Quaternion.Euler(0, 0, Random.Range(-obstacleMinMaxRotation, obstacleMinMaxRotation)), obstaclesContainer);

        var obstacleRB = obstacleObj.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = new Vector2(-3, obstacleRB.velocity.y);

        StartCoroutine(DestroyObstacle(obstacleObj));
    }

    private IEnumerator DestroyObstacle(GameObject obstacleObj)
    {
        yield return new WaitForSeconds(obstacleTimeToDestroy);
        Destroy(obstacleObj);
    }

    internal void GetObstaclesOptions()
    {
        ObstaclesOptions.Clear();

        List<GameObject> Obstacles = Resources.LoadAll<GameObject>("ObstaclesFramesPrefabs").ToList();

        foreach (var singleObstacle in Obstacles)
        {
            var singleObstacleChild = singleObstacle.transform.GetChild(0).gameObject;
            ObstaclesOptions.Add(new ObstaclesOptionsClass(singleObstacleChild.transform.eulerAngles.y, singleObstacleChild.GetComponent<BoxCollider2D>().size.y));
        }
    }
}
