using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public CheckerEnum curChecker { get; set; }
    public Coordinate coord { get; set; }
    public Piece curPiece { get; set; }

    public void OnMouseDown()
    {
        if(curChecker == CheckerEnum.Empty)
            Spawn(PieceEnum.Pawn);
    }

    public void MovePiece(Checker _checker)
    {
        if(this ==  _checker) return;

        curPiece.transform.position = _checker.transform.position;
        _checker.curChecker = curChecker;
        _checker.curPiece = curPiece;

        curChecker = CheckerEnum.Empty;
        curPiece = null;
    }

    public void RemovePiece()
    {
        Destroy(curPiece.gameObject);
        curChecker = CheckerEnum.Empty;
        curPiece= null;
    }

    private void Spawn(PieceEnum pieceEnum)
    {
        GameObject piece = GameManager.Inst.GetPiece(pieceEnum);
        curPiece = Instantiate(piece, transform.position, Quaternion.identity).GetComponent<Piece>();

        curChecker = GameManager.Inst.checker;
        curPiece.Init(GameManager.Inst.checker);
    }
}

public enum CheckerEnum
{
    White,
    Black,
    Empty
}

