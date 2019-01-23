namespace vldaptest
{

    public class Rootobject
    {
        public string ExpectedPath { get; set; }
        public string ActualPath { get; set; }
        public Test[] Tests { get; set; }
    }

    public class Test
    {
        public string Id { get; set; }
        public string Command { get; set; }
        public string FileName { get; set; }
        public int Result;
    }
}
