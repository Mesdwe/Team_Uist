using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    [SerializeField] Transform origin;
    [SerializeField] Transform upgradeTransform;
    [SerializeField] float speed;
    private bool moving;
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject hudCanvas;


    void Start()
    {
        //origin = transform;
        target = upgradeTransform;
        LevelManager.Instance.OnUpgrade += MoveCamera;
        LevelManager.Instance.OnLevelEnd += ResetCamera;
    }

    private void MoveCamera()
    {
        target = upgradeTransform;
        moving = true;
        hudCanvas.SetActive(false);

    }
    private void ResetCamera()
    {
        target = origin;
        upgradeCanvas.SetActive(false);

        hudCanvas.SetActive(true);
        moving = true;
    }

    void Update()
    {
        if (moving)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                moving = false;
                if (target == upgradeTransform)
                    upgradeCanvas.SetActive(true);
            }
        }
    }

    void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnUpgrade -= MoveCamera;
            LevelManager.Instance.OnLevelEnd -= ResetCamera;
        }
    }
}
