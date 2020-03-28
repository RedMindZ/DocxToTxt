using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class NumberingCounter
    {
        private NumberingLevelCounter[] _counters;



        public NumberingCounter(int levelCount)
        {
            _counters = new NumberingLevelCounter[levelCount];

            for (int i = 0; i < _counters.Length; i++)
            {
                _counters[i] = new NumberingLevelCounter(1, 0);
            }
        }



        public void SetCounterInitialNumber(int levelIndex, int initialNumber)
        {
            _counters[levelIndex].InitialNumber = initialNumber;
        }

        public void SetCounterResetIndex(int levelIndex, int resetIndex)
        {
            _counters[levelIndex].ResetIndex = resetIndex;
        }

        public void ResetCounter(int levelIndex)
        {
            _counters[levelIndex].Reset();
        }

        public void UpdateCouters(int currentLevelIndex)
        {
            foreach (NumberingLevelCounter counter in _counters)
            {
                counter.Update(currentLevelIndex);
            }
        }

        public int GetNextLevelNumber(int levelIndex)
        {
            return _counters[levelIndex].GetNextLevelNumber();
        }
    }
}
