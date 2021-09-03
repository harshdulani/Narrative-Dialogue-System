using System.Collections;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float _typingSpeed = 50f;
    
    public Coroutine Run(string text, UnityEngine.UI.Text textBox)
    { 
	    return StartCoroutine(Typer(text, textBox));
    }

    private IEnumerator Typer(string text, UnityEngine.UI.Text textBox)
    {
	    float t = 0;
        textBox.text = "";

        int i = 0;
        while (i < text.Length)
        {
	        t += Time.deltaTime * _typingSpeed;
	        i = Mathf.FloorToInt(t);
	        i = Mathf.Clamp(i, 0, text.Length);

	        textBox.text = text.Substring(0, i);
	        yield return null;
        }

        textBox.text = text;
    }
}
