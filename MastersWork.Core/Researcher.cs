using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersWork.Core
{
    public class Researcher
    {
        private readonly uint _bitsPerSecond;
        private readonly uint _bitsPerBlock;
        private readonly int _maxRandom;
        private readonly Random _rand;     

        public Researcher(uint bitsPerSecond, uint bitsPerBlock, byte bitErrorProbabilityPowerAbs)
        {
            _bitsPerSecond = bitsPerSecond;
            _bitsPerBlock = bitsPerBlock;

            _maxRandom = (int)Math.Pow(10, bitErrorProbabilityPowerAbs + 1);
            _rand = new Random();
        }

        public FullTimeResult[] PerformResearch(ushort numberOfExperiments, ushort secondsPerExperiment)
        {
            var result = new FullTimeResult[numberOfExperiments];

            for (var i = 0; i < numberOfExperiments; i++)
            {
                result[i] = PerformTransmitionDuring(secondsPerExperiment);
            }

            return result;
        }

        private OneSecondResult PerformTransmitionDuringOneSecond(uint bitInBlockCount = 0)
        {
            uint bitErrorCount = 0;
            uint erroredBlockCount = 0;
            uint unerroredBlockCount = 0;

            var blockIsErrored = false;

            for (var bitIndex = 0; bitIndex < _bitsPerSecond; bitIndex++)
            {
                if (_rand.Next(_maxRandom) < 10)
                {
                    bitErrorCount++;
                    blockIsErrored = true;
                }

                if (bitInBlockCount < _bitsPerBlock)
                {
                    bitInBlockCount++;
                }
                else
                {
                    if (blockIsErrored)
                    {
                        erroredBlockCount++;
                    }
                    else
                    {
                        unerroredBlockCount++;
                    }
                    blockIsErrored = false;
                    bitInBlockCount = 0;
                }
            }

            return new OneSecondResult
            {
                BitErrors = bitErrorCount,
                BitErrorRatio = (double)bitErrorCount / _bitsPerSecond,

                BlockErrors = erroredBlockCount,
                ErroredBlockRatio = 1 / (1 + (double)unerroredBlockCount / erroredBlockCount),

                BitInBlockCount = bitInBlockCount
            };
        }

        private FullTimeResult PerformTransmitionDuring(ushort seconds, uint bitInBlockCount = 0)
        {
            ushort erroredSecondsCount = 0;
            ushort severelyErroredSecondsCount = 0;
            double avgBitErrorRatio = 0;

            for (int i = 0; i < seconds; i++)
            {
                var result = PerformTransmitionDuringOneSecond(bitInBlockCount);
                bitInBlockCount = result.BitInBlockCount;

                avgBitErrorRatio = (avgBitErrorRatio * i + result.BitErrorRatio) / (i + 1);

                if (result.BlockErrors > 0) erroredSecondsCount++;
                if (result.ErroredBlockRatio >= 0.3) severelyErroredSecondsCount++;
            }

            return new FullTimeResult
            {
                BitErrorRatio = avgBitErrorRatio,

                ErroredSeconds = erroredSecondsCount,
                ErroredSecondsRatio = (double)erroredSecondsCount / seconds,

                SeverelyErroredSeconds = severelyErroredSecondsCount,
                SeverelyErroredSecondsRatio = (double)severelyErroredSecondsCount / seconds
            };
        }
    }
}
