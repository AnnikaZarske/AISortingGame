using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalTracker : MonoBehaviour
{
    public int plankGoal = 15;
    public int brickGoal = 30;
    public int toolGoal = 10;
    
    public TextMeshProUGUI plankGoalText;
    public TextMeshProUGUI brickGoalText;
    public TextMeshProUGUI toolGoalText;
    public TextMeshProUGUI plankNumText;
    public TextMeshProUGUI brickNumText;
    public TextMeshProUGUI toolNumText;
    public StorageComponent storage;
    public SmithComponent smith;

    // Start is called before the first frame update
    void Start()
    {
        storage = FindObjectOfType<StorageComponent>().GetComponent<StorageComponent>();
        smith = FindObjectOfType<SmithComponent>().GetComponent<SmithComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        plankNumText.text = storage.numPlanks.ToString();
        brickNumText.text = storage.numBricks.ToString();
        toolNumText.text = smith.numTools.ToString();
        
        checkGoal();
    }

    private void checkGoal()
    {
        if (smith.numTools >= toolGoal)
        {
            // set done
            // set not done if it falls under goal again
        }
    }
}
