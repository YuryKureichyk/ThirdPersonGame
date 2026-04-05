using System;

namespace GameAssets.Scripts.DanceBattle
{
    public class Statistics
    {
        public int TotalNotes { get; private set; }
        public int SuccessfulHits { get; private set; }
        public int Score { get; private set; }

        public Statistics(int totalNotes)
        {
            TotalNotes = totalNotes;
            SuccessfulHits = 0;
            Score = 0;
        }

        public void AddHit(int points)
        {
            SuccessfulHits++;
            Score += points;
        }

        public void RemovePoints(int points)
        {
            Score = Math.Max(0, Score - points);
        }

        public float GetAccuracy()
        {
            if (TotalNotes == 0) return 0;
            return ((float)SuccessfulHits / TotalNotes) * 100f;
        }

        public string GetRank()
        {
            float accuracy = GetAccuracy();

            if (accuracy >= 95f) return "<color=yellow>S</color>";
            if (accuracy >= 85f) return "<color=green>A</color>";
            if (accuracy >= 70f) return "<color=blue>B</color>";
            if (accuracy >= 50f) return "<color=orange>C</color>";
            return "<color=red>F</color>";
        }

        public string GetFormattedReport()
        {
            return $"THE DANCE IS OVER!\n\n" +
                   $"RANK: {GetRank()}\n\n" +
                   $"Points: {Score}\n" +
                   $"Hits: {SuccessfulHits} из {TotalNotes}\n" +
                   $"Accuracy: {GetAccuracy():F1}%";
        }
    }
}