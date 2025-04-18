import logo from './logo.svg';
import './App.css';
import SpeechToText from './component/SpeechToText.jsx'; 
import DisplayData from './component/DisplayData.jsx'; 

function App() {
  return (
    <div className="App">
      <div id="voicetotext">
        <h2>Data at voice command.</h2>
        <SpeechToText />
        <DisplayData />
      </div>
    </div>
  );
}

export default App;
