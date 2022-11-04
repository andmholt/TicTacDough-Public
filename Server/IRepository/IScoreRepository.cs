using TicTacDough.Shared.Domain;

namespace TicTacDough.Server.IRepository {
    public interface IScoreRepository : IDisposable {
        public IEnumerable<Score> GetScores();
        public void InsertScore(Score score);
    }
}