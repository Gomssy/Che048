using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public PieceEnum pieceType;
    public CheckerEnum checker;
    [SerializeField]
    Sprite[] spriteList = new Sprite[2];

    public void Init(CheckerEnum _checker)
    {
        checker = _checker;
        GetComponent<SpriteRenderer>().sprite = spriteList[(int)checker];
    }

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