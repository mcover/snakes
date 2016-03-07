using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardEvent {

    private Vector2 pos;
    private List<BoardObject> objectPair;

    public BoardEvent(Vector2 pos, List<BoardObject> objectPair)
    {
        this.pos = pos;
        this.objectPair = objectPair;
    }

    public Vector2 getPos()
    {
        return pos;
    }

    public List<BoardObject> getObjectPair()
    {
        return objectPair;
    }
}
