using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    [SerializeField] private Square squarePrefab;

    // in terms of grid coordinates
    private Vector2Int playerStartGPos = new(0, 0);

    private Vector2 squareDim;
    private Vector2 squareMidPoint;

    // top left
    private Vector2 tOrigin;

    private string map =
        "########" + "\n" +
        "####000#" + "\n" +
        "###000##" + "\n" +
        "##0000##";

    [SerializeField] private List<List<Square>> sqList;

    public void SetupMap()
    {
        sqList = new List<List<Square>>();

        SetDimensionVars();
        SetMapLayout(map);
    }

    // GridPosToTransformPos
    public Vector2 GPosToTPos(Vector2Int gPosIn)
    {
        // this
        // return squareList[gridPosIn.y][gridPosIn.x].transform.position;

        // or this
        gPosIn.y = -gPosIn.y;
        return (tOrigin + squareMidPoint) + (gPosIn * squareDim);
    }

    public Vector2Int GetPlayerStartGPos()
    {
        return playerStartGPos;
    }

    public Square GetSqByGPos(Vector2Int gPosIn)
    {
        if(!IsGPosInBound(gPosIn))
        {
            Debug.LogError($"gPosIn {gPosIn} out of bounds.");
            return null;
        }
        int i = gPosIn.x;
        int j = gPosIn.y;

        return sqList[j][i];
    }

    public bool IsGPosActiveSq(Vector2Int gPosIn)
    {
        int i = gPosIn.x;
        int j = gPosIn.y;

        if (!IsGPosInBound(gPosIn))
            return false;

        Square sq = sqList[j][i];

        return sq != null && sq.IsVisitable();
    }

    private bool IsGPosInBound(Vector2Int gPosIn)
    {
        int i = gPosIn.x;
        int j = gPosIn.y;

        if (sqList == null)
            return false;

        if (j >= 0 && j < sqList.Count)
            return i >= 0 && i < sqList[j].Count;

        return false;
    }

    private void SetDimensionVars()
    {
        squareDim = squarePrefab.transform.localScale;
        squareMidPoint = new(squareDim.x/2, squareDim.y/2);
        tOrigin = Vector2.zero;
    }

    private void SetMapLayout(string mapLayoutIn)
    {
        Vector2 currentTPos = tOrigin;

        int i = 0;
        int j = 0;

        foreach (char c in mapLayoutIn)
        {
            // create a new list at the beginning of each row
            if(i == 0)
                sqList.Add(new List<Square>());

            if (c == '#')
            {
                // add 0.5 to the gobj vec2 when creating
                Square sq = Instantiate(squarePrefab, transform);
                sq.transform.position = currentTPos + squareMidPoint;
                sq.name = $"Sq ({i}, {j})";

                sqList[j].Add(sq);
            }
            // add empty square, or null???
            else if(c == '0')
            {
                sqList[j].Add(null);
            }

            // change position
            if (c == '\n')
            {
                currentTPos.x = tOrigin.x;
                currentTPos.y -= squareDim.y;

                // new row
                i = 0;
                j++;
            }
            else
            {
                currentTPos.x += squareDim.x;

                i++;
            }
        }
    }
}
