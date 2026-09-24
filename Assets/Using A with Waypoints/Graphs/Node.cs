using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    GameObject id;
    public float xPos;
    public float yPos;
    public float zPos;

    public float f, g, h;
    public Node cameFrom;
    
    public Node(GameObject i)
    {
        id = i;
        path = null;
    }

    public GameObject getId()
    {
        return id;
    }
}
