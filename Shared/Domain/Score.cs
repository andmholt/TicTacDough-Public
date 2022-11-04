using System.ComponentModel.DataAnnotations;

namespace TicTacDough.Shared.Domain {
    public class Score : BaseDomainModel {

        public Score(int score, string username) {
            this.score = score;
            this.username = username;
        }

        [Required]
        public int score { get; set; }

        [Required]
        public string username { get; set; }
    }
}