using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class UIController : MonoBehaviour
{
    [SerializeField] private MazeMaker mazeMaker;
    [SerializeField] private BFS bfs;
    [SerializeField] private TextMeshProUGUI farText;
    [SerializeField] private TextMeshProUGUI resultText;
    
    private StringBuilder sb = new StringBuilder();
    public void SearchOneStep() => bfs.SearchOneStep();
    public void StartBFSCoroutine() => bfs.StartBFSCoroutine();

    private void Start()
    {
        bfs.farCount += ChangeFarText;
        bfs.end += ChangeResultText;
    }
    public void ChangeFarText(int far)
    {
        farText.text = sb.Clear().Append("Far : ").Append(far).ToString();
    }

    public void ChangeResultText(int result)
    {
        resultText.gameObject.SetActive(true);
        resultText.text = sb.Clear().Append("Result : ").Append(result).ToString();
    }

}
