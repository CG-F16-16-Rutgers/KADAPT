using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gui_text : MonoBehaviour {

	public Text text;
	private GameObject player_transform;
	// Use this for initialization
	void Start () {
		
		text.text = "GAME DEMO -- Saving Private Brian";
		text.text += "\n\n\n Your friend has been abducted! \nHe managed to text you this: 'I can see water, on the other side there's a white and a red shroom! Come quick!' ";


		//player_transform = GameObject.FindWithTag ("player");
	}
	
	// Update is called once per frame
	void Update () {
		//text.text = player_transform.transform.localPosition.ToString();
			
	}
}
