using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardEvent {

    private Vector2 pos;
    private List<BoardObject> objs;

    public BoardEvent(Vector2 pos, List<BoardObject> objs)
    {
        this.pos = pos;
        this.objs = objs;
    }

    public Vector2 getPos()
    {
        return pos;
    }

    public List<BoardObject> getObjs()
    {
        return objs;
    }
}
