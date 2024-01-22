using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private MapLogic map;

    [SerializeField] private InputManager inputManager;

    private Player player = null;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();

        InitGame();
    }

    void Update()
    {
        if (!inputManager.IsActive())
            return;

        // handle player input here
        if(Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            InitGame();
        }
    }

    private void InitGame()
    {
        map.SetupMap();
        SetPlayer();

        // enable input
         inputManager.SetActive(true);
    }

    private void SetPlayer()
    {
        if(player != null)
        {
            Destroy(player.gameObject);
        }

        GameObject pObj = Instantiate(playerPrefab);
        player = pObj.GetComponent<Player>();

        Vector2Int dstGPos = map.GetPlayerStartGPos();
        VisitGPos(dstGPos);
    }

    private void MovePlayer(int xMovIn, int yMovIn)
    {
        Vector2Int srcGPos = player.getGPos();
        Vector2Int dstGPos = GetPlayerDstGPos(xMovIn, yMovIn);

        // check if in bounds
        if (!map.IsGPosActiveSq(dstGPos))
            return;

        VisitGPos(dstGPos, srcGPos);

        OnMoveEnd();
    }

    private Vector2Int GetPlayerDstGPos(int xMovIn, int yMovIn)
    {
        return player.GetDstGPos(xMovIn, yMovIn);
    }

    private void OnMoveEnd()
    {
        // check if any possible move
        if(CountActiveMoves() < 1)
        {
            Debug.Log("No active move left.");
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        Debug.Log("Game over.");

        // disable input
         inputManager.SetActive(false);

        // play animation
        player.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(2f);

        InitGame();
    }

    private int CountActiveMoves()
    {
        int ans = 0;

        List<Vector2Int> gPosList = new List<Vector2Int>
        {
            GetPlayerDstGPos(0, 1),
            GetPlayerDstGPos(0, -1),
            GetPlayerDstGPos(1, 0),
            GetPlayerDstGPos(-1, 0)
        };

        foreach(var gPos in gPosList)
        {
            ans += map.IsGPosActiveSq(gPos) ? 1 : 0;
        }

        return ans;
    }

    private void VisitGPos(Vector2Int dstGPosIn)
    {
        // move player
        player.SetGPos(dstGPosIn);
        Vector2 tPos = map.GPosToTPos(dstGPosIn);
        player.SetTPos(tPos);

        // get the square
        Square sq = map.GetSqByGPos(dstGPosIn);
        sq.OnVisit();
    }

    private void VisitGPos(Vector2Int dstGPosIn, Vector2Int srcGPosIn)
    {
        Square sq = map.GetSqByGPos(srcGPosIn);
        sq.OnExit();

        // move logic happens in here
        VisitGPos(dstGPosIn);
    }

}
