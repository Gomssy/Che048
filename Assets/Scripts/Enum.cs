using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum : MonoBehaviour
{

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
public enum CheckerEnum
{
    White,
    Black,
    Empty
}

public enum Dir
{
    Down,
    Right,
    Up,
    Left
}