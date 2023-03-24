using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject boardPrefab;
    private Checker[,] boardState;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        boardState = new Checker[4, 4];

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                Vector2 pos = new Vector2(i, j);
                boardState[i, j] = Instantiate(boardPrefab, pos, Quaternion.identity, transform).GetComponent<Checker>();
                boardState[i, j].coord = new Coordinate(i, j);
                boardState[i, j].curPlayer = CheckerEnum.Empty;
                boardState[i, j].curPiece = null;

                if((i+j)%2 == 0)
                {
                    boardState[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        transform.position = new Vector3(-1.5f, -1.5f, 0);
    }
}
