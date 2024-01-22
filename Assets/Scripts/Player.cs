using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2Int gPos;

    public Vector2Int getGPos()
    {
        return gPos;
    }

    public Vector2 getTPos()
    {
        return transform.position;
    }

    public void SetGPos(Vector2Int gPosIn)
    {
        gPos = gPosIn;
    }

    public void SetTPos(Vector2 tPosIn)
    {
        transform.position = tPosIn;
    }

    public Vector2Int GetDstGPos(int xMovIn, int yMovIn)
    {
        return new(gPos.x + xMovIn, gPos.y + yMovIn);
    }

}
