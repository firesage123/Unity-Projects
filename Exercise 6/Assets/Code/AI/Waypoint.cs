using System;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

/// <summary>
/// A waypoint to be used for path planning
/// </summary>
public class Waypoint : MonoBehaviour
{
    /// <summary>
    /// Other Waypoints you can get to from this one with a straight-line path
    /// </summary>
    [NonSerialized]
    public List<Waypoint> Neighbors = new List<Waypoint>();
    
    public float Priority { get; protected internal set; }

    /// <summary>
    /// Represents the current position in the queue
    /// </summary>
    public int QueueIndex { get; internal set; }

    /// <summary>
    /// Used in path planning; next closest node to the start node
    /// </summary>
    private Waypoint predecessor;

    /// <summary>
    /// Cached list of all waypoints.
    /// </summary>
    static Waypoint[] AllWaypoints;

    /// <summary>
    /// Compute the Neighbors list
    /// </summary>
    internal void Start()
    {
        var position = transform.position;
        if (AllWaypoints == null)
        {
            AllWaypoints = FindObjectsOfType<Waypoint>();
        }

        foreach (var wp in AllWaypoints) 
            if (wp != this && !BehaviorTreeNode.WallBetween(position, wp.transform.position))
                Neighbors.Add(wp);
    }

    /// <summary>
    /// Visualize the waypoint graph
    /// </summary>
    internal void OnDrawGizmos()
    {
        var position = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, 0.5f);
        foreach (var wp in Neighbors)
            Gizmos.DrawLine(position, wp.transform.position);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(NearestWaypointTo(BehaviorTreeNode.Player.transform.position).transform.position, 1f);
    }

    /// <summary>
    /// Nearest waypoint to specified location that is reachable by a straight-line path.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Waypoint NearestWaypointTo(Vector2 position)
    {
        Waypoint nearest = null;
        var minDist = float.PositiveInfinity;
        for (int i = 0; i < AllWaypoints.Length; i++)
        {
            var wp = AllWaypoints[i];
            var p = wp.transform.position;
            var d = Vector2.Distance(position, p);
            if (d < minDist && !BehaviorTreeNode.WallBetween(p, position))
            {
                nearest = wp;
                minDist = d;
            }
        }
        return nearest;
    }

    /// <summary>
    /// Returns a series of waypoints to take to get to the specified position
    /// </summary>
    /// <param name="start">Starting position</param>
    /// <param name="end">Desired endpoint</param>
    /// <returns></returns>
    public static List<Waypoint> FindPath(Vector2 start, Vector2 end)
    {
        return FindPath(NearestWaypointTo(start), NearestWaypointTo(end));
    }

    
    public static Dictionary<Waypoint, Waypoint> parent = new Dictionary<Waypoint, Waypoint>();
    public static Dictionary<Waypoint, float> currcost = new Dictionary<Waypoint, float>();
    
    
    /// <summary>
    /// Finds a sequence of waypoints between a specified pair of waypoints.
    /// IMPORTANT: this is a deliberately bad path planner; it's just BFS and doesn't
    /// pay attention to edge lengths.  Your job is to make it find the actually shortest path.
    /// </summary>
    /// <param name="start">Starting waypoint</param>
    /// <param name="end">Goal waypoint</param>
    /// <returns></returns>
    static List<Waypoint> FindPath(Waypoint start, Waypoint end)
    {
        // Do a BFS of the graph
//        var q = new Queue<Waypoint>();
//        foreach (var wp in AllWaypoints)
//            wp.predecessor = null;
//        q.Enqueue(start);
//        Waypoint node;
//        while ((node = q.Dequeue()) != end)
//        {
//            foreach (var n in node.Neighbors)
//            {
//                if (n.predecessor == null)
//                {
//                    q.Enqueue(n);
//                    n.predecessor = node;
//                }
//            }
//        }
//
//        // Reconstruct the path
//        var path = new List<Waypoint>();
//        path.Add(node);
//        while (node != start)
//        {
//            node = node.predecessor;
//            path.Insert(0, node);
//        }
//        return path;
        
        
        
        
        // Do a A* search of the graph
        var q = new FastPriorityQueue<Waypoint>(AllWaypoints.Length);
        foreach (var wp in AllWaypoints)
            wp.predecessor = null;
        q.Enqueue(start, 0);
        parent[start] = start;
        currcost[start] = 0;
        Waypoint node = start;
        while (q.Count > 0) {
            node = q.Dequeue();
            if (node == end) {
                break;
            }
            foreach (var n in node.Neighbors) {
                float cost = currcost[node] + Vector3.Distance(node.transform.position, n.transform.position);
                if (!currcost.ContainsKey(n) || cost < currcost[n]) {
                    currcost[n] = cost;
                    float priority = cost + Vector3.Distance(n.transform.position, end.transform.position);
                    q.Enqueue(n, priority);
                    n.predecessor = node;
                    
                }
            }
        }

        // Reconstruct the path
        var path = new List<Waypoint>();
        if (node != null)
            path.Add(node);
        while (node != start)
        {
            node = node.predecessor;
            path.Insert(0, node);
        }
        return path;
    }
}