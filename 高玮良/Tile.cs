using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Text valueText;
    [SerializeField] private Image background;

    [SerializeField] private Color[] tileColors;

    private Vector2Int gridPosition;
    private int value;
    private bool mergedThisTurn = false;

    public Vector2Int GridPosition => gridPosition;
    public int Value => value;

    public void Initialize(Vector2Int position, int initialValue)
    {
        gridPosition = position;
        SetValue(initialValue);
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        valueText.text = value.ToString();

        // Update the tile color based on its value
        int colorIndex = Mathf.Min(Mathf.FloorToInt(Mathf.Log(value, 2)) - 1, tileColors.Length - 1);
        background.color = tileColors[colorIndex];

        mergedThisTurn = false;
    }

    public bool CanMergeWith(int otherValue)
    {
        return value == otherValue && !mergedThisTurn;
    }

    public void MoveTo(Vector2Int newPosition, Vector3 worldPosition, float duration)
    {
        gridPosition = newPosition;
        StartCoroutine(MoveAnimation(worldPosition, duration));
    }

    private System.Collections.IEnumerator MoveAnimation(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    public void MarkAsMerged()
    {
        mergedThisTurn = true;
    }
}