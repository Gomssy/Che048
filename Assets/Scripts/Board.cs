using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
    [SerializeField]
    private GameObject boardPrefab;
    public Checker[,] boardState;

    int[] dx = new int[] { 0, 1, 0, -1 };
    int[] dy = new int[] { -1, 0, 1, 0 };

    // Start is called before the first frame update
    void Start()
    {
        boardState = GameManager.Inst.boardState;
        Init();        
    }

    private void Update()
    {
        if (GameManager.Inst.playerActed || GameManager.Inst.isGameOver || !GameManager.Inst.TurnPhase) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Slide(Dir.Up);
            GameManager.Inst.TurnPhase = false;
            GameManager.Inst.playerActed = true;
            CanvasManager.Inst.SetTurnEndButton();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Slide(Dir.Down);
            GameManager.Inst.TurnPhase = false;
            GameManager.Inst.playerActed = true;
            CanvasManager.Inst.SetTurnEndButton();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Slide(Dir.Left);
            GameManager.Inst.TurnPhase = false;
            GameManager.Inst.playerActed = true;
            CanvasManager.Inst.SetTurnEndButton();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Slide(Dir.Right);
            GameManager.Inst.TurnPhase = false;
            GameManager.Inst.playerActed = true;
            CanvasManager.Inst.SetTurnEndButton();
        }
    }

    private void Init()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Vector2 pos = new Vector2(i, j);
                boardState[i, j] = Instantiate(boardPrefab, pos, Quaternion.identity, transform).GetComponent<Checker>();
                boardState[i, j].name = "(" + i + "," + j + ")";
                boardState[i, j].coord = new Coordinate(i, j);
                boardState[i, j].curChecker = CheckerEnum.Empty;
                boardState[i, j].curPiece = null;

                if((i+j)%2 == 0)
                {
                    boardState[i, j].GetComponent<SpriteRenderer>().color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
                }
            }
        }
        transform.position = new Vector3(-3.5f, -3.5f, 0);
    }

    public void Slide(Dir dir)
    {
        int _dx = dx[(int)dir];
        int _dy = dy[(int)dir];

        for (int i = _dx < 0 ? 0 : 7; _dx < 0 ? i < 8 : i > -1; i -= _dx != 0 ? _dx : 1)
        {
            for (int j = _dy < 0 ? 0 : 7; _dy < 0 ? j < 8 : j > -1; j -= _dy != 0 ? _dy : 1)
            {
                if (i + _dx < 0 || i + _dx > 7 || j + _dy < 0 || j + _dy > 7) continue;

                if (boardState[i, j].curChecker == CheckerEnum.Empty) continue;

                int temp = 1;
                while (i + _dx * temp > -1 && i + _dx * temp < 8 && j + _dy * temp > -1 && j + _dy * temp < 8 && boardState[i + _dx * temp, j + _dy * temp].curPiece == null)
                {
                    temp++;
                }
                boardState[i, j].MovePiece(boardState[i + _dx * (temp - 1), j + _dy * (temp - 1)]);

                int nx = i + _dx * (temp - 1);
                int ny = j + _dy * (temp - 1);

                if (nx + _dx < 0 || nx + _dx > 7 || ny + _dy < 0 || ny + _dy > 7) continue;
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
        Piece res = null;
        if (piece != null)
        {
            res = Instantiate(piece, p1.transform.position, Quaternion.identity, GameManager.Inst.pieces).GetComponent<Piece>();
            res.Init(p1.checker);
            res.coord = p1.coord;
        }
        Destroy(p1.gameObject);
        Destroy(p2.gameObject);

        return res;
    }

    public void HighlightMovable(List<Coordinate> coordList)
    {
        if (coordList == null) return;
        foreach(var coord in coordList)
        {
            boardState[coord.X, coord.Y].Highlight(Color.yellow);
        }
    }

    public void ResetHighlight()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                boardState[i, j].Highlight((i + j) % 2 == 0 ? new Color(200 / 255f, 200 / 255f, 200 / 255f) : new Color(60 / 255f, 60 / 255f, 60 / 255f));
            }
        }
    }
}


