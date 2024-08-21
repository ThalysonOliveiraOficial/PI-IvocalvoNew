using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para trabalhar com o componente Image

public class ImagePuzzle : MonoBehaviour
{
    public ImageBox boxPrefab;
    public ImageBox[,] boxes = new ImageBox[4, 4];
    public Sprite[] sprites;

    private void Start()
    {
        Init();
        Shuffle();
    }

    void Init()
    {
        int n = 0;
        for (int y = 3; y >= 0; y--)
        {
            for (int x = 0; x < 4; x++)
            {
                ImageBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                box.Init(x, y, n + 1, sprites[n], ClickToSwap);
                boxes[x, y] = box;
                n++;
            }
        }
    }

    void Shuffle()
    {
        List<ImageBox> allBoxes = new List<ImageBox>();

        // Adiciona todas as caixas a uma lista
        foreach (var box in boxes)
        {
            allBoxes.Add(box);
        }

        // Embaralha a lista
        for (int i = 0; i < allBoxes.Count; i++)
        {
            ImageBox temp = allBoxes[i];
            int randomIndex = Random.Range(i, allBoxes.Count);
            allBoxes[i] = allBoxes[randomIndex];
            allBoxes[randomIndex] = temp;
        }

        // Reposiciona as caixas embaralhadas
        for (int y = 3; y >= 0; y--)
        {
            for (int x = 0; x < 4; x++)
            {
                boxes[x, y] = allBoxes[(3 - y) * 4 + x];
                boxes[x, y].UpdatePos(x, y);
            }
        }
    }

    void ClickToSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);

        if (dx == 0 && dy == 0) return;

        var from = boxes[x, y];
        var target = boxes[x + dx, y + dy];

        // Swap these 2 boxes
        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        // Update positions of the 2 boxes
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);

        // Check if puzzle is solved
        if (IsPuzzleSolved())
        {
            ShowSuccessAlert();
        }
    }

    bool IsPuzzleSolved()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (boxes[x, y].index != y * 4 + x + 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void ShowSuccessAlert()
    {
        Debug.Log("Puzzle Solved!"); // Utilize seu método de exibição de alerta preferido aqui
        // Exemplo com Unity UI:
        // alertText.text = "Puzzle Solved!";
    }

    int getDx(int x, int y)
    {
        // Is right empty
        if (x < 3 && boxes[x + 1, y].IsEmpty())
        {
            return 1;
        }
        // Is left empty
        if (x > 0 && boxes[x - 1, y].IsEmpty())
        {
            return -1;
        }
        return 0;
    }

    int getDy(int x, int y)
    {
        // Is top empty
        if (y < 3 && boxes[x, y + 1].IsEmpty())
        {
            return 1;
        }
        // Is bottom empty
        if (y > 0 && boxes[x, y - 1].IsEmpty())
        {
            return -1;
        }
        return 0;
    }
}