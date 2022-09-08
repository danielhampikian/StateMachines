using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//add one state to this ai, make it involve vision
//a visual feild angle that detects when something is in that visual field)

public class StateController : MonoBehaviour {

    public State startState;
    public bool npc;
    public GameObject navPointsParent;
    public Vector3 targetLocationVector;
    public State currentState;
    public GameObject[] navPoints;
    public GameObject enemyToChase;
    public int navPointNum;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public NavMeshAgent navMeshAgent;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 5;
    public GameObject wanderP;
    public GameObject newNavPoint;
    public GameObject obstacles;
    public GameObject self;


    //new code:
    public float timeRemaining;

 
    void Start()
    {
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        childrenRend = GetComponentsInChildren<Renderer>();

        if (npc)
        {
            currentState = new NPCWander(this);
        }
        else
        {
            SetState(new WanderState(this));
        }
        currentState = startState;
        SetState(currentState);
    }

    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
        {
            timeRemaining = getNewTimeLimit(3,6);
            //event timer whatever state their in let them know

        }

    }
    //new code:
    public float getNewTimeLimit(float min, float max)
    {
        return Random.Range(min, max);
    }
    public bool DoneWithNavPatrol()
    {
        return (navPointNum == navPoints.Length - 1);
    }
    public Transform GetNextNavPoint()
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
        return navPoints[navPointNum].transform;
    }
    public Transform GetWanderPoint()
    {
        //This could be done more efficeintly without introducing a empty game object
        Vector3 wanderPoint = new Vector3(Random.Range(-2f, 2f), 1.5f, Random.Range(-2f, 2f));
        GameObject go = Instantiate(wanderP, wanderPoint, Quaternion.identity);
        return go.transform;
    }
    public Vector3 GetRandomPoint()
    {
        Vector3 ran = new Vector3(Random.Range(-detectionRange, detectionRange), 1.5f, 
            Random.Range(-detectionRange, detectionRange));
        return ran - transform.position;
    }
    public Vector3 GetRunawayTarget()
    {
        return -((enemyToChase.transform.position - transform.position) + GetRandomPoint());
    }

    public void AddNavPoint(Vector3 pos)
    {
        GameObject go = Instantiate(newNavPoint, pos, Quaternion.identity);
        go.transform.SetParent(navPointsParent.transform);
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        

    }

    public void ChangeColor(Color color)
    {
        foreach(Renderer r in childrenRend)
        {
            
            foreach(Material m in r.materials)
            {
                
                m.color = color;
            }
        }
    }
    public bool CheckIfInRange(string tag)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < detectionRange)
                {
                    enemyToChase = g;
                    return true;
                }
            }
        }
        return false;
    }
    public Transform CheckIfInRange(GameObject g)
    {
                if (Vector3.Distance(g.transform.position, transform.position) < detectionRange)
                {
                    enemyToChase = g;
                    return g.transform;
                }

        return transform;
    }


    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}
