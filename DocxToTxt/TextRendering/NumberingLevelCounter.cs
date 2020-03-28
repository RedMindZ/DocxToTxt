using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class NumberingLevelCounter
    {
        private int _counter = 1;

        public int InitialNumber { get; set; } = 1;
        public int ResetIndex { get; set; } = 0;



        public NumberingLevelCounter(int initialNumber, int resetIndex)
        {
            InitialNumber = initialNumber;
            Reset();
        }

        public void Reset()
        {
            _counter = InitialNumber;
        }

        public void Update(int currentLevelIndex)
        {
            if (ResetIndex > currentLevelIndex)
            {
                Reset();
            }
        }

        public int GetNextLevelNumber()
        {
            int val = _counter;
            _counter++;

            return val;
        }
    }
}
