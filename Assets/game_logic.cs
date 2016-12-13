using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class game_logic : MonoBehaviour {
	public Text text;
	public Text player_pos;
	private GameObject player;
	private Vector3 player_position;


	//1st stage: looking for the key
	private bool found_key=false;
	private double key_timer = 0.0f;
	private Vector3 key_pos;
	//2nd stage: sneak outside the correct building
	private bool seen=false;
	//3rd stage: get some water
	private bool got_in_lake=false;
	//4th stage: WIN
	private bool found_brian=false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("player");
		key_pos = GameObject.FindWithTag ("keys").transform.localPosition;
		text.text += "\n";
	}
	
	// Update is called once per frame
	void Update () {
		key_timer += Time.deltaTime;
		player_position = player.transform.localPosition;
		player_pos.text = player_position.ToString ();
		//text.text.Insert (text.text.Length, player_position.ToString());

		//1st stage:
		if (!found_key) {
			if (key_timer > 20 && key_timer < 20.1) {
				text.text = "Where would you hide the keys to a military camp...?";
			}
			if (key_timer > 35 && key_timer < 35.1) {
				text.text = "Hurry up!!! Is there a door mat around?";
			}
			if (key_timer > 55 && key_timer < 55.1) {
				text.text = "You hear Brian's voice from far away: \n IT'S UNDER THE ROCK YOU MORON!!!";
			}
			if (key_timer > 120) {
				//GAME OVER
			}

			if (Input.GetKeyUp ("e")) {
				if (check_if_near (player_position, key_pos, 8)) {
					text.text = "Congratulations! You found the key! Now what???";
					key_timer = 0.0f;
					found_key = true;
					print ("I GOT THE KEY");
				}
			}
		}

		//2nd stage
		if(found_key){
			if (seen) {
				//GAME OVER
			}
		}
	}

	bool check_if_near(Vector3 player_pos, Vector3 poi_pos, double thres)
	{
		double dist = Vector3.Distance (player_pos, poi_pos);
		print (dist);
		if (dist < thres) {
			return true;
		} else {
			return false;
		}
	}
}
