using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public static class DocxUnitConverter
    {
        public static int PointsToCharacters(int points)
        {
            return points / 4;
        }

        public static int TwipsToCharacters(int twips)
        {
            // Visit VSauce

            return PointsToCharacters(twips / 20);
        }
    }
}
