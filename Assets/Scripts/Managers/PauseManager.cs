using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	public static UnityAction onSaveClick;
	public static UnityAction onLoadClick;
	
	Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			canvas.enabled = !canvas.enabled;
			Pause();
		}
	}
	
	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
		
	}
	
	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
		}
	}

	/// <summary>
	/// Load Game CallBack
	/// </summary>
    public void LoadSave()
    {
		onLoadClick.Invoke();
    }

	/// <summary>
	/// Save Game CallBack
	/// </summary>
	public void SaveGame()
    {
		onSaveClick.Invoke();
		
    }

    public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
