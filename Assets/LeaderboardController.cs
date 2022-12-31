using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;
using System.Collections;
//using System.Collections.Generic;

public class LeaderboardController : MonoBehaviour
{
	public InputField memberID;
	public int ID;
	int maxScores = 20;
	public Text[] entries;
	private float score;
	private int timeAndSpeed;
	//public Text playerScore;
	public Canvas canvas;
	public Text text;
	public GameManager manager;
	public Button submitScoreButton;
	public Player player;
	private int speed;

	private bool highScoreBool;
	//private float yourHighScore;

	//private List<float> allScores = new List<float>();

	/*
	public void addScore(float temp) {
		allScores.add(temp);
    }
	*/

	public void Start(){
		LootLockerSDKManager.StartGuestSession((response) => {
			if (response.success){
				Debug.Log("success");
			}
			else {
				Debug.Log("failed");
			}
		});
	}

	//public void showCurrentScore(){
		/*
		Debug.Log("SHOWN...VALUE: " + score);
		Debug.Log("scoreObj = " + playerScore);
		Debug.Log("ScoreText: "+ playerScore.text);
		*/

		//playerScore.SetText(score.ToString());
		//playerScore.ForceMeshUpdate(true);
		//Debug.Log(GameObject.Find("scoreDisplay").GetComponent<TextMeshProUGUI>().text);

		/*
		playerScore = Instantiate(text, new Vector3(0,0,0), Quaternion.Euler(0,0,0)) as Text;
		Debug.Log("TEXT SPAWNED: " + playerScore);
		//Parent to the panel
		playerScore.transform.SetParent(canvas.transform, false);
        //Set the text box's text element font size and style:
        playerScore.fontSize = 25;
        //Set the text box's text element to the current textToDisplay:
        playerScore.text = score.ToString();
		*/
 
	//}

	public void showScores(){
		LootLockerSDKManager.GetScoreList(ID, maxScores, (response) => {
			if (response.success){
				LootLockerLeaderboardMember[] scores = response.items;

				for(int i = 0; i < scores.Length; i++){
					entries[i].text = ("<b>" + scores[i].rank + ")</b> " + scores[i].member_id + ":\n" + (scores[i].score / 1000) / 1000.0 + "s | " + scores[i].score % 1000 + " speed");
				}

				if (scores.Length < maxScores){
					for (int i = scores.Length; i < maxScores; i++){
						entries[i].text = "<b>" + (i + 1).ToString() + ".</b>  none";
					}
				}
			}
		});
	}

	public void SubmitScore(){
		score = manager.getTime();
		speed = player.getSpeed();
		timeAndSpeed = int.Parse("" + score * 1000 + speed); //TIMEANDSPEED -> DIVIDE BY 1000 TO GET SPEED, CAST INT TO /100 THEN DIVIDE BY 1000 FOR SPEED
		Debug.Log(timeAndSpeed);
		LootLockerSDKManager.SubmitScore(memberID.text, timeAndSpeed, ID, (response) => {
			if (response.success){
				Debug.Log("success");
			}
			else {
				Debug.Log("failed");
			}
		});
		submitScoreButton.interactable = false;
	}
}