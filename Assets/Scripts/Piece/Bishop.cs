using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{    public override void Init(CheckerEnum _checker)
    {
        base.Init(_checker);

        diff = new List<Coordinate>()
        {
            new Coordinate(1,1),
            new Coordinate(1,-1),
            new Coordinate(-1,1),
            new Coordinate(-1,-1)
        };
        range = 3;
    }
}
