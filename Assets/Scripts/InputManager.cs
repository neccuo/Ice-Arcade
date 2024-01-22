using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private bool isActive = false;

    private Dictionary<KeyCode, UnityEvent> keyDic = new Dictionary<KeyCode, UnityEvent>
    {
        { KeyCode.W, new UnityEvent()},
        { KeyCode.S, new UnityEvent()},
        { KeyCode.A, new UnityEvent()},
        { KeyCode.D, new UnityEvent()},
        { KeyCode.R, new UnityEvent()}
    };

    public void InsertEvent(KeyCode keyIn, UnityEvent eventIn)
    {
        if(keyDic.ContainsKey(keyIn))
        {
            //keyDic[keyIn];
        }
        UnityEvent e = new UnityEvent();
        // maybe add UnityAction if it contains etc (to unity event)
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetActive(bool isActiveIn)
    {
        isActive = isActiveIn;
    }

    public void TakeInput()
    {
        //if (!isActive)
        //    return;


        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    MovePlayer(0, -1);
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    MovePlayer(0, 1);
        //}
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    MovePlayer(-1, 0);
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    MovePlayer(1, 0);
        //}
        //else if (Input.GetKeyDown(KeyCode.R))
        //{
        //    InitGame();
        //}
    }
}
