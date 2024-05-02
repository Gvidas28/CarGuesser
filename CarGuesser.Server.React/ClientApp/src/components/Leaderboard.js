import "../assets/components/leaderboard.css";

export default function Leaderboard(props) {
  return (
    <div className='leaderboard-body'>
      <div className='legend-item'>
        <div className='leaderboard-text legend'>NAME</div>
        <div className='leaderboard-text legend'>DIFFICULTY</div>
        <div className='leaderboard-text legend'>SCORE</div>
      </div>
      {props.items &&
        props.items.map((item, index) => (
          <div key={index} className='leaderboard-item'>
            <div className='leaderboard-text'>{item.name}</div>
            <div className='leaderboard-text'>{item.difficulty}</div>
            <div className='leaderboard-text'>{item.score}</div>
          </div>
        ))}
    </div>
  );
}
