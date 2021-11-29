using UnityEditor;
using UnityEngine;

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
