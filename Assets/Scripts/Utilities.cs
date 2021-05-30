using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utilities : MonoBehaviour {
    // Necessary when canvas render mode is Screen Space - Camera
    // Ref: https://forum.unity.com/threads/mouse-position-for-screen-space-camera.294458/
    public static Vector2 ConvertMousePosToWorldPoint() {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10f;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public static IEnumerator FadeOut(GameObject obj) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Color color = obj.GetComponent<Image>().color;

        while (timeElapsed <= totalTime) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(color.a, 0, ( timeElapsed / totalTime ));
            Color newColor = obj.GetComponent<Image>().color;
            newColor.a = newAlpha;
            obj.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    public static IEnumerator FadeIn(GameObject obj) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Color color = obj.GetComponent<Image>().color;

        while (timeElapsed <= totalTime) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(color.a, 1, ( timeElapsed / totalTime ));
            Color newColor = obj.GetComponent<Image>().color;
            newColor.a = newAlpha;
            obj.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }
}
