using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void ReturnMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
	public void howtoplayscreen()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +3);
	}
	
	public void returnmeunufromhowtoplayscreen()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -3);
	}
}
