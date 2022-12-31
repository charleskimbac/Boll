/*
    TO DO
-scrollable ldrboard - https://www.youtube.com/watch?v=zbUVWnq9j20
color change trigger
color change player

*/

//WARNING: PLAYERPREF IS NOT FULLY IMPLEMENTED. (highScore)

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    //public GameObject updateGameScreen;
    private bool updateGame;

    public RemoteConfig remoteConfig;
    public MapManager mapManager;

    public ResetPlayerPref resetPref;
    public Player player;
    public LeaderboardController leaderboard;
    private float lastTime;
    public GameObject menuUI;
    public GameObject activeUI;
    public Text highScoreText;
    public GameObject needNewScore;
    public GameObject loadingObj;
    public GameObject waitForLoad;
    public GameObject lastTimeObj;
    public GameObject highScoreObj;
    private float highScore;
    public Canvas canvas;
    public Button submitScoreButton;

    public Text lastTimeText;
    
    // Start is called before the first frame update
    void Start(){
        lastTime = float.MaxValue;
        StartCoroutine(firstLeaderboardShow());
        resetPref.resetData();
        //lastTime = getTime();
        //currentTimeText.text = "Last time: " + lastTime.ToString();
		highScore = float.MaxValue - 1;
        updateGame = true;
        submitScoreButton.interactable = false;
        //highScoreObj.SetActive(false);
        //lastTimeObj.SetActive(false);
        waitForLoad.SetActive(false);

        /*
        if (!updateGame){
            updateGameScreen.SetActive(false);
        }
        else {
            updateGameScreen.SetActive(true);
        }
        */ 
    }

    IEnumerator firstLeaderboardShow(){
        yield return new WaitForSeconds(3);
        //Debug.Log("Loading scores...");
        waitForLoad.SetActive(true);
        leaderboard.showScores();
        loadingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("space pressed");
            ifSpacePressed();
        }
        /*
        if (GameObject.Find("scoreDisplay") == null && GameObject.Find("scoreDisplay(Clone)") == null){
            currentTimeText = Instantiate(text, new Vector3(0,0,0), Quaternion.Euler(0,0,0)) as Text;
		    Debug.Log("TEXT SPAWNED: " + currentTimeText);
		    //Parent to the panel
		    currentTimeText.transform.SetParent(menuUI.transform, false);
            //Set the text box's text element font size and style:
            currentTimeText.fontSize = 25;
            //Set the text box's text element to the current textToDisplay:
            currentTimeText.text = lastTime.ToString();
        }
        */
        //Debug.Log(currentTimeText);
        //Debug.Log(leaderboard.getTime());
        //Debug.Log(PlayerPrefs.GetFloat("Score"));
    }
    
	public float getTime(){
        Debug.Log("received playerprefs lastTime: " + PlayerPrefs.GetFloat("Score"));
		return PlayerPrefs.GetFloat("Score");
	}

    void ifSpacePressed(){
        Debug.Log("finished?: " + player.finishedRound());
        Debug.Log("HS?: " + highScore);
        Debug.Log("LS?: " + lastTime);
        if (player.finishedRound()){
            PlayerPrefs.SetFloat("Score", player.getCurrentTime());
            lastTime = getTime();
            updateLastTime();
            if (checkHighScore()){
                submitScoreButton.interactable = true;
                needNewScore.SetActive(false);

            }
            else {
                submitScoreButton.interactable = false;
            }
        }
        mapManager.resetMap();
        player.reset();
        remoteConfig.updateConfig();
        highScoreObj.SetActive(true);
        lastTimeObj.SetActive(true);
        activeUI.SetActive(false);
        menuUI.SetActive(true);
    }

    void updateLastTime(){
        lastTimeText.text = "Last time: " + lastTime + "s";
    }

    public bool checkHighScore(){           //if true, allows for score to be submitted
        if (highScore > lastTime) {
            highScore = lastTime;
            highScoreText.text = "High score: " + lastTime.ToString() + "s";
            Debug.Log("new highscore");
            return true;
        }
        else {
            Debug.Log("highscore: " + highScore + " currentscore: " + lastTime);
            return false;
        }
	}
}