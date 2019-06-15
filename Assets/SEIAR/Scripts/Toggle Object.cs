using UnityEngine;

public class ToggleObject : MonoBehaviour {

	public GameObject[] Object;
	public string[] ObjectNameText;

	private int ObjectNameNumber;

	public void NextButton () {

		if (ObjectNameNumber + 1 < Object.Length && ObjectNameNumber + 1 < ObjectNameText.Length) {

			ObjectNameNumber++;
	
		}
	}

	public void PreviousButton () {

		if (ObjectNameNumber - 1 > Object.Length && ObjectNameNumber + 1 > ObjectNameText.Length) {
				
			ObjectNameNumber--;

	}
  }
}
