namespace TicTacDough.Shared.Domain {
    public abstract class BaseDomainModel {
        public int ID { get; set; }
        public DateTime date_added { get; set; }
    }
}