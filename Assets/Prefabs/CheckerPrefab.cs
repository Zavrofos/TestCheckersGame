using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPrefab : MonoBehaviour
{
    public Transform Transform;
    public SpriteRenderer SpriteRenderer;
    public Sprite BlackCheckerSprite;
    public Sprite WhiteCheckerSprite;
    public Sprite WhiteKingSprite;
    public Sprite BlackKingSprite;

    public Vector2 InitialPosition;
    public Sprite InitialSprite;

    public void MoveAnimate(int newPositionX, int newPositionY)
    {
        StartCoroutine(MoveCoroutine(newPositionX, newPositionY));
    }

    private IEnumerator MoveCoroutine(int newPositionX, int newPositionY)
    {
        Vector3 startPosition = Transform.position;
        Vector3 newPosition = new Vector2(newPositionX, newPositionY);
        float part = 0.02f;
        while (part <= 1)
        {
            Transform.position = Vector3.Lerp(startPosition, newPosition, part);
            yield return new WaitForSeconds(0.005f);
            part += 0.02f;
        }
        Transform.position = newPosition;
    }
}