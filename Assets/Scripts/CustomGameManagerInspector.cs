using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(Obstacles))]
public class CustomGameManagerInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Obstacles obstacles = (Obstacles)target;

        if (GUILayout.Button("Get possible obstacles options"))
        {
            obstacles.GetObstaclesOptions();
            PlayerPrefs.SetInt("highscore", 0);
        }
    }

}

#endif
