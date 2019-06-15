using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetVersion : MonoBehaviour {

	void Start () {

		Text TextComponent = GetComponent<Text>();
		TextComponent.text = Application.version;
		
	}
}
