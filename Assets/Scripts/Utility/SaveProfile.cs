using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    // Save Object of Player Profile
    public class SaveProfile
    {
        public int Currency;
        public int HighScore;
        private List<int> planeHighScore = new List<int>(); 
        private List<String> availablePlanes = new List<String>();

    }
}