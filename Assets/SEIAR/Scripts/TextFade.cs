using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{

	public float TimeFade;
	private Text TextComponent;
	private float TimeFadeBackup;

	void Start ()
	{

		TextComponent = GetComponent<Text> (); //TextComponent receberá o componente texto.
        TimeFadeBackup = TimeFade; //O "Backup" de TimeFade receberá o seu valor inicial.

	}

	void Update ()
	{

        TimeFade -= Time.fixedDeltaTime; //TimeFade perderá seu valor de acordo com o tempo.

		if (TimeFade <= 0) { //Se TimeFade chegar a zero.

            TextComponent.CrossFadeAlpha(0.0f, 0.75f, true); //TextComponent ficará "invisível" em 0.75 segundos.
            StartCoroutine("Fade"); //Será iniciada a Coroutine Fade.
            TimeFade = TimeFadeBackup; //TimeFade receberá o valor do seu "Backup".

		} 
	}

    IEnumerator Fade ()
    {
        yield return new WaitForSecondsRealtime(2.75f); //Depois de 2.75 segundos.

        TextComponent.CrossFadeAlpha(1.0f, 0.75f, true); //TextComponent ficará "visível" em 0.75 segundos.
    }
}