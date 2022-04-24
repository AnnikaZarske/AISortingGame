using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject builtCastle;
    public GameObject unBuiltCastle;
    public Button buildCastleButton;
    public GameObject QuitButton;
    public GameObject winText;
    public bool toolGoalComplete = false;
    public bool brickGoalComplete = false;
    public bool plankGoalComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        storage = FindObjectOfType<StorageComponent>().GetComponent<StorageComponent>();
        smith = FindObjectOfType<SmithComponent>().GetComponent<SmithComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        checkGoal();
    }

    private void checkGoal()
    {
        if (smith.numTools >= toolGoal) {
            toolGoalText.gameObject.SetActive(false);
            toolNumText.text = "COMPLETED";
            toolGoalComplete = true;
        }else {
            toolGoalText.gameObject.SetActive(true);
            toolNumText.text = smith.numTools.ToString();
            toolGoalComplete = false;
        }

        if (storage.numPlanks >= plankGoal) {
            plankGoalText.gameObject.SetActive(false);
            plankNumText.text = "COMPLETED";
            plankGoalComplete = true;
        }else {
            plankGoalText.gameObject.SetActive(true);
            plankNumText.text = storage.numPlanks.ToString();
            plankGoalComplete = false;
        }
        
        if (storage.numBricks >= brickGoal) {
            brickGoalText.gameObject.SetActive(false);
            brickNumText.text = "COMPLETED";
            brickGoalComplete = true;
            
        }else {
            brickGoalText.gameObject.SetActive(true);
            brickNumText.text = storage.numBricks.ToString();
            brickGoalComplete = false;
        }
        
        if (toolGoalComplete && plankGoalComplete && brickGoalComplete) {
            GoalComplete();
        }
    }

    private void GoalComplete()
    {
        if (toolGoalComplete && plankGoalComplete && brickGoalComplete)
        {
            buildCastleButton.gameObject.SetActive(true);
        }
    }

    public void DisplayCastle()
    {
        unBuiltCastle.SetActive(false);
        builtCastle.SetActive(true);
        buildCastleButton.gameObject.SetActive(false);
        winText.SetActive(true);
        QuitButton.SetActive(true);
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
