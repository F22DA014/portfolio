using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_Nazo : MonoBehaviour
{
	// ボタンを押すと謎解き画面等に遷移するプログラム
	string str = "Nazo_";
	[SerializeField] private string No;

	public void OnClickStartButton()
	{
		SceneManager.LoadScene(str + No);
	}

}