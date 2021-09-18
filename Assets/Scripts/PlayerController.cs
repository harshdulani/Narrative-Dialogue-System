using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private DialogueUI dialogueUi;
	[SerializeField] private float movementSpeed;

	public DialogueUI DialogueUi => dialogueUi;
	
	public IInteractable Interactable { get; set; }
	
	private void Update()
	{
		if (dialogueUi.isOpen) return;
		
		Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		
		if(movement != Vector2.zero)
			transform.position += new Vector3(movement.x, 0f, movement.y) * (movementSpeed * Time.deltaTime);

		if (Input.GetKey(KeyCode.E))
			Interactable?.Interact(this);
	}
}
