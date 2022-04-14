using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    private HashSet<KeyValuePair<string, object>> preconditions;
    private HashSet<KeyValuePair<string, object>> effects;

    private bool inRange = false;

    //a bigger cost affects what actions are picked during planning
    public float cost = 1f;

    //object some actions are preformed on. Can be null
    public GameObject target;

    public GoapAction()
    {
        preconditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public void doReset()
    {
        inRange = false;
        target = null;
        reset();
    }

    //reset any variables needed before planning starts
    public abstract void reset();

    public abstract bool isDone();

    public abstract bool checkProceduralPrecondition(GameObject agent);

    public abstract bool perform(GameObject agent);

    public abstract bool requiresInRange();

    public bool isInRange()
    {
        return inRange;
    }

    public void setInRange(bool inRange)
    {
        this.inRange = inRange;
    }

    public void addPrecondition(string key, object value)
    {
        preconditions.Add(new KeyValuePair<string, object>(key, value));
    }

    public void removePrecondition(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in preconditions) {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }

        if (!default(KeyValuePair<string, object>).Equals(remove))
            preconditions.Remove(remove);
    }
    
    public void addEffect(string key, object value) {
        effects.Add (new KeyValuePair<string, object>(key, value) );
    }


    public void removeEffect(string key) {
        KeyValuePair<string, object> remove = default(KeyValuePair<string,object>);
        foreach (KeyValuePair<string, object> kvp in effects) {
            if (kvp.Key.Equals (key)) 
                remove = kvp;
        }
        if ( !default(KeyValuePair<string,object>).Equals(remove) )
            effects.Remove (remove);
    }
    
    public HashSet<KeyValuePair<string, object>> Preconditions {
        get {
            return preconditions;
        }
    }

    public HashSet<KeyValuePair<string, object>> Effects {
        get {
            return effects;
        }
    }
}
