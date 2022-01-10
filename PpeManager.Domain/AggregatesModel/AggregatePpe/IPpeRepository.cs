namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public interface IPpeRepository : IRepository<Ppe>
    {
        public Ppe Add(Ppe entity);

        public Ppe Update(Ppe entity);

        public Ppe Find(Func<Ppe, bool> p);

        public Ppe FindById(int id);

        public IEnumerable<Ppe> FindAll(Func<Ppe, bool> p);

        public PpeCertification FindCertification(Func<PpeCertification, bool> p);
    }
}
