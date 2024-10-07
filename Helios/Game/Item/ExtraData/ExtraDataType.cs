namespace Helios.Game
{
    public enum ExtraDataType
    {
        Legacy = 0, //s
        Map = 1,  //count -> s - s
        StringArray = 2, //count -> s
        VoteResult = 3, //s i
        Empty = 4, //
        IntArray = 5, //count -> i
        HighScore = 6, //
        Crackable = 7, //sii

        ListList = 100, // used for custom parameters
        /// Format is
        ///  [ [ 1, 2, 3, etc],
        ///    [ 1, 2, 3, etc],
        ///    [ 1, 2, 3, etc]
        ///  ]
        ///
        Int = 101
    }
}
