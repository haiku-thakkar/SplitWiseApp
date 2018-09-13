namespace SecondSplitWise.Response
{
    public class MemberResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MemberResponse()
        {

        }

        public MemberResponse(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}