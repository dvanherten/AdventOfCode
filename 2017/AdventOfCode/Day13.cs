using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day13
    {
        private const int STARTING_SCANNER_LOCATION = 1;

        public static int SolvePart1(string[] getPuzzleInput)
        {
            var layerScanners = ParseInputForScanners(getPuzzleInput);

            var maxDepth = layerScanners.Max(x => x.Depth);
            var totalSeverity = 0;
            for (int picosecond = 0; picosecond <= maxDepth; picosecond++)
            {
                foreach (var layerScanner in layerScanners)
                {
                    if (layerScanner.Depth == picosecond && layerScanner.ScannerLocation == STARTING_SCANNER_LOCATION)
                        totalSeverity += layerScanner.Severity;
                    layerScanner.MoveScanner();
                }
            }
            return totalSeverity;
        }

        public static int SolvePart2(string[] getPuzzleInput)
        {
            var layerScanners = ParseInputForScanners(getPuzzleInput);

            // Shift the scanners to simulate our spot being at all layers at once.
            foreach(var layerScanner in layerScanners)
                layerScanner.SetLocationToPicosecond(layerScanner.Depth);

            var picosecondDelay = 1;
            while (true)
            {
                foreach (var layerScanner in layerScanners)
                    layerScanner.MoveScanner();

                // Since we are simulating being in all spots at once, we need to break when none of them are the starting location.
                if (layerScanners.All(x => x.ScannerLocation != STARTING_SCANNER_LOCATION))
                    break;

                picosecondDelay++;
            }
            return picosecondDelay;
        }

        private static LayerScanner[] ParseInputForScanners(string[] getPuzzleInput)
        {
            return getPuzzleInput.Select(x =>
            {
                var split = x.Split(new[] { ": " }, StringSplitOptions.None).Select(int.Parse).ToArray();
                return new LayerScanner { Depth = split[0], Range = split[1] };
            }).ToArray();
        }

        public class LayerScanner
        {
            public int Depth { get; set; }
            public int Range { get; set; }
            public int ScannerLocation { get; private set; } = STARTING_SCANNER_LOCATION;
            public bool MovingDown { get; private set; } = true;
            public int Severity => Depth * Range;
            
            public void MoveScanner()
            {
                ScannerLocation = MovingDown ? ScannerLocation + 1 : ScannerLocation - 1;
                if (ScannerLocation == 1 || ScannerLocation == Range)
                    MovingDown = !MovingDown;
            }

            public void SetLocationToPicosecond(int picosecond)
            {
                for (int i = 0; i < picosecond; i++)
                    MoveScanner();
            }
        }
    }
}
