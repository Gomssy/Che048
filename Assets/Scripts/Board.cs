using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject boardPrefab;
    private Checker[,] boardState;

    int[] dx = new int[] { 0, 1, 0, -1 };
    int[] dy = new int[] { -1, 0, 1, 0 };

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Slide(Dir.Up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Slide(Dir.Down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Slide(Dir.Left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Slide(Dir.Right);
        }
    }

    private void Init()
    {
        boardState = new Checker[4, 4];

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                Vector2 pos = new Vector2(i, j);
                boardState[i, j] = Instantiate(boardPrefab, pos, Quaternion.identity, transform).GetComponent<Checker>();
                boardState[i, j].coord = new Coordinate(i, j);
                boardState[i, j].curChecker = CheckerEnum.Empty;
                boardState[i, j].curPiece = null;

                if((i+j)%2 == 0)
                {
                    boardState[i, j].GetComponent<SpriteRenderer>().color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
                }
            }
        }
        transform.position = new Vector3(-1.5f, -1.5f, 0);
    }

    public void Slide(Dir dir)
    {
        int _dx = dx[(int)dir];
        int _dy = dy[(int)dir];

        for (int i = _dx < 0 ? 0 : 3; _dx < 0 ? i < 4 : i > -1; i -= _dx != 0 ? _dx : 1)
        {
            for (int j = _dy < 0 ? 0 : 3; _dy < 0 ? j < 4 : j > -1; j -= _dy != 0 ? _dy : 1)
            {
                if (i + _dx < 0 || i + _dx > 3 || j + _dy < 0 || j + _dy > 3) continue;

                if (boardState[i, j].curChecker == CheckerEnum.Empty) continue;

                int temp = 1;
                while (i + _dx * temp > -1 && i + _dx * temp < 4 && j + _dy * temp > -1 && j + _dy * temp < 4 && boardState[i + _dx * temp, j + _dy * temp].curPiece == null)
                {
                    temp++;
                }
                boardState[i, j].MovePiece(boardState[i + _dx * (temp - 1), j + _dy * (temp - 1)]);

                int nx = i + _dx * (temp - 1);
                int ny = j + _dy * (temp - 1);

                if (nx + _dx < 0 || nx + _dx > 3 || ny + _dy < 0 || ny + _dy > 3) continue;
                if (boardState[nx + _dx, ny + _dy].curPiece.pieceType != boardState[nx, ny].curPiece.pieceType) continue;

                if (boardState[nx + _dx, ny + _dy].curChecker == boardState[nx, ny].curChecker)
                {
                    boardState[nx + _dx, ny + _dy].curPiece = JoinPiece(boardState[nx + _dx, ny + _dy].curPiece, boardState[nx, ny].curPiece);

                    boardState[nx, ny].curPiece = null;
                    boardState[nx, ny].curChecker = CheckerEnum.Empty;
                }
                else
                {
                    //shoud destroy lower one
                    boardState[nx + _dx, ny + _dy].RemovePiece();
                    boardState[nx, ny].MovePiece(boardState[nx + _dx, ny + _dy]);
                }
            }
        }
    }

    public Piece JoinPiece(Piece p1, Piece p2)
    {
        GameObject piece = GameManager.Inst.GetPiece(p1.pieceType + 1);
        Piece res = Instantiate(piece, p1.transform.position, Quaternion.identity).GetComponent<Piece>();
        res.Init(p1.checker);
        Destroy(p1.gameObject);
        Destroy(p2.gameObject);

        return res;
    }
}

public enum Dir
{
    Down,
    Right,
    Up,
    Left
}
