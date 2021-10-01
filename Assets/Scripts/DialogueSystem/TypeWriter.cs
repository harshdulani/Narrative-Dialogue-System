using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 30f;
    
    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> _punctuations = new List<Punctuation>()
    {
	    {new Punctuation(new HashSet<char>() {'.', '!', '?'}, 0.6f)},
	    {new Punctuation(new HashSet<char>() {',', ';', ':'}, 0.3f)}
    };

    private Coroutine _typerCoroutine;

    public void Run(string text, UnityEngine.UI.Text textBox)
    { 
	    _typerCoroutine = StartCoroutine(Typer(text, textBox));
    }

    public void Stop()
    {
	    StopCoroutine(_typerCoroutine);
	    IsRunning = false;
    }

    private IEnumerator Typer(string text, UnityEngine.UI.Text textBox)
    {
	    IsRunning = true;
	    float t = 0;
        textBox.text = "";

        int charIndex = 0;
        while (charIndex < text.Length)
        {
	        int lastCharIndex = charIndex;
	        
	        t += Time.deltaTime * typingSpeed;
	        charIndex = Mathf.FloorToInt(t);
	        charIndex = Mathf.Clamp(charIndex, 0, text.Length);

	        for (int i = lastCharIndex; i < charIndex; i++)
	        {
		        var isLast = i >= text.Length - 1;

		        textBox.text = text.Substring(0, i + 1);
		        
		        if(IsPunctuation(text[i], out var waitTime) && !isLast && IsPunctuation(text[i + 1], out _))
			        yield return new WaitForSeconds(waitTime);
	        }
	        
	        yield return null;
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char candidate, out float waitTime)
    {
	    foreach (var punctuation in _punctuations)
	    {
		    if (!punctuation.Punctuations.Contains(candidate)) continue;
		    
		    waitTime = punctuation.WaitTime;
		    return true;
	    }

	    waitTime = default;
	    return false;
    }

    private readonly struct Punctuation
    {
	    public readonly HashSet<char> Punctuations;
	    public readonly float WaitTime;

	    public Punctuation(HashSet<char> punctuations, float waitTime)
	    {
		    Punctuations = punctuations;
		    WaitTime = waitTime;
	    }
    }
}
