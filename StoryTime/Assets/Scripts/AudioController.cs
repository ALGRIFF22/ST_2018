using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {


	public List<GameObject> AmbientSounds;
	public List<GameObject> ActionSounds;

	int activePage;

	private void OnEnable()
	{
		BookSelect.SendBookEvent += SendBookEvent;
		PageTurnController.CurrentPageActive += PageSounds;
		ExampleStreaming.SendKeyword += PlayActionSound;
	}

	private void OnDestroy()
	{
		BookSelect.SendBookEvent -= SendBookEvent;
		PageTurnController.CurrentPageActive -= PageSounds;
		ExampleStreaming.SendKeyword -= PlayActionSound;
	}

	void SendBookEvent(GameObject book)
	{
		GameObject sound = book.transform.GetChild(1).gameObject;
		AmbientSounds = sound.GetChildren();
	}

	void PageSounds(GameObject pageSounds, int pageNumber)
	{
		
		activePage = pageNumber;
		GameObject c = pageSounds.transform.Find("Sounds").gameObject;
		StopActionSound();
		ActionSounds = c.GetChildren();
	}

	void PlayActionSound(string keyword)
	{
		for(int i = 0; i < ActionSounds.Count; i++)
		{
			if (ActionSounds[i].name == keyword)
			{
				AudioSource a = ActionSounds[i].GetComponent<AudioSource>();
				a.volume = 1;
				a.Play();
			}
			else return;
		}
	}

	void PlayAmbientSound()
	{

	}

	void StopActionSound()
	{
		for (int i = 0; i < ActionSounds.Count; i++)
		{
			AudioSource a = ActionSounds[i].GetComponent<AudioSource>();
			while (a.volume > 0)
			{
				a.volume = a.volume - .01f;
			}
		}
	}

	void StopAmbientSound()
	{

	}
}
