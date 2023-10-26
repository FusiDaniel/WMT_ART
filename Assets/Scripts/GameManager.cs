using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private int _points = 0;

    private int[,] _badges = new int[12, 12];

    private List<QuestionObject> _questions = new List<QuestionObject>(30);

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GenerateBadges();
            GenerateQuestions();

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SavePoints(int points) => this._points = points;

    public int GetPoints() => _points;

    public void SaveBadges(int[,] badges) => this._badges = badges;

    public int[,] GetBadges() => this._badges;

    private void GenerateBadges()
    {
        foreach (EArtType artType in Enum.GetValues(typeof(EArtType)))
        {
            _badges[(int)EItemType.ArtType, (int)artType] = (int)artType;
            _badges[(int)EItemType.Price, (int)artType] = 50;
            _badges[(int)EItemType.Quantity, (int)artType] = 0;
        }
    }

    private void GenerateQuestions()
    {
        string json = File.ReadAllText("C:\\Users\\casju\\Documents\\UFABC\\Jogos Digitai\\WMT_ART\\Assets\\Scripts\\Questoes_json.json"); // alterar caminho do JSON
        _questions = JsonConvert.DeserializeObject<List<QuestionObject>>(json);
    }

    public List<QuestionObject> GetQuestions() => this._questions;
    public void SaveQuestions(List<QuestionObject> questions) => this._questions = questions;
}
