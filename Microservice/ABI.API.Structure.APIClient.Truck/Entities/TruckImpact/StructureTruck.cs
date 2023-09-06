namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    public class StructureTruck
    {
        public OpecpiniOut OpecpiniOut { get; set; }
        public PtecdireInput Ptecdire { get; set; }
        public PtecareaInput Ptecarea { get; set; }
        public PtecgereInput Ptecgere { get; set; }
        public PtecregiInput Ptecregi { get; set; }
        public PteczocoInput Pteczoco { get; set; }
        public PteczonaInput Pteczona { get; set; }
        public PtecterrInput Ptecterr { get; set; }


        public StructureTruck()
        {
            OpecpiniOut = new OpecpiniOut();
            Ptecdire = new PtecdireInput();
            Ptecarea = new PtecareaInput();
            Ptecgere = new PtecgereInput();
            Ptecregi = new PtecregiInput();
            Pteczoco = new PteczocoInput();
            Pteczona = new PteczonaInput();
            Ptecterr = new PtecterrInput();

        }

    }
}
