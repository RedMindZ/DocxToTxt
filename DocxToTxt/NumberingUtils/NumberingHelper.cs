using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.NumberingUtils
{
    public static class NumberingHelper
    {
        public static List<NumberingInfo> BuildNumberingInfo(WordprocessingDocument doc)
        {
            List<NumberingInfo> docNumberingInfo = new List<NumberingInfo>();

            Numbering docNumbering = doc.MainDocumentPart.NumberingDefinitionsPart.Numbering;

            List<AbstractNum> abstractNums = docNumbering.OfType<AbstractNum>().ToList();
            List<NumberingInstance> numberingInstances = docNumbering.OfType<NumberingInstance>().ToList();

            foreach (NumberingInstance instance in numberingInstances)
            {
                NumberingInfo info = new NumberingInfo();

                info.NumberingInstance = instance;
                info.AbstractNum = abstractNums.Find(x => x.AbstractNumberId.Value == instance.AbstractNumId.Val.Value);
                info.Levels.AddRange(info.AbstractNum.OfType<Level>());

                foreach (LevelOverride levelOverride in info.NumberingInstance.OfType<LevelOverride>())
                {
                    Level level = levelOverride.Level;
                    int abstractLevelIndex = info.Levels.FindIndex(x => x.LevelIndex.Value == level.LevelIndex.Value);

                    if (abstractLevelIndex >= 0)
                    {
                        info.Levels[abstractLevelIndex] = level;
                    }
                    else
                    {
                        info.Levels.Add(level);
                    }
                }

                info.Levels.Sort((x, y) => x.LevelIndex.Value - y.LevelIndex.Value);
                docNumberingInfo.Add(info);
            }

            return docNumberingInfo;
        }
    }
}
