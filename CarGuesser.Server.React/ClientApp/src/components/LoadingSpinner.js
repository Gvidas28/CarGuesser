import "../assets/components/loading-spinner.css";

export default function LoadingSpinner() {
  return (
    <div className='spinner'>
      <img className='spinner-picture' src='/spinner.gif'></img>
    </div>
  );
}
