namespace AltayChillPlace.ApiResponses
{
    public class HouseResponse
    {
        public int IdHouse { get; set; }

        public int HouseNumber { get; set; }

        public string HouseName { get; set; }

        public int PricePerDay { get; set; }

        public int MaxNumberOfPeople { get; set; }

        public int RoomSize { get; set; }

        public string RoomDescription { get; set; }

        public byte[] PhotoOfTheRoom { get; set; }

        public int FkTypeOfNumber { get; set; }

        public TypeOfNumberResponse TypeOfNumber { get; set; }
        public bool IsDetailView { get; internal set; }
    }
}
