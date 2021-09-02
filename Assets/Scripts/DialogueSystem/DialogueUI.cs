using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] private Text _dialogueText;

	private TypeWriter _writer;
	
	private void Start()
	{
		_writer = GetComponent<TypeWriter>();
		_writer.Run("Hello fren\n text is here", _dialogueText);
	}
}