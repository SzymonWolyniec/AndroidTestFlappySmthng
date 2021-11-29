using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObstaclesOptionsClass
{
    public float obstaclePartsRotationY;
    public float obstaclePartsBoxColliderSizeY;

    public ObstaclesOptionsClass(float obstaclePartsRotationY, float obstaclePartsBoxColliderSizeY)
    {
        this.obstaclePartsRotationY = obstaclePartsRotationY;
        this.obstaclePartsBoxColliderSizeY = obstaclePartsBoxColliderSizeY;
    }
}
