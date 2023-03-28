using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private List<GameObject> piecePrefab = new();
    private Dictionary<PieceEnum, GameObject> pieceDict = new();
    public CheckerEnum checker = CheckerEnum.White;

    public Checker[,] boardState;
    public bool isHighlighted = false;
    public Piece selected = null;

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
    }

    public bool PieceExist(Coordinate coord)
    {
        return boardState[coord.X, coord.Y].curChecker != CheckerEnum.Empty;
    }

    public bool OppoPieceExist(Coordinate coord, CheckerEnum checker)
    {
        return boardState[coord.X, coord.Y].curChecker != checker && boardState[coord.X, coord.Y].curChecker != CheckerEnum.Empty;
    }

}
