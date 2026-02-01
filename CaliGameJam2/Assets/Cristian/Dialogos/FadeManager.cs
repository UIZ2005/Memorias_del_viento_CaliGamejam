using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    // Método público que puedes llamar desde cualquier otro script
    public void FadeAndShowDialogs(GameObject canvasToFadeOut, GameObject canvasDialogos, float waitSeconds = 2f)
    {
        StartCoroutine(FadeAndShowRoutine(canvasToFadeOut, canvasDialogos, waitSeconds));
    }

    private IEnumerator FadeAndShowRoutine(GameObject canvasToFadeOut, GameObject canvasDialogos, float waitSeconds)
    {
        // Hacer fade out
        if (canvasToFadeOut != null)
        {
            var fadeScript = canvasToFadeOut.GetComponent<FadeScript>();
            if (fadeScript != null)
            {
                fadeScript.ShowUI();
                fadeScript.HideUI();
            }
        }

        // Esperar los segundos indicados
        yield return new WaitForSeconds(waitSeconds);

        // Hacer fade in en canvasDialogos
        if (canvasDialogos != null)
        {
            var fadeScript = canvasDialogos.GetComponent<FadeScript>();
            if (fadeScript != null)
            {
                canvasDialogos.SetActive(true); // Asegura que esté activo
                fadeScript.ShowUI(); // Empieza el fade in
            }
        }
    }
}
