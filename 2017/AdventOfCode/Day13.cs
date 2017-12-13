using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day13
    {
        public static int SolvePart1(string[] getPuzzleInput)
        {
            var layerScanners = getPuzzleInput.Select(x =>
            {
                var split = x.Split(new []{": "}, StringSplitOptions.None).Select(int.Parse).ToArray();
                return new LayerScanner { Depth = split[0], Range = split[1]};
            }).ToArray();

            var maxDepth = layerScanners.Max(x => x.Depth);
            var totalSeverity = 0;
            for (int picosecond = 0; picosecond <= maxDepth; picosecond++)
            {
                foreach (var layerScanner in layerScanners)
                {
                    if (layerScanner.Depth == picosecond && layerScanner.ScannerLocation == 1)
                        totalSeverity += layerScanner.Severity;
                    layerScanner.MoveScanner();
                }
            }
            return totalSeverity;
        }

        public static int SolvePart2(string[] getPuzzleInput)
        {
            throw new NotImplementedException();
        }

        public class LayerScanner
        {
            public int Depth { get; set; }
            public int Range { get; set; }
            public int ScannerLocation { get; private set; } = 1;
            public bool MovingDown { get; private set; } = true;
            public int Severity => Depth * Range;
            
            public void MoveScanner()
            {
                ScannerLocation = MovingDown ? ScannerLocation + 1 : ScannerLocation - 1;
                if (ScannerLocation == 1 || ScannerLocation == Range)
                    MovingDown = !MovingDown;
            }
        }
    }
}
