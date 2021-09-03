using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] private Text dialogueText;
	[SerializeField] private DialogueObject testDialogue;
	private TypeWriter _writer;
	
	private void Start()
	{
		_writer = GetComponent<TypeWriter>();
		SetDialogueBoxActive(false);
		ShowDialogue(testDialogue);
	}

	private void ShowDialogue(DialogueObject dialogueObject)
	{
		StartCoroutine(StepThroughDialogue(dialogueObject));
	}

	private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
	{
		SetDialogueBoxActive(true);
		foreach (var dialogue in dialogueObject.Dialogue)
		{
			yield return _writer.Run(dialogue, dialogueText);
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
		}
		SetDialogueBoxActive(false);
	}

	private void SetDialogueBoxActive(bool status)
	{
		dialogueText.gameObject.SetActive(status);
	}
}