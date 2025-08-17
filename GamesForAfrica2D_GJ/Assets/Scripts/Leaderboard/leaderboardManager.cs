using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using System;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> PlayerNames;
    [SerializeField] private List<TextMeshProUGUI> PlayerScores;

    private string PublicLeaderboardKey = "b2524ec669e6e40f872afec222b2e086fb327c2f038b735a470b9275168549ef";
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public String PlayerName;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(PublicLeaderboardKey, ((msg) =>       // this method returns a message, so we will now manipulate it in a method within a method which is what the '=>' is for
        {
            for (int count = 0; count < PlayerNames.Count; count++)
            {
                string Name = msg[count].Username;

                if (Name == "")
                {
                    Name = "Unknown";
                }

                PlayerNames[count].text = Name;
                PlayerScores[count].text = Convert.ToString(msg[count].Score);
            }
        }));
    }

    public void UploadNewScoreToLeaderboard(int Score)
    {
        if (PlayerName == "")
        {
            PlayerName = "Uknown";
        }

        LeaderboardCreator.UploadNewEntry(PublicLeaderboardKey, PlayerName, Score);
    }
}
