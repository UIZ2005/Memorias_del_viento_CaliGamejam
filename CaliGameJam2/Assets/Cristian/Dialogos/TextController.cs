using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public GameObject currentDialog;
    public GameObject nextDialog;
    public GameObject currentButton;
    public GameObject nextButton;
    public GameObject canvasDialogos;
    public GameObject canvasToFadeOut;
    public GameObject currentImage;
    public GameObject nextImage;
    public FadeManager fadeManager;
    public bool ultimo = false;
    public bool isTyping = false;
    public TextMeshProUGUI textComponent;
    [TextArea]
    public string fullText; // El texto completo del diálogo
    public float delay = 0.05f; // Tiempo entre letras

    private Coroutine typingCoroutine;

    void OnEnable()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textComponent.text = "";
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        textComponent.text = "";
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }
        isTyping = false;
    }

    public void SkipAnimation()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        textComponent.text = fullText;
        isTyping = false;
    }
    public void OnContinue()
    {
        var typewriter = currentDialog.GetComponent<TextController>();
        if (typewriter.isTyping)
        {
            typewriter.SkipAnimation();
        }
        else
        {
            if (fadeManager != null)
            {
                fadeManager.FadeAndShowDialogs(canvasToFadeOut, canvasDialogos, 0.5f);
            }
            if (canvasDialogos != null && ultimo)
            {
                canvasDialogos.SetActive(false);
            }
            if (currentDialog != null)
            {
                currentDialog.SetActive(false);
            }
            if (nextDialog != null)
            {
                nextDialog.SetActive(true);
            }
            if (currentButton != null)
            {
                currentButton.SetActive(false);
            }
            if (nextButton != null)
            {
                nextButton.SetActive(true);
            }
            if (currentImage != null)
            {
                currentImage.SetActive(false);
            }
            if (nextImage != null)
            {
                nextImage.SetActive(true);
            }
        }
    }
}

