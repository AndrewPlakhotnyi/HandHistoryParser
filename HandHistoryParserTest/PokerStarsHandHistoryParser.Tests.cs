using HandHistoryParser;
using HandHistoryParser.Common;
using HandHistoryParserTest.Common;
using NUnit.Framework;
using System.Collections.Immutable;
using System.Linq;

namespace HandHistoryParser.Tests;
public class PokerStarsHandHistoryParserTests {       
    [Test]
    public static void
    ParseHandHistoryTest02() {

            var handHistory = @"PokerStars Hand #118257024210:  Hold'em No Limit (€1/€2 EUR) - 2014/07/02 16:05:17 ET
Table 'Williams II' 6-max Seat #1 is the button
Seat 1: Aza85 (€252.28 in chips) 
Seat 2: El Tacuba (€376.42 in chips) 
Seat 3: tayfun222 (€230.85 in chips) 
Seat 4: what?NOpair? (€204.60 in chips) 
Seat 5: moxtm (€207 in chips) 
Seat 6: kkgoplay (€100 in chips) 
El Tacuba: posts small blind €1
tayfun222: posts big blind €2
*** HOLE CARDS ***
Dealt to tayfun222 [2c Ac]
what?NOpair?: raises €2.20 to €4.20
moxtm: folds 
kkgoplay: calls €4.20
Aza85: folds 
El Tacuba: folds 
tayfun222: calls €2.20
*** FLOP *** [Jc Qs Kc]
tayfun222: checks 
what?NOpair?: bets €10.39
kkgoplay: calls €10.39
tayfun222: calls €10.39
*** TURN *** [Jc Qs Kc] [8c]
tayfun222: checks 
El Tacuba leaves the table
ivanildo joins the table at seat #2 
what?NOpair?: checks 
kkgoplay: bets €28
tayfun222: calls €28
what?NOpair?: folds 
*** RIVER *** [Jc Qs Kc 8c] [8h]
tayfun222: checks 
kkgoplay: bets €53
tayfun222: calls €53
*** SHOW DOWN ***
kkgoplay: shows [Tc 9c] (a flush, King high)
tayfun222: shows [2c Ac] (a flush, Ace high)
tayfun222 collected €204.62 from pot
*** SUMMARY ***
Total pot €206.77 | Rake €2.15 
Board [Jc Qs Kc 8c 8h]
Seat 1: Aza85 (button) folded before Flop (didn't bet)
Seat 2: El Tacuba (small blind) folded before Flop
Seat 3: tayfun222 (big blind) showed [2c Ac] and won (€204.62) with a flush, Ace high
Seat 4: what?NOpair? folded on the Turn
Seat 5: moxtm folded before Flop (didn't bet)
Seat 6: kkgoplay showed [Tc 9c] and lost with a flush, King high".ParseHandHistoryText();


       handHistory.Players[0].Currency.Assert(Currencies.Euro);

       handHistory.Players[2].DealtCards.AssertSequence([
            new Card(CardRank.Deuce, CardSuit.Clubs),
            new Card(CardRank.Ace, CardSuit.Clubs)
        ]);

    }
}

