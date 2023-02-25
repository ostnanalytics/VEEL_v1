using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleCounterController : MonoBehaviour
{
    public Text counterText;

    private int counterValue = 0;
    private int clickCount = 0;
    private bool isMouseInside = false;

    public void SetCounter(int value)
    {
        counterValue = value;
        counterText.text = counterValue.ToString();
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse down inside circle.");

        clickCount++;

        if (clickCount == 2)
        {
            counterValue++;
            counterText.text = counterValue.ToString();
            Debug.Log("Counter incremented. New value: " + counterValue.ToString());
            clickCount = 0;

            if (counterValue == 6)
            {
                Debug.Log("Victory!");
                StartCoroutine(FlipAnimation());
            }
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D hitCollider = Physics2D.OverlapCircle(touchPosition, 0.1f);
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isMouseInside = true;
            }
        }

        if (Input.touchCount == 0)
        {
            isMouseInside = false;
        }

        if (isMouseInside && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            clickCount++;

            if (clickCount == 2)
            {
                counterValue++;
                counterText.text = counterValue.ToString();
                clickCount = 0;

                if (counterValue == 6)
                {
                    StartCoroutine(FlipAnimation());
                }
            }
        }
    }

    private IEnumerator FlipAnimation()
    {
        // Rotate the circle by 180 degrees over 0.5 seconds
        float t = 0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, 180f, t / 0.5f));
            yield return null;
        }

        // Change the text to "Won"
        counterText.text = "Won";
    }
}
