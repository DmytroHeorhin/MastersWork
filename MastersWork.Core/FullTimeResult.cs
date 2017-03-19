namespace MastersWork.Core
{
    public struct FullTimeResult
    {
        public double BitErrorRatio;

        public uint ErroredBlocks;
        public uint ErroredBlockRatio;

        public ushort ErroredSeconds;
        public double ErroredSecondsRatio;

        public ushort SeverelyErroredSeconds;
        public double SeverelyErroredSecondsRatio;
    }
}