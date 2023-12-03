using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Card> Deck { get; set; }

        public Game(List<string> playerNames)
        {
            Players = playerNames.Select(name => new Player(name)).ToList();
            Deck = GenerateDeck();
            ShuffleDeck();
            DealCards();
        }

        private List<Card> GenerateDeck()
        {
            var suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
            var ranks = new string[] { "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            return (from suit in suits
                    from rank in ranks
                    select new Card(suit, rank)).ToList();
        }

        private void ShuffleDeck()
        {
            var random = new Random();
            Deck = Deck.OrderBy(card => random.Next()).ToList();
        }

        private void DealCards()
        {
            var cardsPerPlayer = Deck.Count / Players.Count;
            for (var i = 0; i < Players.Count; i++)
            {
                Players[i].Hand.AddRange(Deck.Skip(i * cardsPerPlayer).Take(cardsPerPlayer));
            }
        }

        public void Play()
        {
            while (Players.All(player => player.Hand.Count > 0))
            {
                var playedCards = Players.Select(player => player.Hand.First()).ToList();
                Console.WriteLine("Cards on the table:");
                foreach (var card in playedCards)
                {
                    Console.WriteLine($"{card.Rank} of {card.Suit}");
                }

                var winningCard = playedCards.OrderByDescending(card => GetCardValue(card)).First();
                var winningPlayer = Players.First(player => player.Hand.First() == winningCard);
                Console.WriteLine($"{winningPlayer.Name} wins the round!\n");

                Players.ForEach(player => player.Hand.RemoveAt(0));
                winningPlayer.Hand.AddRange(playedCards);

            }

            var winner = Players.OrderByDescending(player => player.Hand.Count).First();
            Console.WriteLine($"{winner.Name} wins the game!");
        }

        private int GetCardValue(Card card)
        {
            var ranks = new string[] { "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            return Array.IndexOf(ranks, card.Rank) + 1;
        }
    }
}
