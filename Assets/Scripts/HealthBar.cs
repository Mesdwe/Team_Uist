using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds = 0.5f;
    [SerializeField] private float positionOffset;
    private Ship ship;

    RectTransform canvasRect;
    RectTransform rectTransform;

    private Vector2 uiOffset;
    void Start()
    {
        // Get the rect transform
        canvasRect = transform.parent.gameObject.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();


        // Calculate the screen offset
        this.uiOffset = new Vector2((float)canvasRect.sizeDelta.x / 2f, (float)canvasRect.sizeDelta.y / 2f);
    }
    public void SetShip(Ship ship)
    {
        this.ship = ship;
        ship.OnHealthPctChanged += HandleHealthChanged;
    }
    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate()
    {
        //transform.position = Camera.main.WorldToScreenPoint(ship.transform.position + Vector3.up * positionOffset);
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(ship.transform.position);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvasRect.sizeDelta.x, ViewportPosition.y * canvasRect.sizeDelta.y);

        // Set the position and remove the screen offset
        this.rectTransform.localPosition = proportionalPosition - uiOffset;
    }

    private void OnDestroy()
    {
        ship.OnHealthPctChanged -= HandleHealthChanged;
    }
}
