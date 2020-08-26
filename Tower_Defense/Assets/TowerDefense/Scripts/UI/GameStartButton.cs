using Core.Game;
using TowerDefense.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace TowerDefense.UI
{
	/// <summary>
	/// The button for selecting a level
	/// </summary>
	[RequireComponent(typeof(Button))]
	public class GameStartButton : MonoBehaviour
	{
		[SerializeField] protected string sceneName;
		
		public void ButtonClicked()
		{
			ChangeScenes();
		}

		protected void ChangeScenes()
		{
			SceneManager.LoadScene(sceneName);
		}
		
	}
}