using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private List<GameObject> piecePrefab = new();
    private Dictionary<PieceEnum, GameObject> pieceDict = new();
    public CheckerEnum checker = CheckerEnum.White;

    public Checker[,] boardState;
    public bool isHighlighted
    {
        get => selected != null;
    }
    public Piece selected = null;

    public int white_idx = 0;
    public int black_idx = 0;

    public List<Coordinate> movableList;
    public List<PieceEnum> spawnList;

    public bool playerActed = false;

    public bool isGameOver = false;

    [SerializeField]
    public Transform pieces;

    // False when spawnPhase, true when movePhase
    private bool turnPhase = false;
    public bool TurnPhase
    {
        get => turnPhase;
        set
        {
            turnPhase = value;
        }
    }

    private void Awake()
    {
        boardState = new Checker[8, 8];
    }
    void Start()
    {
        foreach (var piece in piecePrefab)
        {
            pieceDict.Add(piece.GetComponent<Piece>().pieceType, piece);
        }
    }

    public GameObject GetPiece(PieceEnum pieceEnum)
    {
        if (pieceEnum == PieceEnum.Empty) return null;
        return pieceDict[pieceEnum];
    }

    public void ChangeTurn()
    {
        if (checker == CheckerEnum.White)
            checker = CheckerEnum.Black;
        else
            checker = CheckerEnum.White;

        playerActed = false;
        CanvasManager.Inst.SetTurnEndButton();
        CanvasManager.Inst.ShowPhase();

        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (boardState[i, j].curChecker == CheckerEnum.Empty) continue;
                else if (boardState[i,j].curChecker == checker)
                {
                    boardState[i, j].curPiece.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                    boardState[i, j].curPiece.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public bool PieceExist(Coordinate coord)
    {
        return boardState[coord.X, coord.Y].curChecker != CheckerEnum.Empty;
    }

    public bool OppoPieceExist(Coordinate coord, CheckerEnum checker)
    {
        return boardState[coord.X, coord.Y].curChecker != checker && boardState[coord.X, coord.Y].curChecker != CheckerEnum.Empty;
    }

    public void GameOver(CheckerEnum winner)
    {
        isGameOver = true;
        CanvasManager.Inst.ShowResult(winner);
    }

    public void ResetGame()
    {
        selected = null;
        movableList = null;
        white_idx = 0;
        black_idx = 0;
        playerActed = false;
        checker = CheckerEnum.White;

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                boardState[i, j].curChecker = CheckerEnum.Empty;
                boardState[i, j].curPiece = null;
            }
        }
        foreach(var piece in pieces.GetComponentsInChildren<Transform>())
        {
            if (piece == pieces) continue;
            Destroy(piece.gameObject);
        }
        CanvasManager.Inst.ResetUI();
        isGameOver = false;
    }
}
