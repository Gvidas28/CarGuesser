import { useState, useEffect } from "react";
import "../assets/views/main-screen.css";
import LoadingSpinner from "../components/LoadingSpinner";
import Leaderboard from "../components/Leaderboard";

export default function MainScreen() {
  const [difficulty, setDifficulty] = useState("Easy");
  const [sessionId, setSessionId] = useState("");
  const [guessData, setGuessData] = useState({});
  const [loading, setLoading] = useState(false);
  const [gameStarted, setGameStarted] = useState(false);
  const [finalScore, setFinalScore] = useState(-1);
  const [leaderboard, setLeaderboard] = useState([]);
  const [name, setName] = useState("");

  const difficulties = ["Easy", "Normal", "Hard"];

  const startGame = () => {
    const createSessionRequest = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ Difficulty: difficulty })
    };

    fetch("session/create", createSessionRequest)
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          setSessionId(data.data);
        } else {
          console.log(data.message);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const getGuessData = () => {
    setLoading(true);

    fetch(`/guess/get?sessionId=${sessionId}`)
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          setGuessData(data.data);
          setGameStarted(true);
          setLoading(false);
        } else {
          console.log(data.message);
          setLoading(false);
        }
      })
      .catch((error) => {
        console.log(error);
        setLoading(false);
        console.log(error);
      });
  };

  const submitGuess = (answer) => {
    const submitGuessRequest = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        GuessId: guessData.guessId,
        SessionId: sessionId,
        Answer: answer
      })
    };

    setLoading(true);

    fetch(`/guess/submit`, submitGuessRequest)
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          if (data.data.gameOver) {
            setGameStarted(false);
            setFinalScore(data.data.totalScore);
            setLoading(false);
          } else {
            getGuessData();
          }
        } else {
          console.log(data.message);
          setLoading(false);
        }
      })
      .catch((error) => {
        console.log(error);
        setLoading(false);
        console.log(error);
      });
  };

  const getLeaderboard = () => {
    fetch("leaderboard/get")
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          setLeaderboard(data.data);
        } else {
          console.log(data.message);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const saveScore = () => {
    const addToLeadeboardRequest = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        SessionId: sessionId,
        Name: name
      })
    };

    fetch("/leaderboard/add", addToLeadeboardRequest)
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          getLeaderboard();
          setGuessData({});
          setName("");
          setGameStarted(false);
          setFinalScore(-1);
        } else {
          console.log(data.message);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    if (sessionId !== "") {
      getGuessData();
    }
  }, [sessionId]);

  useEffect(() => {
    getLeaderboard();
  }, []);

  return (
    <div className='background-container'>
      <aside className='side-bar'>
        <div className='side-bar-part'>
          <div className='side-bar-title'>SELECT DIFFICULTY</div>
          {difficulties.map((item, index) => (
            <button
              className={
                item === difficulty
                  ? "difficulty-button"
                  : "difficulty-button selected"
              }
              key={index}
              onClick={() => setDifficulty(item)}
            >
              {item}
            </button>
          ))}
          <button className='start-game-button' onClick={() => startGame()}>
            Start Game
          </button>
        </div>
        <div className='side-bar-part'>
          <div className='side-bar-title'>LEADERBOARD</div>
          <Leaderboard items={leaderboard} />
        </div>
      </aside>
      <div className='game-screen'>
        {!loading ? (
          gameStarted && (
            <div className='game-screen-inner'>
              <div className='game-title'>Car Guesser</div>
              <div className='session-information'>{`Remaining lives: ${guessData.remainingLives}`}</div>
              <div className='session-information'>{`Score: ${guessData.score}`}</div>
              {guessData && guessData.description && (
                <div className='guess-description-container'>
                  <div className='guess-description-text'>{`Engine Position: ${guessData.description.enginePosition}`}</div>
                  <div className='guess-description-text'>{`Engine Type: ${guessData.description.engineType}`}</div>
                  <div className='guess-description-text'>{`Horse Power: ${guessData.description.horsePower}`}</div>
                  <div className='guess-description-text'>{`Doors: ${guessData.description.doors}`}</div>
                  <div className='guess-description-text'>{`Drive: ${guessData.description.drive}`}</div>
                  <div className='guess-description-text'>{`Transmission: ${guessData.description.transmission}`}</div>
                  <div className='guess-description-text'>{`Country: ${guessData.description.country}`}</div>
                  <div className='guess-description-text'>{`Top Speed (km/h): ${guessData.description.topSpeed}`}</div>
                </div>
              )}
              <div className='options-container'>
                {guessData &&
                  guessData.options &&
                  guessData.options.map((item, index) => (
                    <div key={index} className='option'>
                      <img
                        className='option-image'
                        src={item.imageUrl}
                        onClick={() => submitGuess(item.id)}
                      />
                      <div className='option-text'>{`${item.make} ${item.model} ${item.year} ${item.trim}`}</div>
                    </div>
                  ))}
              </div>
            </div>
          )
        ) : (
          <LoadingSpinner />
        )}
        {!gameStarted && finalScore > -1 && (
          <div className='game-over-screen'>
            <div className='game-over-title'>GAME OVER!</div>
            <div className='game-over-text'>{`Your score for this session is ${finalScore}`}</div>
            <div className='game-over-text'>
              Enter your name below if you want your score to be saved in the
              leaderboard
            </div>
            <input
              className='save-score-input'
              placeholder='Name'
              value={name}
              onChange={(e) => setName(e.target.value)}
              maxLength={20}
            ></input>
            <button className='save-score-button' onClick={saveScore}>
              Save Score
            </button>
          </div>
        )}
      </div>
    </div>
  );
}
