using UnityEngine;

public class Square : MonoBehaviour
{
    private bool isVisited = false;

    public bool IsVisitable()
    {
        return !isVisited;
    }

    public void OnVisit()
    {
        isVisited = true;
        SetColor(Color.grey);
    }

    public void OnExit()
    {
        SetColor(Color.cyan);
    }

    public void SetColor(Color colorIn)
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorIn;
    }
}
