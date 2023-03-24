using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public CheckerEnum curPlayer { get; set; }
    public Coordinate coord { get; set; }
    public Piece curPiece { get; set; }

    public void OnMouseDown()
    {
        Spawn(PieceEnum.Pawn);
    }

    private void Spawn(PieceEnum pieceEnum)
    {
        GameObject piece = GameManager.Inst.GetPiece(pieceEnum);
        curPiece = Instantiate(piece, transform.position, Quaternion.identity).GetComponent<Piece>();
    }
}

public enum CheckerEnum
{
    Black,
    White,
    Empty
}

public enum PieceEnum
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King,
    Empty
}