using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoap
{
    //starting state of the agent and world
    //supply what states are needed for action to run
    HashSet<KeyValuePair<string, object>> getWorldState();
    
    //give planner a new goal so it can figure out what actions are needed to fulfill it
    HashSet<KeyValuePair<string, object>> createGoalState();
    
    //No actions could be found for supplied goal. Try different goal
    void planFailed(HashSet<KeyValuePair<string, object>> failedGoal);
    
    //A plan was found. These are the actions in order
    void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions);
    
    //Goal was reached
    void actionsFinished();
    
    //One of the actions caused the plan to abort
    void planAborted(GoapAction aborter);

    //called during update. Move agent towards next action
    //true if agent at target and actions can be preformed
    //false if not
    bool moveAgent(GoapAction nextAction);
}
