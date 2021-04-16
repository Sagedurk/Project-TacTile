using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurnManager : MonoBehaviour
{

     static Dictionary<string, List<TacticsMovement>> units = new Dictionary<string, List<TacticsMovement>>();
     static Queue<string> turnKey = new Queue<string>();
     static Queue<TacticsMovement> turnTeam = new Queue<TacticsMovement>();


    public static List<TacticsMovement> list;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        List<TacticsMovement> teamList = units[turnKey.Peek()];

        
        foreach (TacticsMovement unit in teamList)
        {
            if(!unit.IsDead())
                turnTeam.Enqueue(unit);
        }

        StartTurn();
    }


    static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {

            /*if (turnTeam.Peek().GetComponent<TacticsCombat>().health <= 0)
            {
                Debug.Log(turnTeam.Peek());
                RemoveUnit(turnTeam.Peek());
                Debug.Log(turnTeam.Peek());
                turnTeam.Dequeue();
            }*/
            turnTeam.Peek().BeginTurn();
        }

    }

    public static void EndTurn()
    {
        TacticsMovement unit = turnTeam.Dequeue();
        
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            //Debug.Log(turnTeam.Peek());
            //Debug.Log(turnTeam.Peek().GetComponent<TacticsCombat>().health);
           /* if (turnTeam.Peek().GetComponent<TacticsCombat>().health <= 0)
            {
                //TurnManager.units.Remove(turnTeam.Peek().ToString());
                RemoveUnit(turnTeam.Peek());
                Debug.Log(turnTeam.Peek());
                turnTeam.Dequeue();
            }*/
            string team = turnKey.Dequeue();
            //Debug.Log("Next Unit" + turnTeam.Peek());
            turnKey.Enqueue(team);
            
            InitTeamTurnQueue();
        }

        TacticsMovement.allowFreeCam = true;
        
    }
    
   

    public static IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public static void AddUnit(TacticsMovement unit)
    {
        

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMovement>();
            units[unit.tag] = list;
            //turnKey decides which team's turn it is
            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }

    public static void RemoveUnit(TacticsMovement unit)
    {
        list.Remove(unit);
    }




}
