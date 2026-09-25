using UnityEngine;

public class Follow : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wpManager == null)
        {
            Debug.LogError("Follow needs a WPManager reference.", this);
            enabled = false;
            return;
        }

        WPManager manager = wpManager.GetComponent<WPManager>();
        if (manager == null || manager.waypoints == null || manager.waypoints.Length < 5)
        {
            Debug.LogError("Follow needs a WPManager with at least five waypoints.", this);
            enabled = false;
            return;
        }

        wps = manager.waypoints;
        g = manager.graph;
        currentNode = wps[0];
        Time.timeScale = 5;
    }

    public void GoToHeli()
    {
        SetDestination(wps[0]);
    }

    public void GoToRuin()
    {
        SetDestination(wps[1]);
    }

    public void GoToFactory()
    {
        SetDestination(wps[4]);
    }

    void SetDestination(GameObject destination)
    {
        if (!g.AStar(currentNode, destination))
        {
            Debug.LogError("No waypoint path found from " + currentNode.name + " to " + destination.name + ".", this);
            return;
        }

        currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(g.pathList.Count == 0 || currentWP >= g.pathList.Count)
            return;
        
        if(Vector3.Distance(g.pathList[currentWP].getId().transform.position, this.transform.position) < accuracy)

        {
            currentNode = g.pathList[currentWP].getId();
            currentWP++;
        }

        if (currentWP < g.pathList.Count)
        {
            goal = g.pathList[currentWP].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            if (direction.sqrMagnitude > 0.001f)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
                this.transform.position = Vector3.MoveTowards(this.transform.position, lookAtGoal, speed * Time.deltaTime);
            }
        }
    }
}