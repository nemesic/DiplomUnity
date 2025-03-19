using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Battle }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    GameState state;

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            //playerMovement.Update();
        }
        else if (state == GameState.Dialog)
        {
            Debug.Log("In dialog state");
        }
        else if (state == GameState.Battle)
        {
            Debug.Log("In battle state");
        }
    }

    public void EnterDialogState()
    {
        state = GameState.Dialog;
    }

    public void ExitDialogState()
    {
        state = GameState.FreeRoam;
    }
}

