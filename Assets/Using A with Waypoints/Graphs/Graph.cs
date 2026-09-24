using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if(from != null && to != null)
        {
            Edge e = new Edge(from, to);
            edges.Add(e);
            from.edgeList.Add(e);
        }
    }

    Node FindNode(GameObject id)
    {
        foreach (Node n in nodes)
        {
            if (n.getId() == id)
            return n;
        }
        return null;
    }
}
