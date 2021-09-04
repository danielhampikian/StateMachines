using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public GameObject navPointsParent;
    public State currentState;
    public GameObject[] navPoints;
    public GameObject enemyToChase;
    public int navPointNum;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 5;
    public GameObject wanderP;
    public GameObject newNavPoint;


    void Start()
    {
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        childrenRend = GetComponentsInChildren<Renderer>();
        SetState(new PatrolState(this));
    }

    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
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
        Vector3 ran = new Vector3(Random.Range(-detectionRange, detectionRange), 1.5f, Random.Range(-detectionRange, detectionRange));
        return ran;
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
    public bool CheckAttackRange(string tag)
    {
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < 1f )
                {
                    enemyToChase = g;
                    return true;
                }
            }
        }
        return false;
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
