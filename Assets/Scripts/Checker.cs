using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public CheckerEnum curChecker;
    public Coordinate coord { get; set; }
    public Piece curPiece;
    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    public void OnMouseDown()
    {
        if(curChecker == CheckerEnum.Empty)
            Spawn(PieceEnum.Pawn);
    }

    public void MovePiece(Checker _checker)
    {
        if(this ==  _checker) return;

        curPiece.transform.position = _checker.transform.position;
        curPiece.coord = _checker.coord;
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

        curPiece.coord = coord;
        curPiece.Init(GameManager.Inst.checker);

    }

    public void Highlight(Color color)
    {
        sp.color = color;
    }
}

