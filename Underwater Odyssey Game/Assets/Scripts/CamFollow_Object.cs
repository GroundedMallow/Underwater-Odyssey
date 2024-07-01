using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow_Object : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [Header("Rotation")]
    [SerializeField] private float _flipYRotationTime = .5f;

    private Coroutine turnCoroutine;
    private bool _isFacingRight;
    private Player_Controller player;

    private void Awake()
    {
       player = playerTransform.gameObject.GetComponent<Player_Controller>();
        _isFacingRight = player.isFacingRight;
    }

    private void Update()
    {
        //camObj follow player pos
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipY());
    }

    private IEnumerator FlipY()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = EndRotation();
        float yRotation = 0f;
        float time = 0f;

        while(time < _flipYRotationTime)
        {
            time += Time.deltaTime;

            //lerp y rotation
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (time / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float EndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}
