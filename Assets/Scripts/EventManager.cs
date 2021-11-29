using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    internal delegate void GameStatus();
    internal static event GameStatus OnStartClicked;
    internal static event GameStatus OnDied;
    internal static event GameStatus OnResetClicked;


    private void OnStartBtnClicked()
    {
        if (OnStartClicked != null)
            OnStartClicked();
    }

    private void OnResetBtnClicked()
    {
        if (OnResetClicked != null)
            OnResetClicked();
    }

    internal static void OnPlayerDied()
    {
        if (OnDied != null)
            OnDied();
    }

}
