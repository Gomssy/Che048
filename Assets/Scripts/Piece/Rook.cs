using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{    public override void Init(CheckerEnum _checker)
    {
        base.Init(_checker);

        diff = new List<Coordinate>()
        {
            new Coordinate(0,1),
            new Coordinate(1,0),
            new Coordinate(0,-1),
            new Coordinate(-1,0),
        };
        range = 3;
    }
}
