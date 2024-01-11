using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Moji : MonoBehaviour
{

	[SerializeField] private string MOJI;
	public Nazo_Answer nazo_Answer;

	public void OnClickStartButton()
	{
		nazo_Answer.InputMOJI(MOJI);
	}
}